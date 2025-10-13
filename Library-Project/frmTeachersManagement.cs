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
        private int currentPage = 1;
        private int pageSize = 15;
        public frmTeachersManagement(string username)
        {
            InitializeComponent();
            this.username = username;

            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvTeachers.CellClick += dgvTeachers_CellClick_1;
            dgvTeachers.DataBindingComplete += dgvTeachers_DataBindingComplete;
        }
        public void RefreshTeachersPublic()
        {
            LoadTodayTeacher();
        }
        private void frmTeachersManagement_Load(object sender, EventArgs e)
        {
            LoadTeachers();

            if (dgvTeachers.Columns.Count >= 4)
            {
                dgvTeachers.Columns[0].HeaderText = "Teacher ID";
                dgvTeachers.Columns[1].HeaderText = "Name";
                dgvTeachers.Columns[2].HeaderText = "Subject";
                dgvTeachers.Columns[3].HeaderText = "Date In";
            }
        }
        private void LoadTeachers()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;
                string searchCondition = "";
                string keyword = txtSearch.Text.Trim().Replace("'", "''");

                if (!string.IsNullOrEmpty(keyword))
                {
                    searchCondition = $" WHERE (name LIKE '%{keyword}%' OR teacher_ID LIKE '%{keyword}%' OR subject LIKE '%{keyword}%')";
                }

                // Load one extra record to detect next page
                string query = $"SELECT teacher_ID, name, subject, date_in FROM tbl_teacher {searchCondition} ORDER BY name ASC LIMIT {pageSize + 1} OFFSET {offset}";
                DataTable dt = teachersmanagement.GetData(query);

                bool hasNextPage = dt.Rows.Count > pageSize;
                if (hasNextPage)
                    dt.Rows.RemoveAt(dt.Rows.Count - 1); // Remove extra row

                dgvTeachers.DataSource = dt;
                dgvTeachers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 🔹 Update pagination controls
                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = hasNextPage;

                lblPageInfo.Text = $"Page {currentPage}" + (hasNextPage ? " →" : "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teachers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            currentPage = 1;
            LoadTeachers();
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
                currentPage = 1;
                LoadTeachers();
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

            string editID = dgvTeachers.CurrentRow.Cells["teacher_ID"].Value.ToString();
            string editName = dgvTeachers.CurrentRow.Cells["name"].Value.ToString();
            string editSubject = dgvTeachers.CurrentRow.Cells["subject"].Value.ToString();

            frmUpdateTeacher updateForm = new frmUpdateTeacher(username, editID, editName, editSubject);
            updateForm.FormClosed += (s, args) =>
            {
                LoadTeachers(); // keep same page
                LogAction("UPDATE", "TEACHER MANAGEMENT", $"Updated teacher {editName}");
            };
            updateForm.ShowDialog();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (row >= 0 && row < dgvTeachers.Rows.Count)
            {
                string id = dgvTeachers.Rows[row].Cells["teacher_ID"].Value.ToString();
                string name = dgvTeachers.Rows[row].Cells["name"].Value.ToString();

                DialogResult dr = MessageBox.Show($"Are you sure you want to delete teacher '{name}'?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        teachersmanagement.executeSQL($"DELETE FROM tbl_teacher WHERE teacher_ID = '{id}'");

                        if (teachersmanagement.rowAffected > 0)
                        {
                            LoadTeachers();
                            LogAction("DELETE", "TEACHER MANAGEMENT", $"Deleted teacher {name}");
                            MessageBox.Show("Teacher deleted successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting teacher: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            currentPage = 1;
            LoadTeachers();
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
                    string subject = row.Cells["subject"].Value.ToString().Trim().ToUpper();

                    switch (subject)
                    {
                        case "MATHEMATICS":
                            row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#8DA9FC");
                            break;
                        case "SCIENCE":
                            row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C7FFDE");
                            break;
                        case "ENGLISH":
                            row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E3C7FF");
                            break;
                        case "FILIPINO":
                            row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFE2C7");
                            break;
                        case "MAPEH":
                            row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFFC7");
                            break;
                        case "ARALING PANLIPUNAN":
                            row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFC7C7");
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }
                }
            }
        }

        private void dgvTeachers_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadTeachers();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadTeachers();
        }
    }
}
