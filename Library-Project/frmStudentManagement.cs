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
        }

        private void frmStudentManagement_Load(object sender, EventArgs e)
        {
            LoadTodayStudents();
            txtBarcode.Focus();
        }
        private void LoadTodayStudents()
        {
            try
            {
                DataTable dt = studentmanagement.GetData(
                    "SELECT student_id, name, grade, section, date_in " +
                    "FROM tbl_student " +
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
                DataTable dt = studentmanagement.GetData(
                    "SELECT student_id, name, grade, section, date_in " +
                    "FROM tbl_student " +
                    "WHERE (name LIKE '%" + txtSearch.Text + "%' OR student_id LIKE '%" + txtSearch.Text + "%') " +
                    "AND DATE(date_in) = CURDATE()"
                );
                dgvStudents.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (row >= 0)
            {
                string id = dgvStudents.Rows[row].Cells["student_id"].Value.ToString();
                string name = dgvStudents.Rows[row].Cells["name"].Value.ToString();
                string grade = dgvStudents.Rows[row].Cells["grade"].Value.ToString();
                string section = dgvStudents.Rows[row].Cells["section"].Value.ToString();

                frmUpdateStudent updateForm = new frmUpdateStudent(id, name, grade, section, username);
                updateForm.FormClosed += (s, args) =>
                {
                    LoadTodayStudents();
                    LogAction("UPDATE", "STUDENT MANAGEMENT", $"Updated student {name}");
                };
                updateForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a student to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (row >= 0)
            {
                string id = dgvStudents.Rows[row].Cells["student_id"].Value.ToString();
                string name = dgvStudents.Rows[row].Cells["name"].Value.ToString();

                DialogResult dr = MessageBox.Show($"Are you sure you want to delete student '{name}'?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        studentmanagement.executeSQL($"DELETE FROM tbl_student WHERE student_id = '{id}'");
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
                LogStudentAttendance(txtBarcode.Text.Trim());
                txtBarcode.Clear();
            }
        }
        private void LogStudentAttendance(string student_ID)
        {
            try
            {
                DataTable check = studentmanagement.GetData($"SELECT * FROM tbl_student WHERE student_id = '{student_ID}'");

                if (check.Rows.Count > 0)
                {
                    studentmanagement.executeSQL($"UPDATE tbl_student SET date_in = NOW() WHERE student_id = '{student_ID}'");
                    LogAction("SCAN", "STUDENT MANAGEMENT", $"Student {student_ID} scanned");
                }
                else
                {
                    studentmanagement.executeSQL($"INSERT INTO tbl_student (student_id, name, grade, section, date_in) " +
                                                 $"VALUES ('{student_ID}', 'Unknown', '', '', NOW())");
                    LogAction("ADD", "STUDENT MANAGEMENT", $"Scanned and added student {student_ID}");
                }

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

    }
}
