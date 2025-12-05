using System;

namespace ClinicAppointmentManager.Exceptions
{
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(string message) : base(message) { }
        public DoubleBookingException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidTimeSlotException : Exception
    {
        public InvalidTimeSlotException(string message) : base(message) { }
        public InvalidTimeSlotException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
