using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
        private Label lblToast;
        private int currentPage = 1;
        private int pageSize = 20;
        public frmAttendance(string username)
        {
            InitializeComponent();
            this.username = username;

            lblToast = new Label();
            lblToast.AutoSize = true;
            lblToast.BackColor = Color.FromArgb(50, 50, 50);
            lblToast.ForeColor = Color.White;
            lblToast.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblToast.Padding = new Padding(10);
            lblToast.Visible = false;
            lblToast.BorderStyle = BorderStyle.None;
            lblToast.BringToFront();
            lblToast.Location = new Point((this.Width / 2) - 150, this.Height - 80);
            this.Controls.Add(lblToast);

            txtID.KeyDown += txtID_KeyDown;

            clockTimer = new Timer();
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            dgvAttendance.DataBindingComplete += dgvAttendance_DataBindingComplete;
        }

        private void frmAttendance_Load(object sender, EventArgs e)
        {
            LoadTodayAttendance();

            if (dgvAttendance.Columns.Count > 0)
            {
                dgvAttendance.Columns[0].HeaderText = "ID Number";
                dgvAttendance.Columns[1].HeaderText = "Username";
                dgvAttendance.Columns[2].HeaderText = "User Type";
                dgvAttendance.Columns[3].HeaderText = "Date In";
                dgvAttendance.Columns[4].HeaderText = "Time In";
            }

            txtID.Focus();
        }

        private async void ShowToast(string message, Color backColor)
        {
            lblToast.Text = message;
            lblToast.BackColor = backColor;
            lblToast.Visible = true;
            lblToast.BringToFront();

            await Task.Delay(2000); // show for 2 seconds
            lblToast.Visible = false;
        }
        private void ProcessAttendance(string idNumber, string name, string userType, string grade = "", string section = "", string subject = "")
        {
            try
            {
                string currentDate = DateTime.Now.ToString("yyyy/MM/dd");
                string currentTime = DateTime.Now.ToString("hh:mm tt");

                // ✅ Check if already has attendance today
                DataTable attendanceCheck = attendance.GetData(
                    $"SELECT * FROM tbl_attendance WHERE ID_number = '{idNumber}' AND Date_in = '{currentDate}'");

                if (attendanceCheck.Rows.Count > 0)
                {
                    // ⚠️ Already logged in
                    ShowToast($"{name} has already logged in today.", Color.FromArgb(244, 67, 54)); // red toast
                    ClearForm();
                    return;
                }

                // ✅ Insert new attendance record
                attendance.executeSQL(
                    $"INSERT INTO tbl_attendance (ID_number, username, usertype, Date_in, time_in) " +
                    $"VALUES ('{idNumber}', '{name}', '{userType}', '{currentDate}', '{currentTime}')");

                ShowToast($"{name}'s attendance recorded successfully!", Color.FromArgb(76, 175, 80)); // green toast

                LoadTodayAttendance();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowToast("Error processing attendance: " + ex.Message, Color.FromArgb(231, 76, 60)); // red toast
            }
        }
        private void ClearForm()
        {
            txtID.Clear();
            txtID.Focus();
        }
        private void LoadTodayAttendance()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;
                string currentDate = DateTime.Now.ToString("yyyy/MM/dd");
                string searchCondition = "";

                string keyword = txtsearch.Text.Trim().Replace("'", "''");
                if (!string.IsNullOrEmpty(keyword))
                {
                    searchCondition = $" AND (username LIKE '%{keyword}%' OR ID_number LIKE '%{keyword}%' OR usertype LIKE '%{keyword}%')";
                }

                // load extra record to detect next page
                string query = "SELECT ID_number, username, usertype, Date_in, time_in " +
                               "FROM tbl_attendance " +
                               $"WHERE Date_in = '{currentDate}' {searchCondition} " +
                               "ORDER BY time_in DESC " +
                               $"LIMIT {pageSize + 1} OFFSET {offset}";

                DataTable dt = attendance.GetData(query);
                bool hasNextPage = dt.Rows.Count > pageSize;

                if (hasNextPage)
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);

                dgvAttendance.DataSource = dt;
                dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // update buttons + label
                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = hasNextPage;
                lblPageInfo.Text = $"Page {currentPage}" + (hasNextPage ? " →" : "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string scannedID = txtID.Text.Trim();
                if (string.IsNullOrEmpty(scannedID))
                    return; // do nothing if empty

                AutoFillUserData(scannedID);
            }
        }
        private void AutoFillUserData(string scannedID)
        {
            try
            {
                string cleanID = scannedID.Replace("'", "''");

                // ✅ Check if student exists
                DataTable student = attendance.GetData(
                    $"SELECT student_ID, name, grade, section FROM tbl_students WHERE student_ID = '{cleanID}'");

                if (student.Rows.Count > 0)
                {
                    string name = student.Rows[0]["name"].ToString();
                    string grade = student.Rows[0]["grade"].ToString();
                    string section = student.Rows[0]["section"].ToString();


                    ProcessAttendance(cleanID, name, "STUDENT", grade, section);
                    return;
                }

                // ✅ Check if teacher exists
                DataTable teacher = attendance.GetData(
                    $"SELECT teacher_ID, name, subject FROM tbl_teacher WHERE teacher_ID = '{cleanID}'");

                if (teacher.Rows.Count > 0)
                {
                    string name = teacher.Rows[0]["name"].ToString();
                    string subject = teacher.Rows[0]["subject"].ToString();
                    

                    ProcessAttendance(cleanID, name, "TEACHER", "", "", subject);
                    return;
                }

                // ❌ Not found in both tables
                MessageBox.Show("This ID does not exist in Student or Teacher Management.\nPlease register it first.",
                                "Unregistered ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while checking ID: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearForm();
            }
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }

        private void LoadAttendanceByDate(DateTime selectedDate)
        {
            try
            {
                string formattedDate = selectedDate.ToString("yyyy/MM/dd");
                string query = "SELECT ID_number, username, usertype, Date_in, time_in " +
                               "FROM tbl_attendance " +
                               $"WHERE Date_in = '{formattedDate}' " +
                               "ORDER BY time_in DESC";

                DataTable dt = attendance.GetData(query);
                dgvAttendance.DataSource = dt;
                dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance by date: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpDate.Value;
            LoadAttendanceByDate(selectedDate);
        }
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadTodayAttendance();
        }

        private void dgvAttendance_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                if (row.Cells["usertype"].Value != null)
                {
                    string userType = row.Cells["usertype"].Value.ToString().Trim().ToUpper();

                    if (userType == "TEACHER")
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FDECEF");
                    else if (userType == "STUDENT")
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E5F5E0");
                    else
                        row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadTodayAttendance();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadTodayAttendance();
            }
        }

        private void lblPageInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
