using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ClinicAppointmentManager.Models
{
    /// <summary>
    /// Appointment status enumeration.
    /// </summary>
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled,
        NoShow
    }

    /// <summary>
    /// Represents an appointment between a patient and a doctor.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Appointment
    {
        /// <summary>
        /// Unique identifier for the appointment (MongoDB ObjectId).
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Reference to the patient's ObjectId.
        /// </summary>
        [BsonElement("patientId")]
        public ObjectId PatientId { get; set; }

        /// <summary>
        /// Reference to the doctor's ObjectId.
        /// </summary>
        [BsonElement("doctorId")]
        public ObjectId DoctorId { get; set; }

        /// <summary>
        /// Appointment start date and time.
        /// </summary>
        [BsonElement("startTime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Appointment end date and time.
        /// </summary>
        [BsonElement("endTime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Reason for the appointment (e.g., "Regular checkup", "Follow-up").
        /// </summary>
        [BsonElement("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Current status of the appointment.
        /// </summary>
        [BsonElement("status")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        /// <summary>
        /// Notes about the appointment or consultation.
        /// </summary>
        [BsonElement("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Timestamp when the appointment was created.
        /// </summary>
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp when the appointment was last updated.
        /// </summary>
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Validation method to check if appointment data is valid.
        /// </summary>
        public bool IsValid()
        {
            return PatientId != ObjectId.Empty &&
                   DoctorId != ObjectId.Empty &&
                   StartTime < EndTime &&
                   StartTime > DateTime.Now;
        }

        public override string ToString()
        {
            return $"Appointment: {Id} - Patient: {PatientId}, Doctor: {DoctorId}, {StartTime:yyyy-MM-dd HH:mm}";
        }
    }
}
