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
    public partial class frmTeachersManagement : Form
    {
        Class1 teachersmanagement = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        private int row;
        public frmTeachersManagement(string username)
        {
            InitializeComponent();
            this.username = username;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvTeachers.CellClick += dgvTeachers_CellClick;

            dgvTeachers.DataBindingComplete += dgvTeachers_DataBindingComplete;
        }
        public void RefreshTeachersPublic()
        {
            LoadTodayTeacher();
        }
        private void frmTeachersManagement_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.Date;
            LoadTeachersByDate(dateTimePicker1.Value);

            dgvTeachers.Columns[0].HeaderText = "Teacher ID";
            dgvTeachers.Columns[1].HeaderText = "Name";
            dgvTeachers.Columns[2].HeaderText = "Subject";
            dgvTeachers.Columns[3].HeaderText = "Date In";
        }
        private void LoadTeachersByDate(DateTime date)
        {
            try
            {
                string dateString = date.ToString("yyyy/MM/dd");
                string query = "SELECT teacher_ID, name, subject, date_in " +
                               "FROM tbl_teacher " +
                               $"WHERE DATE(date_in) = '{dateString}' " +
                               "ORDER BY date_in DESC";

                DataTable dt = teachersmanagement.GetData(query);
                dgvTeachers.DataSource = dt;
                dgvTeachers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teachers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTodayTeacher()
        {
            try
            {
                DataTable dt = teachersmanagement.GetData(
                    "SELECT teacher_ID, name, subject, date_in " +
                    "FROM tbl_teacher " +
                    "WHERE DATE(date_in) = CURDATE() " +
                    "ORDER BY date_in DESC"
                );
                dgvTeachers.DataSource = dt;
                dgvTeachers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading teachers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim().Replace("'", "''");
                string dateString = dateTimePicker1.Value.ToString("yyyy/MM/dd");

                string query = "SELECT teacher_ID, name, subject, date_in " +
                               "FROM tbl_teacher " +
                               $"WHERE DATE(date_in) = '{dateString}' ";

                if (!string.IsNullOrEmpty(keyword))
                {
                    query += "AND (name LIKE '%" + keyword + "%' OR teacher_ID LIKE '%" + keyword + "%') ";
                }

                query += "ORDER BY date_in DESC";

                DataTable dt = teachersmanagement.GetData(query);
                dgvTeachers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTeachers_CellClick(object sender, DataGridViewCellEventArgs e)
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
            frmAddTeacher addForm = new frmAddTeacher(username);
            addForm.FormClosed += (s, args) =>
            {
                LoadTodayTeacher();
                LogAction("ADD", "TEACHER MANAGEMENT", "Added a new teacher record");
            };
            addForm.ShowDialog();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.CurrentRow == null)
            {
                MessageBox.Show("Please select a teacher to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string editID = dgvTeachers.CurrentRow.Cells[0].Value.ToString();
            string editName = dgvTeachers.CurrentRow.Cells[1].Value.ToString();
            string editSubject = dgvTeachers.CurrentRow.Cells[2].Value.ToString();

            frmUpdateTeacher updateForm = new frmUpdateTeacher(username, editID, editName, editSubject);
            updateForm.FormClosed += (s, args) => LoadTodayTeacher();
            updateForm.Show();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (row >= 0 && row < dgvTeachers.Rows.Count)
            {
                string id = dgvTeachers.Rows[row].Cells["teacher_ID"].Value.ToString();
                string name = dgvTeachers.Rows[row].Cells["name"].Value.ToString();

                DialogResult dr = MessageBox.Show($"Are you sure you want to delete teacher '{name}'?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        teachersmanagement.executeSQL($"DELETE FROM tbl_teacher WHERE teacher_ID = '{id}'");

                        if (teachersmanagement.rowAffected > 0)
                        {
                            LoadTodayTeacher();
                            LogAction("DELETE", "TEACHER MANAGEMENT", $"Deleted teacher {name}");
                            MessageBox.Show("Teacher deleted successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Please select a teacher to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadTodayTeacher();
        }
        private void LogAction(string action, string module, string details)
        {
            try
            {
                teachersmanagement.executeSQL(
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
            LoadTeachersByDate(dateTimePicker1.Value);
        }
        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.CurrentRow != null)
            {
                string teacherID = dgvTeachers.CurrentRow.Cells["teacher_ID"].Value.ToString();
                string name = dgvTeachers.CurrentRow.Cells["name"].Value.ToString();
                string subject = dgvTeachers.CurrentRow.Cells["subject"].Value.ToString();

                frmTeacherHistory historyForm = new frmTeacherHistory(teacherID, name, subject);
                historyForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a teacher to view history.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dgvTeachers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvTeachers.Rows)
            {
                if (row.Cells["subject"].Value != null)
                {
                    string userType = row.Cells["subject"].Value.ToString().Trim().ToUpper();

                    if (userType == "MATHEMATICS")
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#8DA9FC");
                    }
                    else if (userType == "SCIENCE")
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C7FFDE");
                    }
                    else if (userType == "ENGLISH")
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E3C7FF");
                    }
                    else if (userType == "FILIPINO")
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFE2C7");
                    }
                    else if (userType == "MAPEH")
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFFC7");
                    }
                    else if (userType == "ARALING PANLIPUNAN")
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFC7C7");
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }
    }
}
