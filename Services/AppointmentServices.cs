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

        public async Task<Appointment> BookAppointmentAsync(ObjectId patientId, ObjectId doctorId,
                                                            DateTime startTime, DateTime endTime, string reason)
        {
            try
            {
                var patient = await _patientRepository.GetByIdAsync(patientId);
                if (patient == null)
                    throw new ResourceNotFoundException($"Patient with ID {patientId} not found.");

                var doctor = await _doctorRepository.GetByIdAsync(doctorId);
                if (doctor == null)
                    throw new ResourceNotFoundException($"Doctor with ID {doctorId} not found.");

                ValidateTimeSlot(startTime, endTime, doctor);

                var conflicts = await _appointmentRepository.GetConflictingAppointmentsAsync(doctorId, startTime, endTime);
                if (conflicts.Count > 0)
                {
                    var conflictInfo = string.Join(", ", conflicts.Select(c => $"{c.StartTime:HH:mm}-{c.EndTime:HH:mm}"));
                    throw new DoubleBookingException(
                        $"Doctor has conflicting appointments during this time slot: {conflictInfo}");
                }

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
    }

    public class NotificationService
    {
        private readonly List<string> _notificationQueue;
        private readonly string _logFilePath;

        public NotificationService(string logFilePath = "notifications.log")
        {
            _notificationQueue = new List<string>();
            _logFilePath = logFilePath;
        }

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

        public void SendAllNotifications()
        {
            foreach (var notification in _notificationQueue)
            {
                SendNotification(notification);
            }
            _notificationQueue.Clear();
        }

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
    }

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
