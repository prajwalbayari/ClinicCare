using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ClinicAppointmentManager.Models
{
    /// <summary>
    /// Represents a doctor in the clinic system.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Doctor
    {
        /// <summary>
        /// Unique identifier for the doctor (MongoDB ObjectId).
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Doctor's full name.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Medical specialization (e.g., Cardiology, Dermatology).
        /// </summary>
        [BsonElement("specialization")]
        public string Specialization { get; set; }

        /// <summary>
        /// License number for verification.
        /// </summary>
        [BsonElement("licenseNumber")]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Doctor's email for notifications.
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// Doctor's phone number.
        /// </summary>
        [BsonElement("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Working hours start time (e.g., "09:00").
        /// </summary>
        [BsonElement("workingHoursStart")]
        public string WorkingHoursStart { get; set; } = "09:00";

        /// <summary>
        /// Working hours end time (e.g., "17:00").
        /// </summary>
        [BsonElement("workingHoursEnd")]
        public string WorkingHoursEnd { get; set; } = "17:00";

        /// <summary>
        /// Appointment duration in minutes (typically 30).
        /// </summary>
        [BsonElement("appointmentDurationMinutes")]
        public int AppointmentDurationMinutes { get; set; } = 30;

        /// <summary>
        /// Timestamp when the doctor record was created.
        /// </summary>
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp when the doctor record was last updated.
        /// </summary>
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Validation method to check if doctor data is valid.
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Specialization) &&
                   !string.IsNullOrWhiteSpace(LicenseNumber) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   AppointmentDurationMinutes > 0;
        }

        public override string ToString()
        {
            return $"Dr. {Name}";
        }
    }
}
