namespace ClinicAppointmentManager.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.PatientsTab = new System.Windows.Forms.TabPage();
            this.PatientDataGridView = new System.Windows.Forms.DataGridView();
            this.PatientsLabel = new System.Windows.Forms.Label();

            this.DoctorsTab = new System.Windows.Forms.TabPage();
            this.DoctorDataGridView = new System.Windows.Forms.DataGridView();
            this.DoctorsLabel = new System.Windows.Forms.Label();

            this.AppointmentsTab = new System.Windows.Forms.TabPage();
            this.AppointmentDataGridView = new System.Windows.Forms.DataGridView();
            this.AppointmentLabel = new System.Windows.Forms.Label();
            this.BookingGroupBox = new System.Windows.Forms.GroupBox();

            this.DoctorLabel = new System.Windows.Forms.Label();
            this.DoctorComboBox = new System.Windows.Forms.ComboBox();

            this.PatientLabel = new System.Windows.Forms.Label();
            this.PatientComboBox = new System.Windows.Forms.ComboBox();

            this.DateLabel = new System.Windows.Forms.Label();
            this.AppointmentDatePicker = new System.Windows.Forms.DateTimePicker();

            this.TimeLabel = new System.Windows.Forms.Label();
            this.StartTimePicker = new System.Windows.Forms.DateTimePicker();

            this.ReasonLabel = new System.Windows.Forms.Label();
            this.ReasonTextBox = new System.Windows.Forms.TextBox();

            this.BookButton = new System.Windows.Forms.Button();
            this.AppointmentCancelButton = new System.Windows.Forms.Button();

            this.ToolsGroupBox = new System.Windows.Forms.GroupBox();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ViewScheduleButton = new System.Windows.Forms.Button();
            this.SendNotificationsButton = new System.Windows.Forms.Button();
            this.DeletePatientButton = new System.Windows.Forms.Button();
            this.EditPatientButton = new System.Windows.Forms.Button();
            this.DeleteDoctorButton = new System.Windows.Forms.Button();
            this.EditDoctorButton = new System.Windows.Forms.Button();
            this.DeleteAppointmentButton = new System.Windows.Forms.Button();
            this.EditAppointmentButton = new System.Windows.Forms.Button();
            this.AppointmentActionsGroupBox = new System.Windows.Forms.GroupBox();

            this.MainTabControl.SuspendLayout();
            this.PatientsTab.SuspendLayout();
            this.DoctorsTab.SuspendLayout();
            this.AppointmentsTab.SuspendLayout();
            this.BookingGroupBox.SuspendLayout();
            this.ToolsGroupBox.SuspendLayout();
            this.SuspendLayout();

            // Patient Form Controls
            this.PatientFormGroupBox = new System.Windows.Forms.GroupBox();
            this.PatientNameLabel = new System.Windows.Forms.Label();
            this.PatientNameTextBox = new System.Windows.Forms.TextBox();
            this.PatientAgeLabel = new System.Windows.Forms.Label();
            this.PatientAgeNumeric = new System.Windows.Forms.NumericUpDown();
            this.PatientGenderLabel = new System.Windows.Forms.Label();
            this.PatientGenderComboBox = new System.Windows.Forms.ComboBox();
            this.PatientPhoneLabel = new System.Windows.Forms.Label();
            this.PatientPhoneTextBox = new System.Windows.Forms.TextBox();
            this.PatientEmailLabel = new System.Windows.Forms.Label();
            this.PatientEmailTextBox = new System.Windows.Forms.TextBox();
            this.PatientHistoryLabel = new System.Windows.Forms.Label();
            this.PatientHistoryTextBox = new System.Windows.Forms.TextBox();
            this.AddPatientButton = new System.Windows.Forms.Button();

            // Doctor Form Controls
            this.DoctorFormGroupBox = new System.Windows.Forms.GroupBox();
            this.DoctorNameLabel = new System.Windows.Forms.Label();
            this.DoctorNameTextBox = new System.Windows.Forms.TextBox();
            this.DoctorSpecLabel = new System.Windows.Forms.Label();
            this.DoctorSpecComboBox = new System.Windows.Forms.ComboBox();
            this.DoctorLicenseLabel = new System.Windows.Forms.Label();
            this.DoctorLicenseTextBox = new System.Windows.Forms.TextBox();
            this.DoctorEmailLabel = new System.Windows.Forms.Label();
            this.DoctorEmailTextBox = new System.Windows.Forms.TextBox();
            this.DoctorPhoneLabel = new System.Windows.Forms.Label();
            this.DoctorPhoneTextBox = new System.Windows.Forms.TextBox();
            this.DoctorStartHoursLabel = new System.Windows.Forms.Label();
            this.DoctorStartHoursTextBox = new System.Windows.Forms.TextBox();
            this.DoctorEndHoursLabel = new System.Windows.Forms.Label();
            this.DoctorEndHoursTextBox = new System.Windows.Forms.TextBox();
            this.DoctorDurationLabel = new System.Windows.Forms.Label();
            this.DoctorDurationNumeric = new System.Windows.Forms.NumericUpDown();
            this.AddDoctorButton = new System.Windows.Forms.Button();

            this.PatientFormGroupBox.SuspendLayout();
            this.DoctorFormGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorDurationNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientAgeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).BeginInit();

            // MainTabControl
            this.MainTabControl.Controls.Add(this.PatientsTab);
            this.MainTabControl.Controls.Add(this.DoctorsTab);
            this.MainTabControl.Controls.Add(this.AppointmentsTab);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(1084, 611);
            this.MainTabControl.TabIndex = 0;
            this.MainTabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.MainTabControl.Padding = new System.Drawing.Point(20, 5);

            // PatientsTab
            this.PatientsTab.Controls.Add(this.EditPatientButton);
            this.PatientsTab.Controls.Add(this.DeletePatientButton);
            this.PatientsTab.Controls.Add(this.PatientFormGroupBox);
            this.PatientsTab.Controls.Add(this.PatientsLabel);
            this.PatientsTab.Controls.Add(this.PatientDataGridView);
            this.PatientsTab.Location = new System.Drawing.Point(4, 29);
            this.PatientsTab.Name = "PatientsTab";
            this.PatientsTab.Padding = new System.Windows.Forms.Padding(15);
            this.PatientsTab.Size = new System.Drawing.Size(1076, 578);
            this.PatientsTab.TabIndex = 0;
            this.PatientsTab.Text = "👤 Patients";
            this.PatientsTab.UseVisualStyleBackColor = true;
            this.PatientsTab.BackColor = System.Drawing.Color.FromArgb(250, 250, 252);

            this.PatientsLabel.AutoSize = true;
            this.PatientsLabel.Location = new System.Drawing.Point(15, 15);
            this.PatientsLabel.Name = "PatientsLabel";
            this.PatientsLabel.Size = new System.Drawing.Size(80, 15);
            this.PatientsLabel.TabIndex = 0;
            this.PatientsLabel.Text = "📋 Registered Patients";
            this.PatientsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.PatientsLabel.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);

            this.PatientDataGridView.Location = new System.Drawing.Point(15, 45);
            this.PatientDataGridView.Name = "PatientDataGridView";
            this.PatientDataGridView.Size = new System.Drawing.Size(530, 480);
            this.PatientDataGridView.TabIndex = 1;
            this.PatientDataGridView.ReadOnly = true;
            this.PatientDataGridView.AllowUserToAddRows = false;
            this.PatientDataGridView.AllowUserToDeleteRows = false;
            this.PatientDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PatientDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PatientDataGridView.ColumnHeadersVisible = true;
            this.PatientDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientDataGridView.ColumnHeadersHeight = 35;
            this.PatientDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.PatientDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PatientDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.PatientDataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.PatientDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.PatientDataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.PatientDataGridView.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.PatientDataGridView.EnableHeadersVisualStyles = false;
            this.PatientDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 249, 252);
            this.PatientDataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(200, 230, 245);
            this.PatientDataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.PatientDataGridView.RowTemplate.Height = 30;
            this.PatientDataGridView.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            // PatientFormGroupBox
            this.PatientFormGroupBox.Controls.Add(this.PatientNameLabel);
            this.PatientFormGroupBox.Controls.Add(this.PatientNameTextBox);
            this.PatientFormGroupBox.Controls.Add(this.PatientAgeLabel);
            this.PatientFormGroupBox.Controls.Add(this.PatientAgeNumeric);
            this.PatientFormGroupBox.Controls.Add(this.PatientGenderLabel);
            this.PatientFormGroupBox.Controls.Add(this.PatientGenderComboBox);
            this.PatientFormGroupBox.Controls.Add(this.PatientPhoneLabel);
            this.PatientFormGroupBox.Controls.Add(this.PatientPhoneTextBox);
            this.PatientFormGroupBox.Controls.Add(this.PatientEmailLabel);
            this.PatientFormGroupBox.Controls.Add(this.PatientEmailTextBox);
            this.PatientFormGroupBox.Controls.Add(this.PatientHistoryLabel);
            this.PatientFormGroupBox.Controls.Add(this.PatientHistoryTextBox);
            this.PatientFormGroupBox.Controls.Add(this.AddPatientButton);
            this.PatientFormGroupBox.Location = new System.Drawing.Point(560, 15);
            this.PatientFormGroupBox.Name = "PatientFormGroupBox";
            this.PatientFormGroupBox.Size = new System.Drawing.Size(500, 250);
            this.PatientFormGroupBox.TabIndex = 2;
            this.PatientFormGroupBox.TabStop = false;
            this.PatientFormGroupBox.Text = "➕ Register New Patient";
            this.PatientFormGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.PatientFormGroupBox.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.PatientFormGroupBox.BackColor = System.Drawing.Color.White;

            this.PatientNameLabel.AutoSize = true;
            this.PatientNameLabel.Location = new System.Drawing.Point(15, 35);
            this.PatientNameLabel.Name = "PatientNameLabel";
            this.PatientNameLabel.Text = "Full Name*:";
            this.PatientNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientNameLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.PatientNameTextBox.Location = new System.Drawing.Point(100, 32);
            this.PatientNameTextBox.Name = "PatientNameTextBox";
            this.PatientNameTextBox.Size = new System.Drawing.Size(140, 23);
            this.PatientNameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.PatientAgeLabel.AutoSize = true;
            this.PatientAgeLabel.Location = new System.Drawing.Point(260, 35);
            this.PatientAgeLabel.Name = "PatientAgeLabel";
            this.PatientAgeLabel.Text = "Age*:";
            this.PatientAgeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientAgeLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.PatientAgeNumeric.Location = new System.Drawing.Point(300, 32);
            this.PatientAgeNumeric.Name = "PatientAgeNumeric";
            this.PatientAgeNumeric.Size = new System.Drawing.Size(60, 23);
            this.PatientAgeNumeric.Minimum = 1;
            this.PatientAgeNumeric.Maximum = 120;
            this.PatientAgeNumeric.Value = 25;
            this.PatientAgeNumeric.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.PatientGenderLabel.AutoSize = true;
            this.PatientGenderLabel.Location = new System.Drawing.Point(380, 35);
            this.PatientGenderLabel.Name = "PatientGenderLabel";
            this.PatientGenderLabel.Text = "Gender*:";
            this.PatientGenderLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientGenderLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.PatientGenderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PatientGenderComboBox.Location = new System.Drawing.Point(440, 32);
            this.PatientGenderComboBox.Name = "PatientGenderComboBox";
            this.PatientGenderComboBox.Size = new System.Drawing.Size(45, 23);
            this.PatientGenderComboBox.Items.AddRange(new object[] { "M", "F", "Other" });
            this.PatientGenderComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.PatientPhoneLabel.AutoSize = true;
            this.PatientPhoneLabel.Location = new System.Drawing.Point(15, 70);
            this.PatientPhoneLabel.Name = "PatientPhoneLabel";
            this.PatientPhoneLabel.Text = "Phone*:";
            this.PatientPhoneLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientPhoneLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.PatientPhoneTextBox.Location = new System.Drawing.Point(100, 67);
            this.PatientPhoneTextBox.Name = "PatientPhoneTextBox";
            this.PatientPhoneTextBox.Size = new System.Drawing.Size(140, 23);
            this.PatientPhoneTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.PatientEmailLabel.AutoSize = true;
            this.PatientEmailLabel.Location = new System.Drawing.Point(260, 70);
            this.PatientEmailLabel.Name = "PatientEmailLabel";
            this.PatientEmailLabel.Text = "Email*:";
            this.PatientEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientEmailLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.PatientEmailTextBox.Location = new System.Drawing.Point(300, 67);
            this.PatientEmailTextBox.Name = "PatientEmailTextBox";
            this.PatientEmailTextBox.Size = new System.Drawing.Size(185, 23);
            this.PatientEmailTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.PatientHistoryLabel.AutoSize = true;
            this.PatientHistoryLabel.Location = new System.Drawing.Point(15, 105);
            this.PatientHistoryLabel.Name = "PatientHistoryLabel";
            this.PatientHistoryLabel.Text = "Medical History:";
            this.PatientHistoryLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientHistoryLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.PatientHistoryTextBox.Location = new System.Drawing.Point(120, 102);
            this.PatientHistoryTextBox.Name = "PatientHistoryTextBox";
            this.PatientHistoryTextBox.Size = new System.Drawing.Size(365, 60);
            this.PatientHistoryTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientHistoryTextBox.Multiline = true;

            this.AddPatientButton.Location = new System.Drawing.Point(350, 175);
            this.AddPatientButton.Name = "AddPatientButton";
            this.AddPatientButton.Size = new System.Drawing.Size(135, 35);
            this.AddPatientButton.Text = "➕ Add Patient";
            this.AddPatientButton.UseVisualStyleBackColor = false;
            this.AddPatientButton.BackColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.AddPatientButton.ForeColor = System.Drawing.Color.White;
            this.AddPatientButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPatientButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.AddPatientButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddPatientButton.Click += new System.EventHandler(this.AddPatientButton_Click);

            // EditPatientButton
            this.EditPatientButton.Location = new System.Drawing.Point(15, 535);
            this.EditPatientButton.Name = "EditPatientButton";
            this.EditPatientButton.Size = new System.Drawing.Size(130, 35);
            this.EditPatientButton.Text = "✏️ Edit";
            this.EditPatientButton.UseVisualStyleBackColor = false;
            this.EditPatientButton.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.EditPatientButton.ForeColor = System.Drawing.Color.White;
            this.EditPatientButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditPatientButton.FlatAppearance.BorderSize = 0;
            this.EditPatientButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.EditPatientButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditPatientButton.Click += new System.EventHandler(this.EditPatientButton_Click);

            // DeletePatientButton
            this.DeletePatientButton.Location = new System.Drawing.Point(155, 535);
            this.DeletePatientButton.Name = "DeletePatientButton";
            this.DeletePatientButton.Size = new System.Drawing.Size(130, 35);
            this.DeletePatientButton.Text = "🗑️ Delete";
            this.DeletePatientButton.UseVisualStyleBackColor = false;
            this.DeletePatientButton.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.DeletePatientButton.ForeColor = System.Drawing.Color.White;
            this.DeletePatientButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeletePatientButton.FlatAppearance.BorderSize = 0;
            this.DeletePatientButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.DeletePatientButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeletePatientButton.Click += new System.EventHandler(this.DeletePatientButton_Click);

            // DoctorsTab
            this.DoctorsTab.Controls.Add(this.EditDoctorButton);
            this.DoctorsTab.Controls.Add(this.DeleteDoctorButton);
            this.DoctorsTab.Controls.Add(this.DoctorFormGroupBox);
            this.DoctorsTab.Controls.Add(this.DoctorsLabel);
            this.DoctorsTab.Controls.Add(this.DoctorDataGridView);
            this.DoctorsTab.Location = new System.Drawing.Point(4, 29);
            this.DoctorsTab.Name = "DoctorsTab";
            this.DoctorsTab.Padding = new System.Windows.Forms.Padding(15);
            this.DoctorsTab.Size = new System.Drawing.Size(1076, 578);
            this.DoctorsTab.TabIndex = 1;
            this.DoctorsTab.Text = "🧑‍⚕️ Doctors";
            this.DoctorsTab.UseVisualStyleBackColor = true;
            this.DoctorsTab.BackColor = System.Drawing.Color.FromArgb(250, 250, 252);

            this.DoctorsLabel.AutoSize = true;
            this.DoctorsLabel.Location = new System.Drawing.Point(15, 15);
            this.DoctorsLabel.Name = "DoctorsLabel";
            this.DoctorsLabel.Size = new System.Drawing.Size(69, 15);
            this.DoctorsLabel.TabIndex = 0;
            this.DoctorsLabel.Text = "🩺 Clinic Doctors";
            this.DoctorsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.DoctorsLabel.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);

            this.DoctorDataGridView.Location = new System.Drawing.Point(15, 45);
            this.DoctorDataGridView.Name = "DoctorDataGridView";
            this.DoctorDataGridView.Size = new System.Drawing.Size(530, 480);
            this.DoctorDataGridView.TabIndex = 1;
            this.DoctorDataGridView.ReadOnly = true;
            this.DoctorDataGridView.AllowUserToAddRows = false;
            this.DoctorDataGridView.AllowUserToDeleteRows = false;
            this.DoctorDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DoctorDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DoctorDataGridView.ColumnHeadersVisible = true;
            this.DoctorDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DoctorDataGridView.ColumnHeadersHeight = 35;
            this.DoctorDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DoctorDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DoctorDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DoctorDataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.DoctorDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.DoctorDataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.DoctorDataGridView.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.DoctorDataGridView.EnableHeadersVisualStyles = false;
            this.DoctorDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 249, 252);
            this.DoctorDataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(200, 230, 245);
            this.DoctorDataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.DoctorDataGridView.RowTemplate.Height = 30;
            this.DoctorDataGridView.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            // DoctorFormGroupBox
            this.DoctorFormGroupBox.Controls.Add(this.DoctorNameLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorNameTextBox);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorSpecLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorSpecComboBox);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorLicenseLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorLicenseTextBox);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorEmailLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorEmailTextBox);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorPhoneLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorPhoneTextBox);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorStartHoursLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorStartHoursTextBox);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorEndHoursLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorEndHoursTextBox);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorDurationLabel);
            this.DoctorFormGroupBox.Controls.Add(this.DoctorDurationNumeric);
            this.DoctorFormGroupBox.Controls.Add(this.AddDoctorButton);
            this.DoctorFormGroupBox.Location = new System.Drawing.Point(560, 15);
            this.DoctorFormGroupBox.Name = "DoctorFormGroupBox";
            this.DoctorFormGroupBox.Size = new System.Drawing.Size(500, 260);
            this.DoctorFormGroupBox.TabIndex = 2;
            this.DoctorFormGroupBox.TabStop = false;
            this.DoctorFormGroupBox.Text = "➕ Register New Doctor";
            this.DoctorFormGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.DoctorFormGroupBox.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.DoctorFormGroupBox.BackColor = System.Drawing.Color.White;

            this.DoctorNameLabel.AutoSize = true;
            this.DoctorNameLabel.Location = new System.Drawing.Point(15, 35);
            this.DoctorNameLabel.Name = "DoctorNameLabel";
            this.DoctorNameLabel.Text = "Full Name*:";
            this.DoctorNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorNameLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorNameTextBox.Location = new System.Drawing.Point(100, 32);
            this.DoctorNameTextBox.Name = "DoctorNameTextBox";
            this.DoctorNameTextBox.Size = new System.Drawing.Size(140, 23);
            this.DoctorNameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.DoctorSpecLabel.AutoSize = true;
            this.DoctorSpecLabel.Location = new System.Drawing.Point(260, 35);
            this.DoctorSpecLabel.Name = "DoctorSpecLabel";
            this.DoctorSpecLabel.Text = "Specialization*:";
            this.DoctorSpecLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorSpecLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorSpecComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DoctorSpecComboBox.Location = new System.Drawing.Point(355, 32);
            this.DoctorSpecComboBox.Name = "DoctorSpecComboBox";
            this.DoctorSpecComboBox.Size = new System.Drawing.Size(130, 23);
            this.DoctorSpecComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorSpecComboBox.Items.AddRange(new object[] {
                "General Practice",
                "Cardiology",
                "Dermatology",
                "Endocrinology",
                "Gastroenterology",
                "Neurology",
                "Obstetrics & Gynecology",
                "Oncology",
                "Ophthalmology",
                "Orthopedics",
                "Pediatrics",
                "Psychiatry",
                "Pulmonology",
                "Radiology",
                "Urology",
                "ENT (Otolaryngology)",
                "Nephrology",
                "Rheumatology",
                "Anesthesiology",
                "Emergency Medicine"
            });

            this.DoctorLicenseLabel.AutoSize = true;
            this.DoctorLicenseLabel.Location = new System.Drawing.Point(15, 70);
            this.DoctorLicenseLabel.Name = "DoctorLicenseLabel";
            this.DoctorLicenseLabel.Text = "License No:";
            this.DoctorLicenseLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorLicenseLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorLicenseTextBox.Location = new System.Drawing.Point(100, 67);
            this.DoctorLicenseTextBox.Name = "DoctorLicenseTextBox";
            this.DoctorLicenseTextBox.Size = new System.Drawing.Size(140, 23);
            this.DoctorLicenseTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.DoctorEmailLabel.AutoSize = true;
            this.DoctorEmailLabel.Location = new System.Drawing.Point(260, 70);
            this.DoctorEmailLabel.Name = "DoctorEmailLabel";
            this.DoctorEmailLabel.Text = "Email*:";
            this.DoctorEmailLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorEmailLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorEmailTextBox.Location = new System.Drawing.Point(310, 67);
            this.DoctorEmailTextBox.Name = "DoctorEmailTextBox";
            this.DoctorEmailTextBox.Size = new System.Drawing.Size(175, 23);
            this.DoctorEmailTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.DoctorPhoneLabel.AutoSize = true;
            this.DoctorPhoneLabel.Location = new System.Drawing.Point(15, 105);
            this.DoctorPhoneLabel.Name = "DoctorPhoneLabel";
            this.DoctorPhoneLabel.Text = "Phone:";
            this.DoctorPhoneLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorPhoneLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorPhoneTextBox.Location = new System.Drawing.Point(100, 102);
            this.DoctorPhoneTextBox.Name = "DoctorPhoneTextBox";
            this.DoctorPhoneTextBox.Size = new System.Drawing.Size(140, 23);
            this.DoctorPhoneTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.DoctorStartHoursLabel.AutoSize = true;
            this.DoctorStartHoursLabel.Location = new System.Drawing.Point(260, 105);
            this.DoctorStartHoursLabel.Name = "DoctorStartHoursLabel";
            this.DoctorStartHoursLabel.Text = "Start:";
            this.DoctorStartHoursLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorStartHoursLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorStartHoursTextBox.Location = new System.Drawing.Point(300, 102);
            this.DoctorStartHoursTextBox.Name = "DoctorStartHoursTextBox";
            this.DoctorStartHoursTextBox.Size = new System.Drawing.Size(60, 23);
            this.DoctorStartHoursTextBox.Text = "09:00";
            this.DoctorStartHoursTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.DoctorEndHoursLabel.AutoSize = true;
            this.DoctorEndHoursLabel.Location = new System.Drawing.Point(380, 105);
            this.DoctorEndHoursLabel.Name = "DoctorEndHoursLabel";
            this.DoctorEndHoursLabel.Text = "End:";
            this.DoctorEndHoursLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorEndHoursLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorEndHoursTextBox.Location = new System.Drawing.Point(420, 102);
            this.DoctorEndHoursTextBox.Name = "DoctorEndHoursTextBox";
            this.DoctorEndHoursTextBox.Size = new System.Drawing.Size(60, 23);
            this.DoctorEndHoursTextBox.Text = "17:00";
            this.DoctorEndHoursTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.DoctorDurationLabel.AutoSize = true;
            this.DoctorDurationLabel.Location = new System.Drawing.Point(15, 140);
            this.DoctorDurationLabel.Name = "DoctorDurationLabel";
            this.DoctorDurationLabel.Text = "Appt Duration (mins):";
            this.DoctorDurationLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorDurationLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorDurationNumeric.Location = new System.Drawing.Point(145, 137);
            this.DoctorDurationNumeric.Name = "DoctorDurationNumeric";
            this.DoctorDurationNumeric.Size = new System.Drawing.Size(70, 23);
            this.DoctorDurationNumeric.Minimum = 10;
            this.DoctorDurationNumeric.Maximum = 120;
            this.DoctorDurationNumeric.Value = 30;
            this.DoctorDurationNumeric.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.AddDoctorButton.Location = new System.Drawing.Point(350, 175);
            this.AddDoctorButton.Name = "AddDoctorButton";
            this.AddDoctorButton.Size = new System.Drawing.Size(135, 35);
            this.AddDoctorButton.Text = "➕ Add Doctor";
            this.AddDoctorButton.UseVisualStyleBackColor = false;
            this.AddDoctorButton.BackColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.AddDoctorButton.ForeColor = System.Drawing.Color.White;
            this.AddDoctorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddDoctorButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.AddDoctorButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddDoctorButton.Click += new System.EventHandler(this.AddDoctorButton_Click);

            // EditDoctorButton
            this.EditDoctorButton.Location = new System.Drawing.Point(15, 535);
            this.EditDoctorButton.Name = "EditDoctorButton";
            this.EditDoctorButton.Size = new System.Drawing.Size(130, 35);
            this.EditDoctorButton.Text = "✏️ Edit";
            this.EditDoctorButton.UseVisualStyleBackColor = false;
            this.EditDoctorButton.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.EditDoctorButton.ForeColor = System.Drawing.Color.White;
            this.EditDoctorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditDoctorButton.FlatAppearance.BorderSize = 0;
            this.EditDoctorButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.EditDoctorButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditDoctorButton.Click += new System.EventHandler(this.EditDoctorButton_Click);

            // DeleteDoctorButton
            this.DeleteDoctorButton.Location = new System.Drawing.Point(155, 535);
            this.DeleteDoctorButton.Name = "DeleteDoctorButton";
            this.DeleteDoctorButton.Size = new System.Drawing.Size(130, 35);
            this.DeleteDoctorButton.Text = "🗑️ Delete";
            this.DeleteDoctorButton.UseVisualStyleBackColor = false;
            this.DeleteDoctorButton.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.DeleteDoctorButton.ForeColor = System.Drawing.Color.White;
            this.DeleteDoctorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteDoctorButton.FlatAppearance.BorderSize = 0;
            this.DeleteDoctorButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.DeleteDoctorButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteDoctorButton.Click += new System.EventHandler(this.DeleteDoctorButton_Click);

            // AppointmentsTab
            this.AppointmentsTab.Controls.Add(this.ToolsGroupBox);
            this.AppointmentsTab.Controls.Add(this.AppointmentActionsGroupBox);
            this.AppointmentsTab.Controls.Add(this.BookingGroupBox);
            this.AppointmentsTab.Controls.Add(this.AppointmentLabel);
            this.AppointmentsTab.Controls.Add(this.AppointmentDataGridView);
            this.AppointmentsTab.Location = new System.Drawing.Point(4, 29);
            this.AppointmentsTab.Name = "AppointmentsTab";
            this.AppointmentsTab.Padding = new System.Windows.Forms.Padding(15);
            this.AppointmentsTab.Size = new System.Drawing.Size(1076, 578);
            this.AppointmentsTab.TabIndex = 2;
            this.AppointmentsTab.Text = "📅 Appointments";
            this.AppointmentsTab.UseVisualStyleBackColor = true;
            this.AppointmentsTab.BackColor = System.Drawing.Color.FromArgb(250, 250, 252);

            // AppointmentDataGridView
            this.AppointmentDataGridView.Location = new System.Drawing.Point(15, 45);
            this.AppointmentDataGridView.Name = "AppointmentDataGridView";
            this.AppointmentDataGridView.Size = new System.Drawing.Size(1046, 350);
            this.AppointmentDataGridView.TabIndex = 1;
            this.AppointmentDataGridView.ReadOnly = true;
            this.AppointmentDataGridView.AllowUserToAddRows = false;
            this.AppointmentDataGridView.AllowUserToDeleteRows = false;
            this.AppointmentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AppointmentDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AppointmentDataGridView.ColumnHeadersVisible = true;
            this.AppointmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppointmentDataGridView.ColumnHeadersHeight = 35;
            this.AppointmentDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.AppointmentDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AppointmentDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.AppointmentDataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.AppointmentDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.AppointmentDataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.AppointmentDataGridView.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AppointmentDataGridView.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.AppointmentDataGridView.EnableHeadersVisualStyles = false;
            this.AppointmentDataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 249, 252);
            this.AppointmentDataGridView.RowTemplate.Height = 30;
            this.AppointmentDataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AppointmentDataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(200, 230, 245);
            this.AppointmentDataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.AppointmentDataGridView.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            this.AppointmentLabel.AutoSize = true;
            this.AppointmentLabel.Location = new System.Drawing.Point(15, 15);
            this.AppointmentLabel.Name = "AppointmentLabel";
            this.AppointmentLabel.Size = new System.Drawing.Size(80, 15);
            this.AppointmentLabel.TabIndex = 0;
            this.AppointmentLabel.Text = "📋 Scheduled Appointments";
            this.AppointmentLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.AppointmentLabel.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);

            // BookingGroupBox
            this.BookingGroupBox.Controls.Add(this.DoctorLabel);
            this.BookingGroupBox.Controls.Add(this.DoctorComboBox);
            this.BookingGroupBox.Controls.Add(this.PatientLabel);
            this.BookingGroupBox.Controls.Add(this.PatientComboBox);
            this.BookingGroupBox.Controls.Add(this.DateLabel);
            this.BookingGroupBox.Controls.Add(this.AppointmentDatePicker);
            this.BookingGroupBox.Controls.Add(this.TimeLabel);
            this.BookingGroupBox.Controls.Add(this.StartTimePicker);
            this.BookingGroupBox.Controls.Add(this.ReasonLabel);
            this.BookingGroupBox.Controls.Add(this.ReasonTextBox);
            this.BookingGroupBox.Controls.Add(this.BookButton);
            this.BookingGroupBox.Controls.Add(this.AppointmentCancelButton);
            this.BookingGroupBox.Location = new System.Drawing.Point(15, 405);
            this.BookingGroupBox.Name = "BookingGroupBox";
            this.BookingGroupBox.Size = new System.Drawing.Size(1046, 105);
            this.BookingGroupBox.TabIndex = 2;
            this.BookingGroupBox.TabStop = false;
            this.BookingGroupBox.Text = "➕ Book New Appointment";
            this.BookingGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BookingGroupBox.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.BookingGroupBox.BackColor = System.Drawing.Color.White;

            this.DoctorLabel.AutoSize = true;
            this.DoctorLabel.Location = new System.Drawing.Point(15, 35);
            this.DoctorLabel.Name = "DoctorLabel";
            this.DoctorLabel.Size = new System.Drawing.Size(41, 13);
            this.DoctorLabel.TabIndex = 0;
            this.DoctorLabel.Text = "Doctor:";
            this.DoctorLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DoctorLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.DoctorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DoctorComboBox.Location = new System.Drawing.Point(70, 32);
            this.DoctorComboBox.Name = "DoctorComboBox";
            this.DoctorComboBox.Size = new System.Drawing.Size(150, 23);
            this.DoctorComboBox.TabIndex = 1;
            this.DoctorComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.PatientLabel.AutoSize = true;
            this.PatientLabel.Location = new System.Drawing.Point(240, 35);
            this.PatientLabel.Name = "PatientLabel";
            this.PatientLabel.Size = new System.Drawing.Size(41, 13);
            this.PatientLabel.TabIndex = 0;
            this.PatientLabel.Text = "Patient:";
            this.PatientLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PatientLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.PatientComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PatientComboBox.Location = new System.Drawing.Point(300, 32);
            this.PatientComboBox.Name = "PatientComboBox";
            this.PatientComboBox.Size = new System.Drawing.Size(150, 23);
            this.PatientComboBox.TabIndex = 1;
            this.PatientComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(470, 35);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(33, 13);
            this.DateLabel.TabIndex = 0;
            this.DateLabel.Text = "Date:";
            this.DateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DateLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.AppointmentDatePicker.Location = new System.Drawing.Point(510, 32);
            this.AppointmentDatePicker.Name = "AppointmentDatePicker";
            this.AppointmentDatePicker.Size = new System.Drawing.Size(130, 23);
            this.AppointmentDatePicker.TabIndex = 2;
            this.AppointmentDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.AppointmentDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(660, 35);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(30, 13);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "Time:";
            this.TimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TimeLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.StartTimePicker.Location = new System.Drawing.Point(705, 32);
            this.StartTimePicker.Name = "StartTimePicker";
            this.StartTimePicker.Size = new System.Drawing.Size(90, 23);
            this.StartTimePicker.TabIndex = 2;
            this.StartTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartTimePicker.CustomFormat = "HH:mm";
            this.StartTimePicker.ShowUpDown = true;
            this.StartTimePicker.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.ReasonLabel.AutoSize = true;
            this.ReasonLabel.Location = new System.Drawing.Point(15, 70);
            this.ReasonLabel.Name = "ReasonLabel";
            this.ReasonLabel.Size = new System.Drawing.Size(44, 13);
            this.ReasonLabel.TabIndex = 0;
            this.ReasonLabel.Text = "Reason:";
            this.ReasonLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ReasonLabel.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);

            this.ReasonTextBox.Location = new System.Drawing.Point(70, 67);
            this.ReasonTextBox.Name = "ReasonTextBox";
            this.ReasonTextBox.Size = new System.Drawing.Size(380, 23);
            this.ReasonTextBox.TabIndex = 3;
            this.ReasonTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.BookButton.Location = new System.Drawing.Point(820, 35);
            this.BookButton.Name = "BookButton";
            this.BookButton.Size = new System.Drawing.Size(100, 32);
            this.BookButton.TabIndex = 4;
            this.BookButton.Text = "✓ Book";
            this.BookButton.UseVisualStyleBackColor = false;
            this.BookButton.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.BookButton.ForeColor = System.Drawing.Color.White;
            this.BookButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BookButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BookButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BookButton.Click += new System.EventHandler(this.BookButton_Click);

            this.AppointmentCancelButton.Location = new System.Drawing.Point(930, 35);
            this.AppointmentCancelButton.Name = "AppointmentCancelButton";
            this.AppointmentCancelButton.Size = new System.Drawing.Size(100, 32);
            this.AppointmentCancelButton.TabIndex = 4;
            this.AppointmentCancelButton.Text = "✗ Cancel";
            this.AppointmentCancelButton.UseVisualStyleBackColor = false;
            this.AppointmentCancelButton.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.AppointmentCancelButton.ForeColor = System.Drawing.Color.White;
            this.AppointmentCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AppointmentCancelButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.AppointmentCancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AppointmentCancelButton.Click += new System.EventHandler(this.AppointmentCancelButton_Click);

            // AppointmentActionsGroupBox
            this.AppointmentActionsGroupBox.Controls.Add(this.EditAppointmentButton);
            this.AppointmentActionsGroupBox.Controls.Add(this.DeleteAppointmentButton);
            this.AppointmentActionsGroupBox.Location = new System.Drawing.Point(15, 520);
            this.AppointmentActionsGroupBox.Name = "AppointmentActionsGroupBox";
            this.AppointmentActionsGroupBox.Size = new System.Drawing.Size(300, 55);
            this.AppointmentActionsGroupBox.TabIndex = 4;
            this.AppointmentActionsGroupBox.TabStop = false;
            this.AppointmentActionsGroupBox.Text = "📝 Appointment Actions";
            this.AppointmentActionsGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.AppointmentActionsGroupBox.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.AppointmentActionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.AppointmentActionsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // EditAppointmentButton
            this.EditAppointmentButton.Location = new System.Drawing.Point(15, 18);
            this.EditAppointmentButton.Name = "EditAppointmentButton";
            this.EditAppointmentButton.Size = new System.Drawing.Size(120, 30);
            this.EditAppointmentButton.TabIndex = 0;
            this.EditAppointmentButton.Text = "✏️ Edit";
            this.EditAppointmentButton.UseVisualStyleBackColor = false;
            this.EditAppointmentButton.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.EditAppointmentButton.ForeColor = System.Drawing.Color.White;
            this.EditAppointmentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditAppointmentButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.EditAppointmentButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditAppointmentButton.Click += new System.EventHandler(this.EditAppointmentButton_Click);

            // DeleteAppointmentButton
            this.DeleteAppointmentButton.Location = new System.Drawing.Point(150, 18);
            this.DeleteAppointmentButton.Name = "DeleteAppointmentButton";
            this.DeleteAppointmentButton.Size = new System.Drawing.Size(120, 30);
            this.DeleteAppointmentButton.TabIndex = 1;
            this.DeleteAppointmentButton.Text = "🗑️ Delete";
            this.DeleteAppointmentButton.UseVisualStyleBackColor = false;
            this.DeleteAppointmentButton.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.DeleteAppointmentButton.ForeColor = System.Drawing.Color.White;
            this.DeleteAppointmentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAppointmentButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.DeleteAppointmentButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteAppointmentButton.Click += new System.EventHandler(this.DeleteAppointmentButton_Click);

            // ToolsGroupBox
            this.ToolsGroupBox.Controls.Add(this.ExportButton);
            this.ToolsGroupBox.Controls.Add(this.ViewScheduleButton);
            this.ToolsGroupBox.Controls.Add(this.SendNotificationsButton);
            this.ToolsGroupBox.Location = new System.Drawing.Point(330, 520);
            this.ToolsGroupBox.Name = "ToolsGroupBox";
            this.ToolsGroupBox.Size = new System.Drawing.Size(731, 55);
            this.ToolsGroupBox.TabIndex = 3;
            this.ToolsGroupBox.TabStop = false;
            this.ToolsGroupBox.Text = "⚡ Quick Actions";
            this.ToolsGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ToolsGroupBox.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.ToolsGroupBox.ForeColor = System.Drawing.Color.FromArgb(45, 125, 154);
            this.ToolsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.ExportButton.Location = new System.Drawing.Point(15, 18);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(140, 30);
            this.ExportButton.TabIndex = 5;
            this.ExportButton.Text = "📊 Export to CSV";
            this.ExportButton.UseVisualStyleBackColor = false;
            this.ExportButton.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.ExportButton.ForeColor = System.Drawing.Color.White;
            this.ExportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ExportButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);

            this.ViewScheduleButton.Location = new System.Drawing.Point(170, 18);
            this.ViewScheduleButton.Name = "ViewScheduleButton";
            this.ViewScheduleButton.Size = new System.Drawing.Size(140, 30);
            this.ViewScheduleButton.TabIndex = 5;
            this.ViewScheduleButton.Text = "📅 View Schedule";
            this.ViewScheduleButton.UseVisualStyleBackColor = false;
            this.ViewScheduleButton.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.ViewScheduleButton.ForeColor = System.Drawing.Color.White;
            this.ViewScheduleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewScheduleButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ViewScheduleButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewScheduleButton.Click += new System.EventHandler(this.ViewScheduleButton_Click);

            this.SendNotificationsButton.Location = new System.Drawing.Point(325, 18);
            this.SendNotificationsButton.Name = "SendNotificationsButton";
            this.SendNotificationsButton.Size = new System.Drawing.Size(160, 30);
            this.SendNotificationsButton.TabIndex = 5;
            this.SendNotificationsButton.Text = "🔔 Send Notifications";
            this.SendNotificationsButton.UseVisualStyleBackColor = false;
            this.SendNotificationsButton.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
            this.SendNotificationsButton.ForeColor = System.Drawing.Color.White;
            this.SendNotificationsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendNotificationsButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.SendNotificationsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SendNotificationsButton.Click += new System.EventHandler(this.SendNotificationsButton_Click);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.MainTabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🏥 Clinic Appointment Manager";
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.MinimumSize = new System.Drawing.Size(1100, 700);

            this.MainTabControl.ResumeLayout(false);
            this.PatientsTab.ResumeLayout(false);
            this.PatientsTab.PerformLayout();
            this.DoctorsTab.ResumeLayout(false);
            this.DoctorsTab.PerformLayout();
            this.AppointmentsTab.ResumeLayout(false);
            this.AppointmentsTab.PerformLayout();
            this.BookingGroupBox.ResumeLayout(false);
            this.BookingGroupBox.PerformLayout();
            this.AppointmentActionsGroupBox.ResumeLayout(false);
            this.ToolsGroupBox.ResumeLayout(false);
            this.PatientFormGroupBox.ResumeLayout(false);
            this.PatientFormGroupBox.PerformLayout();
            this.DoctorFormGroupBox.ResumeLayout(false);
            this.DoctorFormGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorDurationNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientAgeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoctorDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentDataGridView)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage PatientsTab;
        private System.Windows.Forms.TabPage DoctorsTab;
        private System.Windows.Forms.TabPage AppointmentsTab;
        private System.Windows.Forms.DataGridView PatientDataGridView;
        private System.Windows.Forms.DataGridView DoctorDataGridView;
        private System.Windows.Forms.DataGridView AppointmentDataGridView;
        private System.Windows.Forms.Label PatientsLabel;
        private System.Windows.Forms.Label DoctorsLabel;
        private System.Windows.Forms.Label AppointmentLabel;
        private System.Windows.Forms.GroupBox BookingGroupBox;
        private System.Windows.Forms.Label DoctorLabel;
        private System.Windows.Forms.ComboBox DoctorComboBox;
        private System.Windows.Forms.Label PatientLabel;
        private System.Windows.Forms.ComboBox PatientComboBox;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.DateTimePicker AppointmentDatePicker;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.DateTimePicker StartTimePicker;
        private System.Windows.Forms.Label ReasonLabel;
        private System.Windows.Forms.TextBox ReasonTextBox;
        private System.Windows.Forms.Button BookButton;
        private System.Windows.Forms.Button AppointmentCancelButton;
        private System.Windows.Forms.GroupBox ToolsGroupBox;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ViewScheduleButton;
        private System.Windows.Forms.Button SendNotificationsButton;

        // Patient Form Fields
        private System.Windows.Forms.GroupBox PatientFormGroupBox;
        private System.Windows.Forms.Label PatientNameLabel;
        private System.Windows.Forms.TextBox PatientNameTextBox;
        private System.Windows.Forms.Label PatientAgeLabel;
        private System.Windows.Forms.NumericUpDown PatientAgeNumeric;
        private System.Windows.Forms.Label PatientGenderLabel;
        private System.Windows.Forms.ComboBox PatientGenderComboBox;
        private System.Windows.Forms.Label PatientPhoneLabel;
        private System.Windows.Forms.TextBox PatientPhoneTextBox;
        private System.Windows.Forms.Label PatientEmailLabel;
        private System.Windows.Forms.TextBox PatientEmailTextBox;
        private System.Windows.Forms.Label PatientHistoryLabel;
        private System.Windows.Forms.TextBox PatientHistoryTextBox;
        private System.Windows.Forms.Button AddPatientButton;

        // Doctor Form Fields
        private System.Windows.Forms.GroupBox DoctorFormGroupBox;
        private System.Windows.Forms.Label DoctorNameLabel;
        private System.Windows.Forms.TextBox DoctorNameTextBox;
        private System.Windows.Forms.Label DoctorSpecLabel;
        private System.Windows.Forms.ComboBox DoctorSpecComboBox;
        private System.Windows.Forms.Label DoctorLicenseLabel;
        private System.Windows.Forms.TextBox DoctorLicenseTextBox;
        private System.Windows.Forms.Label DoctorEmailLabel;
        private System.Windows.Forms.TextBox DoctorEmailTextBox;
        private System.Windows.Forms.Label DoctorPhoneLabel;
        private System.Windows.Forms.TextBox DoctorPhoneTextBox;
        private System.Windows.Forms.Label DoctorStartHoursLabel;
        private System.Windows.Forms.TextBox DoctorStartHoursTextBox;
        private System.Windows.Forms.Label DoctorEndHoursLabel;
        private System.Windows.Forms.TextBox DoctorEndHoursTextBox;
        private System.Windows.Forms.Label DoctorDurationLabel;
        private System.Windows.Forms.NumericUpDown DoctorDurationNumeric;
        private System.Windows.Forms.Button AddDoctorButton;
        private System.Windows.Forms.Button DeletePatientButton;
        private System.Windows.Forms.Button EditPatientButton;
        private System.Windows.Forms.Button DeleteDoctorButton;
        private System.Windows.Forms.Button EditDoctorButton;
        private System.Windows.Forms.Button DeleteAppointmentButton;
        private System.Windows.Forms.Button EditAppointmentButton;
        private System.Windows.Forms.GroupBox AppointmentActionsGroupBox;
    }
}
