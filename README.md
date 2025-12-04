# Clinic Appointment Manager

A comprehensive C# WinForms desktop application for managing patient appointments, doctor schedules, and patient records with MongoDB persistence.

## Features

- **Patient Management**: Register and manage patient records with medical history
- **Doctor Management**: Manage clinic doctors with specializations and working hours
- **Appointment Booking**: Schedule appointments with automatic conflict detection
- **Double-Booking Prevention**: Intelligent algorithm prevents scheduling conflicts
- **Notification System**: Queue and send appointment reminders
- **CSV Export**: Export daily appointment schedules to CSV
- **Sample Data Seeding**: Pre-populate database with sample patients, doctors, and appointments
- **Exception Handling**: Custom exceptions for specific error scenarios
- **File Logging**: Exception and event logging for debugging

## Architecture

The application follows a layered architecture pattern:

```
┌─────────────────────────────────────────────────────────┐
│                    UI Layer (WinForms)                  │
│         MainForm with Tabs & Appointment Booking       │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                    Service Layer                        │
│  SchedulerService | NotificationService | ReportService│
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                 Repository Layer                        │
│  IPatientRepository | IDoctorRepository | IAppointmentRepository
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                Data Layer (MongoDB)                     │
│           MongoDbContext & Collections                 │
└─────────────────────────────────────────────────────────┘
```

## Double-Booking Prevention Algorithm

The `SchedulerService.BookAppointmentAsync()` method implements a robust conflict detection algorithm:

1. **Input Validation**: Validates that the patient and doctor exist in the database
2. **Time Slot Validation**: Ensures the appointment time falls within the doctor's working hours and matches the required duration
3. **Future Date Validation**: Confirms the appointment is scheduled for a future date/time
4. **Conflict Detection**: Queries all non-cancelled appointments for the doctor and checks for overlaps using the mathematical formula: `existing.StartTime < new.EndTime AND existing.EndTime > new.StartTime`. This interval intersection check reliably detects any time overlap.
5. **Atomic Operation**: Only after all validations pass successfully is the appointment inserted into MongoDB
6. **Exception Handling**: If any conflict is detected, a `DoubleBookingException` is thrown with details of existing appointments in that time slot

This approach ensures data integrity and prevents scheduling conflicts at the application level while leveraging MongoDB's ACID transaction support for consistency.

## Project Structure

```
ClinicAppointmentManager/
├── Models/                          # Data models with BSON attributes
│   ├── Patient.cs
│   ├── Doctor.cs
│   └── Appointment.cs
├── Data/                            # MongoDB context
│   └── MongoDbContext.cs
├── Repositories/                    # Data access layer
│   ├── IRepositories.cs             # Repository interfaces
│   └── RepositoryImplementations.cs # Concrete implementations
├── Services/                        # Business logic layer
│   ├── AppointmentServices.cs       # SchedulerService, NotificationService, ReportService
│   └── SampleDataSeeder.cs          # Database seeding
├── Exceptions/                      # Custom exceptions
│   └── CustomExceptions.cs
├── UI/                              # WinForms UI layer
│   ├── MainForm.cs
│   └── MainForm.Designer.cs
├── Program.cs                       # Application entry point
├── ClinicAppointmentManager.csproj  # Project configuration
└── README.md                        # This file
```

## Prerequisites

- .NET 6.0 or later
- MongoDB (local instance or MongoDB Atlas cloud)
- Visual Studio 2022 or Visual Studio Code with C# extension

## Setup Instructions

### 1. Configure MongoDB Connection

Edit `Data/MongoDbContext.cs` and replace the placeholders:

```csharp
// Option 1: Local MongoDB (ensure 'mongod' is running)
private const string MONGO_CONNECTION_STRING = "mongodb://localhost:27017";
private const string MONGO_DB_NAME = "clinic_db";

// Option 2: MongoDB Atlas (Cloud)
private const string MONGO_CONNECTION_STRING = "mongodb+srv://<username>:<password>@cluster0.xxxxx.mongodb.net/?retryWrites=true&w=majority";
private const string MONGO_DB_NAME = "clinic_db";
```

### 2. Local MongoDB Setup (Optional)

If using a local MongoDB instance:

```powershell
# Download and install MongoDB Community Edition from https://www.mongodb.com/try/download/community

# Start MongoDB service (Windows)
mongod

# Or using MongoDB in Docker:
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

### 3. Build and Run

```powershell
# Navigate to project directory
cd ClinicAppointmentManager

# Restore NuGet packages
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

## Saturday Demo Script

Follow these steps to demonstrate the application's key features:

### Step 1: Launch and Seed Data

1. **Run the Application**
   - Execute `dotnet run` from the project directory
   - The MainForm window appears with three tabs: Patients, Doctors, Appointments

2. **Seed Sample Data**
   - Click the **"Seed Sample Data"** button
   - Confirmation dialog appears; click **Yes**
   - System populates the database with:
     - 4 Sample Patients (John Smith, Sarah Johnson, Michael Brown, Emily Davis)
     - 3 Clinic Doctors (Dr. Alice Williams - Cardiology, Dr. Robert Martinez - Dermatology, Dr. Jennifer Lee - General Practice)
     - 4 Pre-scheduled Appointments for today

3. **Verify Data**
   - Switch to **Patients Tab**: See all registered patients listed
   - Switch to **Doctors Tab**: See all doctors with specializations
   - Switch to **Appointments Tab**: See today's scheduled appointments

### Step 2: View Doctor Schedule

1. Select **Dr. Alice Williams** from the doctor dropdown
2. Keep today's date selected
3. Click **"View Schedule"** button
4. A message box displays Dr. Williams' full schedule:
   - Shows working hours (09:00 - 17:00)
   - Lists all appointments with patient names and reasons
   - Example output:
     ```
     === Dr. Alice Williams - Schedule for 2024-12-04 ===
     Specialization: Cardiology
     Total Appointments: 2
     
     09:00 - 09:30: John Smith (Regular checkup)
     10:00 - 10:30: Sarah Johnson (Follow-up consultation)
     ```

### Step 3: Test Double-Booking Prevention (The Critical Test!)

1. **Set Up Conflict Scenario**:
   - Doctor: Select **Dr. Alice Williams**
   - Patient: Select **John Smith**
   - Date: Today's date (same day as existing appointments)
   - Time: **09:15** (this overlaps with his 9:00-9:30 appointment with Sarah)
   - Reason: "Test Appointment"

2. **Attempt to Book**:
   - Click **"Book"** button
   - **CONFLICT DETECTED!** A warning dialog appears:
     ```
     Booking Failed - Double Booking Detected:
     Doctor has conflicting appointments during this time slot: 09:00-09:30
     ```

3. **Demonstrate Successful Booking**:
   - Change time to **11:00** (after existing appointments)
   - Click **"Book"** button
   - Success dialog shows: `Appointment booked successfully! ID: [ObjectId]`
   - The appointment appears in the list below

### Step 4: Export Daily Schedule

1. Keep the current date selected
2. Click **"Export to CSV"** button
3. System creates file: `appointments_2024-12-04.csv`
4. Success message shows file path
5. Open the CSV file to show:
   ```
   Patient,Doctor,Start Time,End Time,Reason,Status,Duration (mins)
   "John Smith","Dr. Alice Williams",2024-12-04 09:00,2024-12-04 09:30,Regular checkup,Scheduled,30
   "Sarah Johnson","Dr. Alice Williams",2024-12-04 10:00,2024-12-04 10:30,Follow-up consultation,Scheduled,30
   ...
   ```

### Step 5: Send Notifications

1. Click **"Send Notifications"** button
2. Notification popups display for queued reminders
3. Notifications are logged to `notifications.log`

### Step 6: Test Error Logging

1. Attempt an invalid booking (e.g., select only doctor, no patient)
2. Click **"Book"** button
3. Validation error appears
4. Exception is logged to `errors.log` for debugging

## Key Classes & Methods

### Models
- **Patient**: Represents a patient with name, contact, date of birth, medical history
- **Doctor**: Represents a doctor with specialization, working hours, appointment duration
- **Appointment**: Represents a scheduled appointment with status tracking

### Services
- **SchedulerService.BookAppointmentAsync()**: Books appointment with conflict detection
- **SchedulerService.GetAvailableTimeSlotsAsync()**: Returns free slots for a doctor on a date
- **NotificationService.QueueReminder()**: Queues appointment reminder
- **ReportService.ExportAppointmentsToCSVAsync()**: Exports appointments to CSV
- **ReportService.GenerateDoctorDaySummaryAsync()**: Generates daily schedule summary

### Repositories
- **IPatientRepository / PatientRepository**: CRUD operations for patients
- **IDoctorRepository / DoctorRepository**: CRUD operations for doctors
- **IAppointmentRepository / AppointmentRepository**: CRUD operations for appointments
- **GetConflictingAppointmentsAsync()**: Critical method for conflict detection

### Custom Exceptions
- **DoubleBookingException**: Thrown when scheduling conflicts detected
- **InvalidTimeSlotException**: Thrown when time slot is invalid
- **DatabaseException**: Thrown for database operation errors
- **ResourceNotFoundException**: Thrown when resource not found

## Error Handling

The application implements comprehensive exception handling:

- **UI Level**: User-friendly message boxes for all errors
- **Service Level**: Custom exceptions for specific business logic failures
- **Repository Level**: Database operation error wrapping
- **File Logging**: All exceptions logged to `errors.log` with timestamps and stack traces

## Logging

- **errors.log**: Application exceptions and errors
- **notifications.log**: Notification queue and delivery logs
- **seeding.log**: Database seeding operation logs

## Testing

Basic unit test skeletons can be added in a `Tests/` folder:

```csharp
// Tests/SchedulerServiceTests.cs
[TestClass]
public class SchedulerServiceTests
{
    [TestMethod]
    public async Task BookAppointment_WithConflict_ThrowsException()
    {
        // Arrange: Create conflicting appointments
        // Act: Attempt to book overlapping appointment
        // Assert: DoubleBookingException thrown
    }

    [TestMethod]
    public async Task BookAppointment_ValidSlot_Succeeds()
    {
        // Arrange: Create valid appointment data
        // Act: Book appointment
        // Assert: Appointment created successfully
    }
}
```

## Troubleshooting

### MongoDB Connection Error
**Error**: "Failed to connect to MongoDB"
- **Solution 1**: Ensure MongoDB is running locally (`mongod`)
- **Solution 2**: Verify connection string in `MongoDbContext.cs`
- **Solution 3**: If using MongoDB Atlas, check username/password and IP whitelist

### Duplicate Key Error
**Error**: "Patient with email already exists"
- **Cause**: Seeding attempted with existing data
- **Solution**: Clear database or use different email addresses

### Collection Not Found
**Error**: "Collection does not exist"
- **Cause**: Database or connection issue
- **Solution**: Verify MongoDB is running and connection string is correct

## Future Enhancements

- Add user authentication and role-based access
- Implement email notifications with SMTP
- Add calendar view for visual scheduling
- Support for recurring appointments
- Patient medical records with document storage
- Payment processing integration
- Advanced reporting and analytics

## License

This project is provided as a demonstration application for educational purposes.

## Support

For issues or questions:
1. Check the logs (`errors.log`, `notifications.log`, `seeding.log`)
2. Verify MongoDB connection and data
3. Review the double-booking algorithm explanation above
4. Ensure all NuGet packages are properly restored

---

**Created**: December 2024  
**Technology Stack**: C#, WinForms, MongoDB, .NET 6.0
