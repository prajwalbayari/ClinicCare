using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClinicAppointmentManager.Models;
using ClinicAppointmentManager.Repositories;
using ClinicAppointmentManager.Exceptions;

namespace ClinicAppointmentManager.Services
{
    public class SampleDataSeeder
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly string _logFilePath;

        public SampleDataSeeder(IPatientRepository patientRepository,
                                IDoctorRepository doctorRepository,
                                IAppointmentRepository appointmentRepository,
                                string logFilePath = "seeding.log")
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _logFilePath = logFilePath;
        }

        public async Task SeedSampleDataAsync()
        {
            try
            {
                LogMessage("Starting data seeding...");

                var patients = await SeedPatientsAsync();
                LogMessage($"✓ Seeded {patients.Count} patients");

                var doctors = await SeedDoctorsAsync();
                LogMessage($"✓ Seeded {doctors.Count} doctors");

                var appointments = await SeedAppointmentsAsync(patients, doctors);
                LogMessage($"✓ Seeded {appointments.Count} appointments");

                LogMessage("Data seeding completed successfully!");
            }
            catch (Exception ex)
            {
                LogMessage($"✗ Error during seeding: {ex.Message}");
                throw;
            }
        }

        private async Task<List<Patient>> SeedPatientsAsync()
        {
            var patients = new List<Patient>
            {
                new Patient
                {
                    Name = "John Smith",
                    Age = 39,
                    Gender = "Male",
                    Phone = "555-0101",
                    Email = "john.smith@example.com",
                    MedicalHistory = "Allergy to Penicillin, Hypertension"
                },
                new Patient
                {
                    Name = "Sarah Johnson",
                    Age = 35,
                    Gender = "Female",
                    Phone = "555-0102",
                    Email = "sarah.johnson@example.com",
                    MedicalHistory = "Asthma, No known allergies"
                },
                new Patient
                {
                    Name = "Michael Brown",
                    Age = 47,
                    Gender = "Male",
                    Phone = "555-0103",
                    Email = "michael.brown@example.com",
                    MedicalHistory = "Diabetes Type 2, Overweight"
                },
                new Patient
                {
                    Name = "Emily Davis",
                    Age = 30,
                    Gender = "Female",
                    Phone = "555-0104",
                    Email = "emily.davis@example.com",
                    MedicalHistory = "No significant medical history"
                }
            };

            var seededPatients = new List<Patient>();
            foreach (var patient in patients)
            {
                try
                {
                    var seeded = await _patientRepository.AddAsync(patient);
                    seededPatients.Add(seeded);
                }
                catch (DatabaseException ex)
                {
                    LogMessage($"⚠ Patient {patient.Name} might already exist: {ex.Message}");
                }
            }

            return seededPatients;
        }

        private async Task<List<Doctor>> SeedDoctorsAsync()
        {
            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    Name = "Alice Williams",
                    Specialization = "Cardiology",
                    LicenseNumber = "MD-001",
                    Email = "alice.williams@clinic.com",
                    Phone = "555-1001",
                    WorkingHoursStart = "09:00",
                    WorkingHoursEnd = "17:00",
                    AppointmentDurationMinutes = 30
                },
                new Doctor
                {
                    Name = "Robert Martinez",
                    Specialization = "Dermatology",
                    LicenseNumber = "MD-002",
                    Email = "robert.martinez@clinic.com",
                    Phone = "555-1002",
                    WorkingHoursStart = "10:00",
                    WorkingHoursEnd = "18:00",
                    AppointmentDurationMinutes = 20
                },
                new Doctor
                {
                    Name = "Jennifer Lee",
                    Specialization = "General Practice",
                    LicenseNumber = "MD-003",
                    Email = "jennifer.lee@clinic.com",
                    Phone = "555-1003",
                    WorkingHoursStart = "08:00",
                    WorkingHoursEnd = "16:00",
                    AppointmentDurationMinutes = 30
                }
            };

            var seededDoctors = new List<Doctor>();
            foreach (var doctor in doctors)
            {
                try
                {
                    var seeded = await _doctorRepository.AddAsync(doctor);
                    seededDoctors.Add(seeded);
                }
                catch (DatabaseException ex)
                {
                    LogMessage($"⚠ Doctor {doctor.LicenseNumber} might already exist: {ex.Message}");
                }
            }

            return seededDoctors;
        }

        private async Task<List<Appointment>> SeedAppointmentsAsync(List<Patient> patients, List<Doctor> doctors)
        {
            var seededAppointments = new List<Appointment>();
            if (patients.Count == 0 || doctors.Count == 0)
                return seededAppointments;

            var today = DateTime.Now.Date;
            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    PatientId = patients[0].Id,
                    DoctorId = doctors[0].Id,
                    StartTime = today.AddHours(9),
                    EndTime = today.AddHours(9.5),
                    Reason = "Regular checkup",
                    Status = AppointmentStatus.Scheduled,
                    Notes = "Patient reports no current symptoms"
                },
                new Appointment
                {
                    PatientId = patients[1].Id,
                    DoctorId = doctors[0].Id,
                    StartTime = today.AddHours(10),
                    EndTime = today.AddHours(10.5),
                    Reason = "Follow-up consultation",
                    Status = AppointmentStatus.Scheduled,
                    Notes = "Monitor blood pressure"
                },
                new Appointment
                {
                    PatientId = patients[2].Id,
                    DoctorId = doctors[1].Id,
                    StartTime = today.AddHours(10),
                    EndTime = today.AddHours(10).AddMinutes(20),
                    Reason = "Skin condition review",
                    Status = AppointmentStatus.Scheduled,
                    Notes = "Dermatology assessment"
                },
                new Appointment
                {
                    PatientId = patients[3].Id,
                    DoctorId = doctors[2].Id,
                    StartTime = today.AddHours(8),
                    EndTime = today.AddHours(8.5),
                    Reason = "Annual physical",
                    Status = AppointmentStatus.Scheduled,
                    Notes = "Routine annual checkup"
                }
            };

            foreach (var appointment in appointments)
            {
                try
                {
                    var seeded = await _appointmentRepository.AddAsync(appointment);
                    seededAppointments.Add(seeded);
                }
                catch (Exception ex)
                {
                    LogMessage($"⚠ Could not seed appointment: {ex.Message}");
                }
            }

            return seededAppointments;
        }

        private void LogMessage(string message)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var logEntry = $"[{timestamp}] {message}";
            Console.WriteLine(logEntry);

            try
            {
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not write to log file: {ex.Message}");
            }
        }
    }
}
