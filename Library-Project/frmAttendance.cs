using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ticket_management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Library_Project
{
    public partial class frmAttendance : Form
    {
        Class1 attendance = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        private Timer clockTimer;
        public frmAttendance(string username)
        {
            InitializeComponent();
            this.username = username;
            txtID.KeyDown += txtID_KeyDown;

            clockTimer = new Timer();
            clockTimer.Interval = 1000; // Update every 1 second
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void frmAttendance_Load(object sender, EventArgs e)
        {
            cmbUsertype.Items.Clear();
            cmbUsertype.Items.Add("STUDENT");
            cmbUsertype.Items.Add("TEACHER");
            cmbUsertype.SelectedIndex = -1;

            LoadTodayAttendance();

            if (dgvAttendance.Columns.Count > 0)
            {
                dgvAttendance.Columns[0].HeaderText = "ID Number";
                dgvAttendance.Columns[1].HeaderText = "Username";
                dgvAttendance.Columns[2].HeaderText = "User Type";
                dgvAttendance.Columns[3].HeaderText = "Date In";
                dgvAttendance.Columns[4].HeaderText = "Time In";
            }

            cmbGrade.Enabled = false;
            cmbSection.Enabled = false;
            cmbSubject.Enabled = false;
            txtID.Focus();
        }


        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
        private void cmbUsertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsertype.SelectedItem == null)
            {
                cmbGrade.Enabled = false;
                cmbSection.Enabled = false;
                cmbSubject.Enabled = false;
                return;
            }

            string selectedType = cmbUsertype.SelectedItem.ToString();

            if (selectedType == "STUDENT")
            {
                cmbGrade.Enabled = true;
                cmbSection.Enabled = true;
                cmbSubject.Enabled = false;

                cmbSubject.SelectedIndex = -1;
            }
            else if (selectedType == "TEACHER")
            {
                cmbGrade.Enabled = false;
                cmbSection.Enabled = false;
                cmbSubject.Enabled = true;

                cmbGrade.SelectedIndex = -1;
                cmbSection.SelectedIndex = -1;
            }
        }
        private void ProcessAttendance(string idNumber, string name, string userType, string grade = "", string section = "", string subject = "")
        {
            try
            {
                string currentDate = DateTime.Now.ToString("yyyy/MM/dd");
                string currentTime = DateTime.Now.ToString("hh:mm tt");

                DataTable attendanceCheck = attendance.GetData(
                    $"SELECT * FROM tbl_attendance WHERE ID_number = '{idNumber}' AND Date_in = '{currentDate}'");

                if (attendanceCheck.Rows.Count > 0)
                {
                    attendance.executeSQL(
                        $"UPDATE tbl_attendance SET username = '{name}', usertype = '{userType}', time_in = '{currentTime}' " +
                        $"WHERE ID_number = '{idNumber}' AND Date_in = '{currentDate}'");

                    MessageBox.Show($"{userType} {name} time updated successfully!",
                        "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LogAction("UPDATE", "ATTENDANCE SYSTEM",
                        $"{userType} {name} ({idNumber}) time updated");
                }
                else
                {
                    attendance.executeSQL(
                        $"INSERT INTO tbl_attendance (ID_number, username, usertype, Date_in, time_in) " +
                        $"VALUES ('{idNumber}', '{name}', '{userType}', '{currentDate}', '{currentTime}')");

                    MessageBox.Show($"Attendance recorded successfully!\n\n" +
                        $"ID: {idNumber}\n" +
                        $"Name: {name}\n" +
                        $"Type: {userType}\n" +
                        $"Time: {currentTime}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LogAction("ATTENDANCE", "ATTENDANCE SYSTEM",
                        $"{userType} {name} ({idNumber}) logged in");
                }

                // Sync to management
                SyncToManagement(idNumber, name, userType, grade, section, subject);

                LoadTodayAttendance();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing attendance: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SyncToManagement(string idNumber, string name, string userType, string grade, string section, string subject)
        {
            try
            {
                string currentDate = DateTime.Now.ToString("yyyy/MM/dd");

                if (userType == "STUDENT")
                {
                    DataTable check = attendance.GetData(
                        $"SELECT * FROM tbl_students WHERE student_ID = '{idNumber}'");

                    if (check.Rows.Count > 0)
                    {
                        attendance.executeSQL(
                            $"UPDATE tbl_students SET name = '{name}', grade = '{grade}', section = '{section}', date_in = '{currentDate}' " +
                            $"WHERE student_ID = '{idNumber}'");
                        LogAction("UPDATE", "STUDENT MANAGEMENT", $"Student {name} ({idNumber}) date_in updated");
                    }
                    else
                    {
                        attendance.executeSQL(
                            $"INSERT INTO tbl_students (student_ID, name, grade, section, date_in) " +
                            $"VALUES ('{idNumber}', '{name}', '{grade}', '{section}', '{currentDate}')");
                        LogAction("ADD", "STUDENT MANAGEMENT", $"New student {name} ({idNumber}) added");
                    }

                    var studentForm = Application.OpenForms.OfType<frmStudentManagement>().FirstOrDefault();
                    if (studentForm != null)
                    {
                        studentForm.RefreshStudentsPublic();
                    }
                }
                else if (userType == "TEACHER")
                {
                    DataTable check = attendance.GetData(
                        $"SELECT * FROM tbl_teacher WHERE teacher_ID = '{idNumber}'");

                    if (check.Rows.Count > 0)
                    {
                        attendance.executeSQL(
                            $"UPDATE tbl_teacher SET name = '{name}', subject = '{subject}', date_in = '{currentDate}' " +
                            $"WHERE teacher_ID = '{idNumber}'");
                        LogAction("UPDATE", "TEACHER MANAGEMENT", $"Teacher {name} ({idNumber}) date_in updated");
                    }
                    else
                    {
                        attendance.executeSQL(
                            $"INSERT INTO tbl_teacher (teacher_ID, name, subject, date_in) " +
                            $"VALUES ('{idNumber}', '{name}', '{subject}', '{currentDate}')");
                        LogAction("ADD", "TEACHER MANAGEMENT", $"New teacher {name} ({idNumber}) added");
                    }

                    var teacherForm = Application.OpenForms.OfType<frmTeachersManagement>().FirstOrDefault();
                    if (teacherForm != null)
                    {
                        teacherForm.RefreshTeachersPublic();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error syncing to management: " + ex.Message, "Sync Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearForm()
        {
            txtID.Clear();
            txtName.Clear();
            cmbGrade.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbUsertype.SelectedIndex = -1;
            cmbSubject.SelectedIndex = -1;

            cmbGrade.Enabled = false;
            cmbSection.Enabled = false;
            cmbSubject.Enabled = false;

            txtID.Focus();
        }
        private void LoadTodayAttendance()
        {
            try
            {
                string currentDate = DateTime.Now.ToString("yyyy/MM/dd");
                string query = "SELECT ID_number, username, usertype, Date_in, time_in " +
                               "FROM tbl_attendance " +
                               $"WHERE Date_in = '{currentDate}' " +
                               "ORDER BY time_in DESC";

                DataTable dt = attendance.GetData(query);
                dgvAttendance.DataSource = dt;
                dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LogAction(string action, string module, string details)
        {
            try
            {
                attendance.executeSQL(
                    $"INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                    $"VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now:hh\\:mm tt}', '{action}', '{module}', '{details}', '{username}')");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Log Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtName.Focus();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string idNumber = txtID.Text.Trim();
            string name = txtName.Text.Trim();
            string userType = cmbUsertype.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(idNumber))
            {
                MessageBox.Show("Please scan or enter ID Number.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter Name.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(userType))
            {
                MessageBox.Show("Please select User Type.", "Required Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUsertype.Focus();
                return;
            }

            string grade = (cmbGrade.Enabled) ? cmbGrade.Text.Trim() : "";
            string section = (cmbSection.Enabled) ? cmbSection.Text.Trim() : "";
            string subject = (cmbSubject.Enabled) ? cmbSubject.Text.Trim() : "";

            ProcessAttendance(idNumber, name, userType, grade, section, subject);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        { 

        }
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }
    }
}
