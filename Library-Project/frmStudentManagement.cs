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

namespace Library_Project
{
    public partial class frmStudentManagement : Form
    {
        Class1 studentmanagement = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        private int row;
        public frmStudentManagement(string username)
        {
            InitializeComponent();
            this.username = username;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            txtSearch.TextChanged += txtsearch_TextChanged;
            dgvStudents.CellClick += dgvStudents_CellClick;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
        }

        private void frmStudentManagement_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.Date;
            LoadStudentsByDate(dateTimePicker1.Value);
            txtBarcode.Focus();
            dgvStudents.Columns[0].HeaderText = "Student Number";
            dgvStudents.Columns[1].HeaderText = "Name";
            dgvStudents.Columns[2].HeaderText = "Grade";
            dgvStudents.Columns[3].HeaderText = "Section";
            dgvStudents.Columns[4].HeaderText = "Date In";
        }
        private void LoadStudentsByDate(DateTime date)
        {
            try
            {
                string dateString = date.ToString("yyyy/MM/dd"); 
                string query = "SELECT student_ID, name, grade, section, date_in " +
                               "FROM tbl_students " +
                               $"WHERE DATE(date_in) = '{dateString}' " +
                               "ORDER BY date_in DESC";

                DataTable dt = studentmanagement.GetData(query);
                dgvStudents.DataSource = dt;
                dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTodayStudents()
        {
            try
            {
                DataTable dt = studentmanagement.GetData(
                    "SELECT student_ID, name, grade, section, date_in " +
                    "FROM tbl_students " +
                    "WHERE DATE(date_in) = CURDATE() " +
                    "ORDER BY date_in DESC"
                );
                dgvStudents.DataSource = dt;
                dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading students", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim().Replace("'", "''");
                string dateString = dateTimePicker1.Value.ToString("yyyy/MM/dd");

                string query = "SELECT student_ID, name, grade, section, date_in " +
                               "FROM tbl_students " +
                               $"WHERE DATE(date_in) = '{dateString}' ";

                if (!string.IsNullOrEmpty(keyword))
                {
                    query += "AND (name LIKE '%" + keyword + "%' OR student_ID LIKE '%" + keyword + "%') ";
                }

                query += "ORDER BY date_in DESC";

                DataTable dt = studentmanagement.GetData(query);
                dgvStudents.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = e.RowIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cell Click Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            frmAddStudent addForm = new frmAddStudent(username);
            addForm.FormClosed += (s, args) =>
            {
                LoadTodayStudents();
                LogAction("ADD", "STUDENT MANAGEMENT", "Added a new student record");
            };
            addForm.ShowDialog();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            int row = dgvStudents.CurrentCell.RowIndex;

            if (row < 0 || row >= dgvStudents.Rows.Count)
            {
                MessageBox.Show("Please select a valid row to update.",
                               "Invalid Selection",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }
            string editID = dgvStudents.Rows[row].Cells[0].Value.ToString();
            string editname = dgvStudents.Rows[row].Cells[1].Value.ToString();
            string editgrade = dgvStudents.Rows[row].Cells[2].Value.ToString();
            string editsection = dgvStudents.Rows[row].Cells[3].Value.ToString();
            frmUpdateStudent updatestudentform = new frmUpdateStudent(username, editID, editname, editgrade, editsection);
            updatestudentform.FormClosed += (s, args) =>
            {
                frmStudentManagement_Load(sender, e);
            };
            updatestudentform.Show();
        }
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (row >= 0)
            {
                string id = dgvStudents.Rows[row].Cells["student_ID"].Value.ToString();
                string name = dgvStudents.Rows[row].Cells["name"].Value.ToString();
                DialogResult dr = MessageBox.Show($"Are you sure you want to delete student '{name}'?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        studentmanagement.executeSQL($"DELETE FROM tbl_students WHERE student_ID = '{id}'");
                        if (studentmanagement.rowAffected > 0)
                        {
                            LoadTodayStudents();
                            LogAction("DELETE", "STUDENT MANAGEMENT", $"Deleted student {name}");
                            MessageBox.Show("Student deleted successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadTodayStudents();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string student_ID = txtBarcode.Text.Trim();

                if (!string.IsNullOrEmpty(student_ID))
                {
                    try
                    {
                        DataTable check = studentmanagement.GetData(
                            $"SELECT * FROM tbl_students WHERE student_ID = '{student_ID}'");

                        if (check.Rows.Count > 0)
                        {
                            studentmanagement.executeSQL(
                                $"UPDATE tbl_students SET date_in = NOW() WHERE student_ID = '{student_ID}'");
                            LogAction("SCAN", "STUDENT MANAGEMENT", $"Student {student_ID} scanned");
                        }
                        else
                        {
                            studentmanagement.executeSQL(
                                $"INSERT INTO tbl_students (student_ID, name, grade, section, date_in) " +
                                $"VALUES ('{student_ID}', 'Unknown', '', '', NOW())");
                            LogAction("ADD", "STUDENT MANAGEMENT", $"Scanned and added student {student_ID}");
                        }

                        LoadTodayStudents();
                        txtBarcode.Clear();
                        LogStudentAttendance(student_ID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Logging Attendance",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                txtBarcode.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true; // prevent beep sound
            }
        }
        private void LogStudentAttendance(string student_ID)
        {
            try
            {
                studentmanagement.executeSQL(
                    $"INSERT INTO tbl_students (student_ID, name, grade, section, date_in) " +
                    $"VALUES ('{student_ID}', '', '', '', NOW())");

                LogAction("SCAN", "STUDENT MANAGEMENT", $"Student {student_ID} scanned");

                LoadTodayStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Logging Attendance", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LogAction(string action, string module, string details)
        {
            try
            {
                studentmanagement.executeSQL(
                    $"INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                    $"VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now:hh\\:mm tt}', '{action}', '{module}', '{details}', '{username}')");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Log Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadStudentsByDate(dateTimePicker1.Value);
        }
    }
}
