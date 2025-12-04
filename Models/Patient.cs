using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ClinicAppointmentManager.Models
{
    /// <summary>
    /// Represents a patient in the clinic system.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Patient
    {
        /// <summary>
        /// Unique identifier for the patient (MongoDB ObjectId).
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Patient's full name.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Patient's age in years.
        /// </summary>
        [BsonElement("age")]
        public int Age { get; set; }

        /// <summary>
        /// Patient's gender (Male, Female, Other).
        /// </summary>
        [BsonElement("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Patient's phone number for reminders.
        /// </summary>
        [BsonElement("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Patient's email address for notifications (optional).
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// Medical history notes - allergies, previous conditions, etc. (optional).
        /// </summary>
        [BsonElement("medicalHistory")]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// Timestamp when the patient record was created.
        /// </summary>
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp when the patient record was last updated.
        /// </summary>
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Validation method to check if patient data is valid.
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Phone) &&
                   !string.IsNullOrWhiteSpace(Gender) &&
                   Age > 0 && Age < 150;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
