using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ClinicAppointmentManager.Models
{
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled,
        NoShow
    }

    [BsonIgnoreExtraElements]
    public class Appointment
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("patientId")]
        public ObjectId PatientId { get; set; }

        [BsonElement("doctorId")]
        public ObjectId DoctorId { get; set; }

        [BsonElement("startTime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartTime { get; set; }

        [BsonElement("endTime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndTime { get; set; }

        [BsonElement("reason")]
        public string Reason { get; set; }

        [BsonElement("status")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        [BsonElement("notes")]
        public string Notes { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsValid()
        {
            return PatientId != ObjectId.Empty &&
                   DoctorId != ObjectId.Empty &&
                   StartTime < EndTime &&
                   StartTime > DateTime.Now;
        }
    }
}
