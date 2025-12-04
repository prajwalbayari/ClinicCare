using System;

namespace ClinicAppointmentManager.Exceptions
{
    /// <summary>
    /// Custom exception thrown when a double-booking conflict is detected.
    /// </summary>
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(string message) : base(message) { }
        public DoubleBookingException(string message, Exception innerException) 
            : base(message, innerException) { }
    }

    /// <summary>
    /// Custom exception thrown when an invalid time slot is provided.
    /// </summary>
    public class InvalidTimeSlotException : Exception
    {
        public InvalidTimeSlotException(string message) : base(message) { }
        public InvalidTimeSlotException(string message, Exception innerException) 
            : base(message, innerException) { }
    }

    /// <summary>
    /// Custom exception thrown when a database operation fails.
    /// </summary>
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, Exception innerException) 
            : base(message, innerException) { }
    }

    /// <summary>
    /// Custom exception thrown when a resource is not found.
    /// </summary>
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
