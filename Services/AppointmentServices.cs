using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicAppointmentManager.Models;
using ClinicAppointmentManager.Repositories;
using ClinicAppointmentManager.Exceptions;

namespace ClinicAppointmentManager.Services
{
    /// <summary>
    /// Service for scheduling appointments with conflict detection.
    /// </summary>
    public class SchedulerService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public SchedulerService(IAppointmentRepository appointmentRepository,
                                IDoctorRepository doctorRepository,
                                IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Books an appointment after validating time slots and checking for conflicts.
        /// Double-booking prevention algorithm:
        /// 1. Validates that the appointment time is within doctor's working hours
        /// 2. Validates that start time is before end time and end time is in the future
        /// 3. Queries all non-cancelled appointments for the doctor
        /// 4. Checks if the new appointment's time range overlaps with any existing appointment
        /// 5. An overlap occurs when: existing.StartTime < new.EndTime AND existing.EndTime > new.StartTime
        /// 6. If any overlap is found, throws DoubleBookingException
        /// 7. Only after all validations pass is the appointment inserted into the database
        /// </summary>
        public async Task<Appointment> BookAppointmentAsync(ObjectId patientId, ObjectId doctorId,
                                                            DateTime startTime, DateTime endTime, string reason)
        {
            try
            {
                // Validate patient exists
                var patient = await _patientRepository.GetByIdAsync(patientId);
                if (patient == null)
                    throw new ResourceNotFoundException($"Patient with ID {patientId} not found.");

                // Validate doctor exists
                var doctor = await _doctorRepository.GetByIdAsync(doctorId);
                if (doctor == null)
                    throw new ResourceNotFoundException($"Doctor with ID {doctorId} not found.");

                // Validate time slot
                ValidateTimeSlot(startTime, endTime, doctor);

                // Check for conflicts (double-booking)
                var conflicts = await _appointmentRepository.GetConflictingAppointmentsAsync(doctorId, startTime, endTime);
                if (conflicts.Count > 0)
                {
                    var conflictInfo = string.Join(", ", conflicts.Select(c => $"{c.StartTime:HH:mm}-{c.EndTime:HH:mm}"));
                    throw new DoubleBookingException(
                        $"Doctor has conflicting appointments during this time slot: {conflictInfo}");
                }

                // Create and add appointment
                var appointment = new Appointment
                {
                    PatientId = patientId,
                    DoctorId = doctorId,
                    StartTime = startTime,
                    EndTime = endTime,
                    Reason = reason,
                    Status = AppointmentStatus.Scheduled,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                return await _appointmentRepository.AddAsync(appointment);
            }
            catch (DoubleBookingException)
            {
                throw;
            }
            catch (InvalidTimeSlotException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error booking appointment: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validates that the appointment time is within doctor's working hours.
        /// </summary>
        private void ValidateTimeSlot(DateTime startTime, DateTime endTime, Doctor doctor)
        {
            if (startTime >= endTime)
                throw new InvalidTimeSlotException("Start time must be before end time.");

            if (endTime <= DateTime.Now)
                throw new InvalidTimeSlotException("Appointment time must be in the future.");

            if (!TimeSpan.TryParse(doctor.WorkingHoursStart, out var workStart) ||
                !TimeSpan.TryParse(doctor.WorkingHoursEnd, out var workEnd))
                throw new InvalidTimeSlotException("Doctor's working hours are not properly configured.");

            var appointmentStart = startTime.TimeOfDay;
            var appointmentEnd = endTime.TimeOfDay;

            if (appointmentStart < workStart || appointmentEnd > workEnd)
                throw new InvalidTimeSlotException(
                    $"Appointment must be within doctor's working hours ({doctor.WorkingHoursStart} - {doctor.WorkingHoursEnd}).");

            var durationMinutes = (int)(endTime - startTime).TotalMinutes;
            if (durationMinutes != doctor.AppointmentDurationMinutes)
                throw new InvalidTimeSlotException(
                    $"Appointment duration must be {doctor.AppointmentDurationMinutes} minutes.");
        }

        /// <summary>
        /// Cancels an existing appointment.
        /// </summary>
        public async Task<Appointment> CancelAppointmentAsync(ObjectId appointmentId)
        {
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
                if (appointment == null)
                    throw new ResourceNotFoundException($"Appointment with ID {appointmentId} not found.");

                appointment.Status = AppointmentStatus.Cancelled;
                appointment.UpdatedAt = DateTime.UtcNow;

                return await _appointmentRepository.UpdateAsync(appointment);
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error cancelling appointment: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets available time slots for a doctor on a specific date.
        /// </summary>
        public async Task<List<(DateTime, DateTime)>> GetAvailableTimeSlotsAsync(ObjectId doctorId, DateTime date)
        {
            try
            {
                var doctor = await _doctorRepository.GetByIdAsync(doctorId);
                if (doctor == null)
                    throw new ResourceNotFoundException($"Doctor with ID {doctorId} not found.");

                var appointments = await _appointmentRepository.GetAppointmentsForDoctorAsync(
                    doctorId,
                    date.Date,
                    date.Date.AddDays(1).AddSeconds(-1)
                );

                var availableSlots = new List<(DateTime, DateTime)>();

                if (!TimeSpan.TryParse(doctor.WorkingHoursStart, out var workStart) ||
                    !TimeSpan.TryParse(doctor.WorkingHoursEnd, out var workEnd))
                    return availableSlots;

                var currentTime = date.Date.Add(workStart);
                var endTime = date.Date.Add(workEnd);

                while (currentTime < endTime)
                {
                    var slotEnd = currentTime.AddMinutes(doctor.AppointmentDurationMinutes);
                    if (slotEnd > endTime) break;

                    var isConflict = appointments.Any(a =>
                        a.StartTime < slotEnd && a.EndTime > currentTime
                    );

                    if (!isConflict)
                    {
                        availableSlots.Add((currentTime, slotEnd));
                    }

                    currentTime = slotEnd;
                }

                return availableSlots;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error getting available slots: {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Service for sending appointment notifications and reminders.
    /// </summary>
    public class NotificationService
    {
        private readonly List<string> _notificationQueue;
        private readonly string _logFilePath;

        public NotificationService(string logFilePath = "notifications.log")
        {
            _notificationQueue = new List<string>();
            _logFilePath = logFilePath;
        }

        /// <summary>
        /// Queues a reminder notification.
        /// </summary>
        public void QueueReminder(Appointment appointment, Patient patient, Doctor doctor)
        {
            try
            {
                var reminder = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Reminder: Patient {patient.Name} has appointment with Dr. {doctor.Name} on {appointment.StartTime:yyyy-MM-dd HH:mm}";
                _notificationQueue.Add(reminder);
                LogNotification(reminder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error queuing reminder: {ex.Message}");
            }
        }

        /// <summary>
        /// Sends all queued notifications (in demo mode, shows message boxes).
        /// </summary>
        public void SendAllNotifications()
        {
            foreach (var notification in _notificationQueue)
            {
                SendNotification(notification);
            }
            _notificationQueue.Clear();
        }

        /// <summary>
        /// Sends a single notification.
        /// </summary>
        public void SendNotification(string message)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show(message, "Appointment Notification",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
                LogNotification(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending notification: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs notifications to a file.
        /// </summary>
        private void LogNotification(string message)
        {
            try
            {
                File.AppendAllText(_logFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging notification: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all queued notifications.
        /// </summary>
        public List<string> GetQueuedNotifications()
        {
            return new List<string>(_notificationQueue);
        }
    }

    /// <summary>
    /// Service for generating appointment reports and exports.
    /// </summary>
    public class ReportService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public ReportService(IAppointmentRepository appointmentRepository,
                             IDoctorRepository doctorRepository,
                             IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// Exports appointments to a CSV file.
        /// </summary>
        public async Task<string> ExportAppointmentsToCSVAsync(DateTime date, string filePath = null)
        {
            try
            {
                filePath = filePath ?? $"appointments_{date:yyyy-MM-dd}.csv";

                var appointments = await _appointmentRepository.FindByFilterAsync(a =>
                    a.StartTime.Date == date.Date &&
                    a.Status != AppointmentStatus.Cancelled
                );

                var csvContent = new StringBuilder();
                csvContent.AppendLine("Patient,Doctor,Start Time,End Time,Reason,Status,Duration (mins)");

                foreach (var appt in appointments)
                {
                    var patient = await _patientRepository.GetByIdAsync(appt.PatientId);
                    var doctor = await _doctorRepository.GetByIdAsync(appt.DoctorId);
                    var duration = (int)(appt.EndTime - appt.StartTime).TotalMinutes;

                    var patientName = patient?.Name ?? "Unknown";
                    var doctorName = doctor?.Name ?? "Unknown";

                    csvContent.AppendLine(
                        $"\"{patientName}\",\"Dr. {doctorName}\",{appt.StartTime:yyyy-MM-dd HH:mm}," +
                        $"{appt.EndTime:yyyy-MM-dd HH:mm},\"{appt.Reason}\",{appt.Status},{duration}"
                    );
                }

                File.WriteAllText(filePath, csvContent.ToString());
                return filePath;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error exporting appointments: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets a summary report of appointments for a doctor on a given date.
        /// </summary>
        public async Task<string> GenerateDoctorDaySummaryAsync(ObjectId doctorId, DateTime date)
        {
            try
            {
                var doctor = await _doctorRepository.GetByIdAsync(doctorId);
                if (doctor == null)
                    throw new ResourceNotFoundException($"Doctor with ID {doctorId} not found.");

                var appointments = await _appointmentRepository.GetAppointmentsForDoctorAsync(
                    doctorId,
                    date.Date,
                    date.Date.AddDays(1).AddSeconds(-1)
                );

                var summary = new StringBuilder();
                summary.AppendLine($"=== Dr. {doctor.Name} - Schedule for {date:yyyy-MM-dd} ===");
                summary.AppendLine($"Specialization: {doctor.Specialization}");
                summary.AppendLine($"Total Appointments: {appointments.Count}");
                summary.AppendLine();

                foreach (var appt in appointments)
                {
                    var patient = await _patientRepository.GetByIdAsync(appt.PatientId);
                    var startLocal = appt.StartTime.ToLocalTime();
                    var endLocal = appt.EndTime.ToLocalTime();
                    summary.AppendLine($"{startLocal:hh:mm tt} - {endLocal:hh:mm tt}: {patient?.Name ?? "Unknown"} ({appt.Reason})");
                }

                return summary.ToString();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error generating report: {ex.Message}", ex);
            }
        }
    }
}
