using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ClinicAppointmentManager.Models
{
    [BsonIgnoreExtraElements]
    public class Doctor
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("specialization")]
        public string Specialization { get; set; }

        [BsonElement("licenseNumber")]
        public string LicenseNumber { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("workingHoursStart")]
        public string WorkingHoursStart { get; set; } = "09:00";

        [BsonElement("workingHoursEnd")]
        public string WorkingHoursEnd { get; set; } = "17:00";

        [BsonElement("appointmentDurationMinutes")]
        public int AppointmentDurationMinutes { get; set; } = 30;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Specialization) &&
                   !string.IsNullOrWhiteSpace(LicenseNumber) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   AppointmentDurationMinutes > 0;
        }

        public override string ToString() => $"Dr. {Name}";
    }
}
