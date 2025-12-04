using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicAppointmentManager.Models;
using ClinicAppointmentManager.Data;
using ClinicAppointmentManager.Exceptions;

namespace ClinicAppointmentManager.Repositories
{
    /// <summary>
    /// Repository implementation for Patient data access.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly IMongoCollection<Patient> _collection;

        public PatientRepository(MongoDbContext context)
        {
            _collection = context.Patients;
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            try
            {
                if (!patient.IsValid())
                    throw new ArgumentException("Patient data is invalid.");

                await _collection.InsertOneAsync(patient);
                return patient;
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new DatabaseException($"Patient with email {patient.Email} already exists.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error adding patient: {ex.Message}", ex);
            }
        }

        public async Task<Patient> GetByIdAsync(ObjectId id)
        {
            try
            {
                return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving patient: {ex.Message}", ex);
            }
        }

        public async Task<Patient> GetByEmailAsync(string email)
        {
            try
            {
                return await _collection.Find(p => p.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving patient by email: {ex.Message}", ex);
            }
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            try
            {
                return await _collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving all patients: {ex.Message}", ex);
            }
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            try
            {
                if (!patient.IsValid())
                    throw new ArgumentException("Patient data is invalid.");

                patient.UpdatedAt = DateTime.UtcNow;
                var result = await _collection.ReplaceOneAsync(p => p.Id == patient.Id, patient);

                if (result.ModifiedCount == 0)
                    throw new ResourceNotFoundException($"Patient with ID {patient.Id} not found.");

                return patient;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error updating patient: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(ObjectId id)
        {
            try
            {
                var result = await _collection.DeleteOneAsync(p => p.Id == id);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error deleting patient: {ex.Message}", ex);
            }
        }

        public async Task<List<Patient>> FindByFilterAsync(Func<Patient, bool> filter)
        {
            try
            {
                var allPatients = await GetAllAsync();
                return allPatients.Where(filter).ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error filtering patients: {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Repository implementation for Doctor data access.
    /// </summary>
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IMongoCollection<Doctor> _collection;

        public DoctorRepository(MongoDbContext context)
        {
            _collection = context.Doctors;
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            try
            {
                if (!doctor.IsValid())
                    throw new ArgumentException("Doctor data is invalid.");

                await _collection.InsertOneAsync(doctor);
                return doctor;
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new DatabaseException($"Doctor with license number {doctor.LicenseNumber} already exists.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error adding doctor: {ex.Message}", ex);
            }
        }

        public async Task<Doctor> GetByIdAsync(ObjectId id)
        {
            try
            {
                return await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving doctor: {ex.Message}", ex);
            }
        }

        public async Task<Doctor> GetByLicenseNumberAsync(string licenseNumber)
        {
            try
            {
                return await _collection.Find(d => d.LicenseNumber == licenseNumber).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving doctor by license: {ex.Message}", ex);
            }
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            try
            {
                return await _collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving all doctors: {ex.Message}", ex);
            }
        }

        public async Task<Doctor> UpdateAsync(Doctor doctor)
        {
            try
            {
                if (!doctor.IsValid())
                    throw new ArgumentException("Doctor data is invalid.");

                doctor.UpdatedAt = DateTime.UtcNow;
                var result = await _collection.ReplaceOneAsync(d => d.Id == doctor.Id, doctor);

                if (result.ModifiedCount == 0)
                    throw new ResourceNotFoundException($"Doctor with ID {doctor.Id} not found.");

                return doctor;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error updating doctor: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(ObjectId id)
        {
            try
            {
                var result = await _collection.DeleteOneAsync(d => d.Id == id);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error deleting doctor: {ex.Message}", ex);
            }
        }

        public async Task<List<Doctor>> FindByFilterAsync(Func<Doctor, bool> filter)
        {
            try
            {
                var allDoctors = await GetAllAsync();
                return allDoctors.Where(filter).ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error filtering doctors: {ex.Message}", ex);
            }
        }

        public async Task<List<Doctor>> GetBySpecializationAsync(string specialization)
        {
            try
            {
                return await _collection.Find(d => d.Specialization == specialization).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving doctors by specialization: {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Repository implementation for Appointment data access.
    /// </summary>
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IMongoCollection<Appointment> _collection;

        public AppointmentRepository(MongoDbContext context)
        {
            _collection = context.Appointments;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            try
            {
                if (!appointment.IsValid())
                    throw new ArgumentException("Appointment data is invalid.");

                await _collection.InsertOneAsync(appointment);
                return appointment;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error adding appointment: {ex.Message}", ex);
            }
        }

        public async Task<Appointment> GetByIdAsync(ObjectId id)
        {
            try
            {
                return await _collection.Find(a => a.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving appointment: {ex.Message}", ex);
            }
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            try
            {
                return await _collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving all appointments: {ex.Message}", ex);
            }
        }

        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
            try
            {
                if (!appointment.IsValid())
                    throw new ArgumentException("Appointment data is invalid.");

                appointment.UpdatedAt = DateTime.UtcNow;
                var result = await _collection.ReplaceOneAsync(a => a.Id == appointment.Id, appointment);

                if (result.ModifiedCount == 0)
                    throw new ResourceNotFoundException($"Appointment with ID {appointment.Id} not found.");

                return appointment;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error updating appointment: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(ObjectId id)
        {
            try
            {
                var result = await _collection.DeleteOneAsync(a => a.Id == id);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error deleting appointment: {ex.Message}", ex);
            }
        }

        public async Task<List<Appointment>> FindByFilterAsync(Func<Appointment, bool> filter)
        {
            try
            {
                var allAppointments = await GetAllAsync();
                return allAppointments.Where(filter).ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error filtering appointments: {ex.Message}", ex);
            }
        }

        public async Task<List<Appointment>> GetAppointmentsForDoctorAsync(ObjectId doctorId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _collection.Find(a =>
                    a.DoctorId == doctorId &&
                    a.StartTime >= startDate &&
                    a.EndTime <= endDate &&
                    a.Status != AppointmentStatus.Cancelled
                ).SortBy(a => a.StartTime).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving doctor appointments: {ex.Message}", ex);
            }
        }

        public async Task<List<Appointment>> GetAppointmentsForPatientAsync(ObjectId patientId)
        {
            try
            {
                return await _collection.Find(a =>
                    a.PatientId == patientId &&
                    a.Status != AppointmentStatus.Cancelled
                ).SortBy(a => a.StartTime).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error retrieving patient appointments: {ex.Message}", ex);
            }
        }

        public async Task<List<Appointment>> GetConflictingAppointmentsAsync(ObjectId doctorId, DateTime startTime, DateTime endTime)
        {
            try
            {
                return await _collection.Find(a =>
                    a.DoctorId == doctorId &&
                    a.Status != AppointmentStatus.Cancelled &&
                    // Check for overlap: existing appointment starts before new appointment ends 
                    // AND existing appointment ends after new appointment starts
                    a.StartTime < endTime &&
                    a.EndTime > startTime
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Error checking conflicting appointments: {ex.Message}", ex);
            }
        }
    }
}
