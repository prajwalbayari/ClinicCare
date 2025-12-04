using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using ClinicAppointmentManager.Data;
using ClinicAppointmentManager.Models;
using ClinicAppointmentManager.Repositories;
using ClinicAppointmentManager.Services;
using ClinicAppointmentManager.Exceptions;

namespace ClinicAppointmentManager.UI
{
    /// <summary>
    /// Main application form with tabbed interface for managing patients, doctors, and appointments.
    /// </summary>
    public partial class MainForm : Form
    {
        private MongoDbContext _dbContext;
        private List<Patient> _patientsList = new List<Patient>();
        private List<Doctor> _doctorsList = new List<Doctor>();
        private IPatientRepository _patientRepository;
        private IDoctorRepository _doctorRepository;
        private IAppointmentRepository _appointmentRepository;
        private SchedulerService _schedulerService;
        private NotificationService _notificationService;
        private ReportService _reportService;
        private SampleDataSeeder _seeder;

        public MainForm()
        {
            InitializeComponent();
            InitializeServices();
        }

        /// <summary>
        /// Initializes all services and repositories.
        /// </summary>
        private void InitializeServices()
        {
            try
            {
                _dbContext = new MongoDbContext();
                _dbContext.CreateIndexes();

                _patientRepository = new PatientRepository(_dbContext);
                _doctorRepository = new DoctorRepository(_dbContext);
                _appointmentRepository = new AppointmentRepository(_dbContext);

                _schedulerService = new SchedulerService(_appointmentRepository, _doctorRepository, _patientRepository);
                _notificationService = new NotificationService();
                _reportService = new ReportService(_appointmentRepository, _doctorRepository, _patientRepository);
                _seeder = new SampleDataSeeder(_patientRepository, _doctorRepository, _appointmentRepository);

                Text = "Clinic Appointment Manager";
                this.Load += MainForm_Load;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize services: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadPatients();
            LoadDoctors();
            LoadAppointments();
        }

        /// <summary>
        /// Loads and displays all patients in a table format.
        /// </summary>
        private async void LoadPatients()
        {
            try
            {
                _patientsList = (await _patientRepository.GetAllAsync()).ToList();
                
                // Create DataTable for display
                var dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Age", typeof(int));
                dt.Columns.Add("Gender", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Email", typeof(string));

                foreach (var patient in _patientsList)
                {
                    dt.Rows.Add(patient.Name, patient.Age, patient.Gender, patient.Phone, patient.Email ?? "-");
                }

                PatientDataGridView.DataSource = dt;

                // Update ComboBox for appointments
                PatientComboBox.Items.Clear();
                foreach (var patient in _patientsList)
                {
                    PatientComboBox.Items.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Loads and displays all doctors in a table format.
        /// </summary>
        private async void LoadDoctors()
        {
            try
            {
                _doctorsList = (await _doctorRepository.GetAllAsync()).ToList();

                // Create DataTable for display
                var dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Specialization", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Hours", typeof(string));

                foreach (var doctor in _doctorsList)
                {
                    dt.Rows.Add($"Dr. {doctor.Name}", doctor.Specialization, doctor.Phone ?? "-", 
                        doctor.Email, $"{doctor.WorkingHoursStart}-{doctor.WorkingHoursEnd}");
                }

                DoctorDataGridView.DataSource = dt;

                // Update ComboBox for appointments
                DoctorComboBox.Items.Clear();
                foreach (var doctor in _doctorsList)
                {
                    DoctorComboBox.Items.Add(doctor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Loads and displays all appointments in a table format.
        /// </summary>
        private async void LoadAppointments()
        {
            try
            {
                var appointments = await _appointmentRepository.GetAllAsync();

                // Create DataTable for display
                var dt = new DataTable();
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Time", typeof(string));
                dt.Columns.Add("Patient", typeof(string));
                dt.Columns.Add("Doctor", typeof(string));
                dt.Columns.Add("Reason", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                foreach (var appointment in appointments)
                {
                    var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);
                    var doctor = await _doctorRepository.GetByIdAsync(appointment.DoctorId);
                    
                    // Convert UTC to local time for display
                    var startTimeLocal = appointment.StartTime.ToLocalTime();
                    var endTimeLocal = appointment.EndTime.ToLocalTime();
                    
                    dt.Rows.Add(
                        startTimeLocal.ToString("dd-MMM-yyyy"),
                        $"{startTimeLocal:hh:mm tt} - {endTimeLocal:hh:mm tt}",
                        patient?.Name ?? "Unknown",
                        doctor != null ? $"Dr. {doctor.Name}" : "Unknown",
                        appointment.Reason,
                        appointment.Status
                    );
                }

                AppointmentDataGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles booking a new appointment.
        /// </summary>
        private async void BookButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DoctorComboBox.SelectedItem == null || PatientComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a doctor and patient.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var doctor = (Doctor)DoctorComboBox.SelectedItem;
                var patient = (Patient)PatientComboBox.SelectedItem;
                var appointmentDate = AppointmentDatePicker.Value.Date;
                var startTime = appointmentDate.Add(StartTimePicker.Value.TimeOfDay);
                var endTime = startTime.AddMinutes(doctor.AppointmentDurationMinutes);
                var reason = ReasonTextBox.Text;

                if (string.IsNullOrWhiteSpace(reason))
                {
                    MessageBox.Show("Please enter a reason for the appointment.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var appointment = await _schedulerService.BookAppointmentAsync(patient.Id, doctor.Id, startTime, endTime, reason);

                MessageBox.Show($"Appointment booked successfully!\nID: {appointment.Id}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Queue a notification
                _notificationService.QueueReminder(appointment, patient, doctor);

                LoadAppointments();
                ClearAppointmentFields();
            }
            catch (DoubleBookingException ex)
            {
                MessageBox.Show($"Booking Failed - Double Booking Detected:\n{ex.Message}", "Conflict",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogException(ex);
            }
            catch (InvalidTimeSlotException ex)
            {
                MessageBox.Show($"Booking Failed - Invalid Time Slot:\n{ex.Message}", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogException(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles cancelling an appointment.
        /// </summary>
        private async void AppointmentCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppointmentDataGridView.SelectedRows.Count < 1)
                {
                    MessageBox.Show("Please select an appointment to cancel.", "Selection Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIndex = AppointmentDataGridView.SelectedRows[0].Index;
                var appointments = (await _appointmentRepository.GetAllAsync()).ToList();
                if (selectedIndex >= appointments.Count)
                {
                    MessageBox.Show("Invalid selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var appointment = appointments[selectedIndex];

                var result = MessageBox.Show("Are you sure you want to cancel this appointment?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await _schedulerService.CancelAppointmentAsync(appointment.Id);
                    MessageBox.Show("Appointment cancelled successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles exporting appointments to CSV.
        /// </summary>
        private async void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                var date = AppointmentDatePicker.Value.Date;
                var filePath = await _reportService.ExportAppointmentsToCSVAsync(date);
                MessageBox.Show($"Appointments exported to:\n{filePath}", "Export Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting appointments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles viewing doctor schedule.
        /// </summary>
        private async void ViewScheduleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DoctorComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a doctor.", "Selection Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var doctor = (Doctor)DoctorComboBox.SelectedItem;
                var date = AppointmentDatePicker.Value.Date;
                var summary = await _reportService.GenerateDoctorDaySummaryAsync(doctor.Id, date);

                MessageBox.Show(summary, $"Dr. {doctor.Name} - Schedule",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing schedule: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles sending queued notifications.
        /// </summary>
        private void SendNotificationsButton_Click(object sender, EventArgs e)
        {
            try
            {
                _notificationService.SendAllNotifications();
                MessageBox.Show("All notifications sent.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending notifications: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        private void ClearAppointmentFields()
        {
            ReasonTextBox.Clear();
            AppointmentDatePicker.Value = DateTime.Now.AddDays(1);
            DoctorComboBox.SelectedIndex = -1;
            PatientComboBox.SelectedIndex = -1;
            StartTimePicker.Value = DateTime.Now;
        }

        /// <summary>
        /// Handles adding a new patient to the database.
        /// </summary>
        private async void AddPatientButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields (Name, Age, Gender, Phone)
                if (string.IsNullOrWhiteSpace(PatientNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PatientPhoneTextBox.Text) ||
                    PatientGenderComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Please fill in all required fields (Name, Age, Gender, Phone).", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (PatientAgeNumeric.Value < 1 || PatientAgeNumeric.Value > 120)
                {
                    MessageBox.Show("Please enter a valid age (1-120).", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate phone number (must be exactly 10 digits)
                string phone = PatientPhoneTextBox.Text.Trim();
                if (!IsValidPhoneNumber(phone))
                {
                    MessageBox.Show("Phone number must be exactly 10 digits.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate email format (required)
                string email = PatientEmailTextBox.Text.Trim();
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Email is required.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address (e.g., name@gmail.com, name@hotmail.com).", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var patient = new Patient
                {
                    Name = PatientNameTextBox.Text.Trim(),
                    Age = (int)PatientAgeNumeric.Value,
                    Gender = PatientGenderComboBox.SelectedItem.ToString(),
                    Phone = phone,
                    Email = email,  // Optional
                    MedicalHistory = PatientHistoryTextBox.Text.Trim(),  // Optional
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _patientRepository.AddAsync(patient);

                MessageBox.Show($"Patient '{patient.Name}' registered successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearPatientForm();
                LoadPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding patient: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles adding a new doctor to the database.
        /// </summary>
        private async void AddDoctorButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(DoctorNameTextBox.Text) ||
                    DoctorSpecComboBox.SelectedIndex < 0 ||
                    string.IsNullOrWhiteSpace(DoctorLicenseTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DoctorEmailTextBox.Text))
                {
                    MessageBox.Show("Please fill in all required fields (Name, Specialization, License, Email).", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate email format
                string email = DoctorEmailTextBox.Text.Trim();
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address (e.g., name@gmail.com, name@hotmail.com).", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate phone number if provided (must be exactly 10 digits)
                string phone = DoctorPhoneTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(phone) && !IsValidPhoneNumber(phone))
                {
                    MessageBox.Show("Phone number must be exactly 10 digits.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate working hours format
                if (!IsValidTimeFormat(DoctorStartHoursTextBox.Text) || !IsValidTimeFormat(DoctorEndHoursTextBox.Text))
                {
                    MessageBox.Show("Working hours must be in HH:mm format (e.g., 09:00).", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var doctor = new Doctor
                {
                    Name = DoctorNameTextBox.Text.Trim(),
                    Specialization = DoctorSpecComboBox.SelectedItem.ToString(),
                    LicenseNumber = DoctorLicenseTextBox.Text.Trim(),
                    Email = email,
                    Phone = phone,
                    WorkingHoursStart = DoctorStartHoursTextBox.Text.Trim(),
                    WorkingHoursEnd = DoctorEndHoursTextBox.Text.Trim(),
                    AppointmentDurationMinutes = (int)DoctorDurationNumeric.Value,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _doctorRepository.AddAsync(doctor);

                MessageBox.Show($"Dr. {doctor.Name} ({doctor.Specialization}) registered successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearDoctorForm();
                LoadDoctors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding doctor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Validates phone number (must be exactly 10 digits).
        /// </summary>
        private bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{10}$");
        }

        /// <summary>
        /// Validates email format (e.g., name@gmail.com, name@hotmail.com, etc.).
        /// </summary>
        private bool IsValidEmail(string email)
        {
            // Pattern matches: something@domain.extension
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        /// <summary>
        /// Validates time format (HH:mm).
        /// </summary>
        private bool IsValidTimeFormat(string time)
        {
            return TimeSpan.TryParse(time, out _);
        }

        /// <summary>
        /// Clears the patient registration form.
        /// </summary>
        private void ClearPatientForm()
        {
            PatientNameTextBox.Clear();
            PatientAgeNumeric.Value = 25;
            PatientGenderComboBox.SelectedIndex = -1;
            PatientPhoneTextBox.Clear();
            PatientEmailTextBox.Clear();
            PatientHistoryTextBox.Clear();
        }

        /// <summary>
        /// Clears the doctor registration form.
        /// </summary>
        private void ClearDoctorForm()
        {
            DoctorNameTextBox.Clear();
            DoctorSpecComboBox.SelectedIndex = -1;
            DoctorLicenseTextBox.Clear();
            DoctorEmailTextBox.Clear();
            DoctorPhoneTextBox.Clear();
            DoctorStartHoursTextBox.Text = "09:00";
            DoctorEndHoursTextBox.Text = "17:00";
            DoctorDurationNumeric.Value = 30;
        }

        /// <summary>
        /// Logs exceptions to a file for debugging.
        /// </summary>
        private void LogException(Exception ex)
        {
            try
            {
                var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Exception: {ex.GetType().Name} - {ex.Message}\n{ex.StackTrace}\n";
                System.IO.File.AppendAllText("errors.log", logMessage);
            }
            catch { }
        }

        /// <summary>
        /// Handles deleting the selected patient.
        /// </summary>
        private async void DeletePatientButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PatientDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a patient to delete.", "Selection Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIndex = PatientDataGridView.SelectedRows[0].Index;
                if (selectedIndex >= _patientsList.Count)
                {
                    MessageBox.Show("Invalid selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var patient = _patientsList[selectedIndex];

                // Check for related appointments
                var patientAppointments = await _appointmentRepository.GetAppointmentsForPatientAsync(patient.Id);
                var appointmentCount = patientAppointments.Count;

                var message = $"Are you sure you want to delete patient '{patient.Name}'?";
                if (appointmentCount > 0)
                {
                    message += $"\n\n⚠️ WARNING: This will also delete {appointmentCount} appointment(s) associated with this patient.";
                }
                message += "\n\nThis action cannot be undone.";

                var result = MessageBox.Show(
                    message,
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Delete all related appointments first
                    foreach (var appointment in patientAppointments)
                    {
                        await _appointmentRepository.DeleteAsync(appointment.Id);
                    }

                    await _patientRepository.DeleteAsync(patient.Id);
                    
                    var successMsg = $"Patient '{patient.Name}' deleted successfully.";
                    if (appointmentCount > 0)
                    {
                        successMsg += $"\n{appointmentCount} related appointment(s) were also removed.";
                    }
                    MessageBox.Show(successMsg, "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPatients();
                    LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting patient: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles deleting the selected doctor.
        /// </summary>
        private async void DeleteDoctorButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DoctorDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a doctor to delete.", "Selection Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIndex = DoctorDataGridView.SelectedRows[0].Index;
                if (selectedIndex >= _doctorsList.Count)
                {
                    MessageBox.Show("Invalid selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var doctor = _doctorsList[selectedIndex];

                // Check for related appointments
                var doctorAppointments = await _appointmentRepository.GetAppointmentsForDoctorAsync(doctor.Id, DateTime.MinValue, DateTime.MaxValue);
                var appointmentCount = doctorAppointments.Count;

                var message = $"Are you sure you want to delete Dr. {doctor.Name}?";
                if (appointmentCount > 0)
                {
                    message += $"\n\n⚠️ WARNING: This will also delete {appointmentCount} appointment(s) associated with this doctor.";
                }
                message += "\n\nThis action cannot be undone.";

                var result = MessageBox.Show(
                    message,
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Delete all related appointments first
                    foreach (var appointment in doctorAppointments)
                    {
                        await _appointmentRepository.DeleteAsync(appointment.Id);
                    }

                    await _doctorRepository.DeleteAsync(doctor.Id);
                    
                    var successMsg = $"Dr. {doctor.Name} deleted successfully.";
                    if (appointmentCount > 0)
                    {
                        successMsg += $"\n{appointmentCount} related appointment(s) were also removed.";
                    }
                    MessageBox.Show(successMsg, "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDoctors();
                    LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting doctor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }

        /// <summary>
        /// Handles deleting the selected appointment.
        /// </summary>
        private async void DeleteAppointmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppointmentDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an appointment to delete.", "Selection Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIndex = AppointmentDataGridView.SelectedRows[0].Index;
                var appointments = (await _appointmentRepository.GetAllAsync()).ToList();
                
                if (selectedIndex >= appointments.Count)
                {
                    MessageBox.Show("Invalid selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var appointment = appointments[selectedIndex];
                var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);
                var doctor = await _doctorRepository.GetByIdAsync(appointment.DoctorId);

                var result = MessageBox.Show(
                    $"Are you sure you want to delete this appointment?\n\n" +
                    $"Patient: {patient?.Name ?? "Unknown"}\n" +
                    $"Doctor: Dr. {doctor?.Name ?? "Unknown"}\n" +
                    $"Date: {appointment.StartTime.ToLocalTime():dd-MMM-yyyy hh:mm tt}\n\n" +
                    "This action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    await _appointmentRepository.DeleteAsync(appointment.Id);
                    MessageBox.Show("Appointment deleted successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogException(ex);
            }
        }
    }
}
