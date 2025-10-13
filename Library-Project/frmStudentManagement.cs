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
        private int currentPage = 1;
        private int pageSize = 20;
        public frmStudentManagement(string username)
        {
            InitializeComponent();
            this.username = username;
            txtSearch.TextChanged += txtsearch_TextChanged;
            dgvStudents.CellClick += dgvStudents_CellClick;
        }
        private void frmStudentManagement_Load(object sender, EventArgs e)
        {
            LoadStudents();

            if (dgvStudents.Columns.Count >= 5)
            {
                dgvStudents.Columns[0].HeaderText = "Student Number";
                dgvStudents.Columns[1].HeaderText = "Name";
                dgvStudents.Columns[2].HeaderText = "Grade";
                dgvStudents.Columns[3].HeaderText = "Section";
                dgvStudents.Columns[4].HeaderText = "Date In";
            }
        }
        private void LoadStudents()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize; // pageSize = 15
                string searchCondition = "";
                string keyword = txtSearch.Text.Trim().Replace("'", "''");

                if (!string.IsNullOrEmpty(keyword))
                {
                    searchCondition = $" WHERE (name LIKE '%{keyword}%' OR student_ID LIKE '%{keyword}%')";
                }

                // Load one extra record to check if there's a next page
                string query = $"SELECT student_ID, name, grade, section, date_in FROM tbl_students {searchCondition} ORDER BY name ASC LIMIT {pageSize + 1} OFFSET {offset}";
                DataTable dt = studentmanagement.GetData(query);

                bool hasNextPage = dt.Rows.Count > pageSize;
                if (hasNextPage)
                    dt.Rows.RemoveAt(dt.Rows.Count - 1); // remove extra row

                dgvStudents.DataSource = dt;
                dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Update pagination buttons
                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = hasNextPage;

                // Show page info
                lblPageInfo.Text = $"Page {currentPage}" + (hasNextPage ? " →" : "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadStudents();
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
                currentPage = 1;
                LoadStudents();
                LogAction("ADD", "STUDENT MANAGEMENT", "Added a new student record");
            };
            addForm.ShowDialog();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentCell == null || dgvStudents.CurrentRow == null)
            {
                MessageBox.Show("Please select a student to update.",
                                "Invalid Selection",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = dgvStudents.CurrentCell.RowIndex;
            if (rowIndex < 0 || rowIndex >= dgvStudents.Rows.Count)
            {
                MessageBox.Show("Please select a valid row to update.",
                                "Invalid Selection",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Get the student details
            string editID = dgvStudents.Rows[rowIndex].Cells["student_ID"].Value.ToString();
            string editname = dgvStudents.Rows[rowIndex].Cells["name"].Value.ToString();
            string editgrade = dgvStudents.Rows[rowIndex].Cells["grade"].Value.ToString();
            string editsection = dgvStudents.Rows[rowIndex].Cells["section"].Value.ToString();

            // Open the update form
            frmUpdateStudent updatestudentform = new frmUpdateStudent(username, editID, editname, editgrade, editsection);

            // When update form closes, refresh the student list and keep the pagination
            updatestudentform.FormClosed += (s, args) =>
            {
                LoadStudents(); // keep same page
                LogAction("UPDATE", "STUDENT MANAGEMENT", $"Updated student {editname}");
            };

            updatestudentform.ShowDialog();
        }
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (row >= 0 && row < dgvStudents.Rows.Count)
            {
                string id = dgvStudents.Rows[row].Cells["student_ID"].Value.ToString();
                string name = dgvStudents.Rows[row].Cells["name"].Value.ToString();

                DialogResult dr = MessageBox.Show(
                    $"Are you sure you want to delete student '{name}'?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        // Execute delete query
                        studentmanagement.executeSQL($"DELETE FROM tbl_students WHERE student_ID = '{id}'");

                        if (studentmanagement.rowAffected > 0)
                        {
                            // Refresh the student list with pagination
                            LoadStudents();

                            // Log the deletion
                            LogAction("DELETE", "STUDENT MANAGEMENT", $"Deleted student {name}");

                            MessageBox.Show("Student deleted successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No student was deleted. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting student: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            currentPage = 1;
            LoadStudents();
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

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow != null)
            {
                string studentID = dgvStudents.CurrentRow.Cells["student_ID"].Value.ToString();
                string name = dgvStudents.CurrentRow.Cells["name"].Value.ToString();
                string grade = dgvStudents.CurrentRow.Cells["grade"].Value.ToString();
                string section = dgvStudents.CurrentRow.Cells["section"].Value.ToString();

                frmStudentHistory historyForm = new frmStudentHistory(studentID, name, grade, section);
                historyForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a student to view history.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadStudents();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadStudents();
            }
        }

   
    }
}
