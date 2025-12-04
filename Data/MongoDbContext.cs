using MongoDB.Driver;
using MongoDB.Bson;
using System;
using ClinicAppointmentManager.Models;
using ClinicAppointmentManager.Exceptions;

namespace ClinicAppointmentManager.Data
{
    /// <summary>
    /// MongoDB context provider that manages database connections and collections.
    /// TODO: Replace placeholders with your actual MongoDB connection details.
    /// </summary>
    public class MongoDbContext
    {
        // ===== PLACEHOLDER: Configure your MongoDB connection string here =====
        // Option 1: Local MongoDB (ensure 'mongod' is running)
        // private const string MONGO_CONNECTION_STRING = "mongodb://localhost:27017";
        //
        // Option 2: MongoDB Atlas (Cloud)
        // private const string MONGO_CONNECTION_STRING = "mongodb+srv://<username>:<password>@cluster0.xxxxx.mongodb.net/?retryWrites=true&w=majority";
        //mongodb://localhost:27017/
        private const string MONGO_CONNECTION_STRING = "mongodb+srv://udupishreyasbhat_db_user:qQibcWmN8vLcDNEh@cluster0.muyslgj.mongodb.net/";

        // ===== PLACEHOLDER: Set your database name =====
        private const string MONGO_DB_NAME = "appointmentBooking";

        private IMongoClient _client;
        private IMongoDatabase _database;

        /// <summary>
        /// Initializes the MongoDB context with connection string and database name from placeholders.
        /// </summary>
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

                // Verify connection by performing a ping
                var admin = _database.Client.GetDatabase("admin");
                var result = admin.RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                Console.WriteLine("✓ MongoDB connection established successfully.");
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Failed to connect to MongoDB: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the MongoDB collection for patients.
        /// </summary>
        public IMongoCollection<Patient> Patients
        {
            get
            {
                return _database.GetCollection<Patient>("patients");
            }
        }

        /// <summary>
        /// Gets the MongoDB collection for doctors.
        /// </summary>
        public IMongoCollection<Doctor> Doctors
        {
            get
            {
                return _database.GetCollection<Doctor>("doctors");
            }
        }

        /// <summary>
        /// Gets the MongoDB collection for appointments.
        /// </summary>
        public IMongoCollection<Appointment> Appointments
        {
            get
            {
                return _database.GetCollection<Appointment>("appointments");
            }
        }

        /// <summary>
        /// Creates necessary indexes for improved query performance.
        /// </summary>
        public void CreateIndexes()
        {
            try
            {
                // Create index for patient email (unique constraint)
                var patientEmailIndexModel = new CreateIndexModel<Patient>(
                    Builders<Patient>.IndexKeys.Ascending(p => p.Email),
                    new CreateIndexOptions { Unique = true }
                );
                Patients.Indexes.CreateOne(patientEmailIndexModel);

                // Create index for doctor license number (unique constraint)
                var doctorLicenseIndexModel = new CreateIndexModel<Doctor>(
                    Builders<Doctor>.IndexKeys.Ascending(d => d.LicenseNumber),
                    new CreateIndexOptions { Unique = true }
                );
                Doctors.Indexes.CreateOne(doctorLicenseIndexModel);

                // Create indexes for appointment queries
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

        /// <summary>
        /// Drops all collections (use only for testing/cleanup).
        /// </summary>
        public void DropAllCollections()
        {
            try
            {
                _database.DropCollection("patients");
                _database.DropCollection("doctors");
                _database.DropCollection("appointments");
                Console.WriteLine("✓ All collections dropped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Warning: {ex.Message}");
            }
        }
    }
}
