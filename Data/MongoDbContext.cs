using MongoDB.Driver;
using MongoDB.Bson;
using System;
using ClinicAppointmentManager.Models;
using ClinicAppointmentManager.Exceptions;

namespace ClinicAppointmentManager.Data
{
    public class MongoDbContext
    {
        private const string MONGO_CONNECTION_STRING = "mongodb+srv://udupishreyasbhat_db_user:qQibcWmN8vLcDNEh@cluster0.muyslgj.mongodb.net/";
        private const string MONGO_DB_NAME = "appointmentBooking";

        private IMongoClient _client;
        private IMongoDatabase _database;

        public MongoDbContext()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MONGO_CONNECTION_STRING) ||
                    string.IsNullOrWhiteSpace(MONGO_DB_NAME))
                {
                    throw new DatabaseException(
                        "MongoDB configuration not set. Please configure MONGO_CONNECTION_STRING and MONGO_DB_NAME in MongoDbContext.cs");
                }

                _client = new MongoClient(MONGO_CONNECTION_STRING);
                _database = _client.GetDatabase(MONGO_DB_NAME);

                var admin = _database.Client.GetDatabase("admin");
                var result = admin.RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                Console.WriteLine("✓ MongoDB connection established successfully.");
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Failed to connect to MongoDB: {ex.Message}", ex);
            }
        }

        public IMongoCollection<Patient> Patients => _database.GetCollection<Patient>("patients");

        public IMongoCollection<Doctor> Doctors => _database.GetCollection<Doctor>("doctors");

        public IMongoCollection<Appointment> Appointments => _database.GetCollection<Appointment>("appointments");

        public void CreateIndexes()
        {
            try
            {
                var patientEmailIndexModel = new CreateIndexModel<Patient>(
                    Builders<Patient>.IndexKeys.Ascending(p => p.Email),
                    new CreateIndexOptions { Unique = true }
                );
                Patients.Indexes.CreateOne(patientEmailIndexModel);

                var doctorLicenseIndexModel = new CreateIndexModel<Doctor>(
                    Builders<Doctor>.IndexKeys.Ascending(d => d.LicenseNumber),
                    new CreateIndexOptions { Unique = true }
                );
                Doctors.Indexes.CreateOne(doctorLicenseIndexModel);

                var appointmentDoctorDateIndexModel = new CreateIndexModel<Appointment>(
                    Builders<Appointment>.IndexKeys
                        .Ascending(a => a.DoctorId)
                        .Ascending(a => a.StartTime)
                );
                Appointments.Indexes.CreateOne(appointmentDoctorDateIndexModel);

                var appointmentPatientIndexModel = new CreateIndexModel<Appointment>(
                    Builders<Appointment>.IndexKeys.Ascending(a => a.PatientId)
                );
                Appointments.Indexes.CreateOne(appointmentPatientIndexModel);

                Console.WriteLine("✓ Database indexes created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Warning: Could not create indexes: {ex.Message}");
            }
        }
    }
}
