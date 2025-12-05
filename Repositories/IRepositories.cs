using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicAppointmentManager.Models;

namespace ClinicAppointmentManager.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddAsync(Patient patient);
        Task<Patient> GetByIdAsync(ObjectId id);
        Task<List<Patient>> GetAllAsync();
        Task<Patient> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(ObjectId id);
    }

    public interface IDoctorRepository
    {
        Task<Doctor> AddAsync(Doctor doctor);
        Task<Doctor> GetByIdAsync(ObjectId id);
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(ObjectId id);
    }

    public interface IAppointmentRepository
    {
        Task<Appointment> AddAsync(Appointment appointment);
        Task<Appointment> GetByIdAsync(ObjectId id);
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(ObjectId id);
        Task<List<Appointment>> FindByFilterAsync(Func<Appointment, bool> filter);
        Task<List<Appointment>> GetAppointmentsForDoctorAsync(ObjectId doctorId, DateTime startDate, DateTime endDate);
        Task<List<Appointment>> GetAppointmentsForPatientAsync(ObjectId patientId);
        Task<List<Appointment>> GetConflictingAppointmentsAsync(ObjectId doctorId, DateTime startTime, DateTime endTime);
    }
}
