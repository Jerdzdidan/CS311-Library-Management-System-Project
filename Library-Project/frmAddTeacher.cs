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
    public partial class frmAddTeacher : Form
    {
        private readonly Class1 addteacher = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private readonly string username;
        private int errorcount;
        

        public frmAddTeacher(string username)
        {
            InitializeComponent();
            this.username = username;
            txtBarcode.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorcount = 0;

            // Input validation
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                errorProvider1.SetError(txtBarcode, "Teacher ID (Barcode) is required.");
                errorcount++;
            }

            if (string.IsNullOrWhiteSpace(txtTeacherName.Text))
            {
                errorProvider1.SetError(txtTeacherName, "Teacher Name is required.");
                errorcount++;
            }

            if (cmbSubject.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbSubject, "Please select a subject.");
                errorcount++;
            }

            // 🔹 Check barcode in both tbl_teacher and tbl_students
            try
            {
                string barcode = txtBarcode.Text.Trim();

                string checkQuery = $@"
                    SELECT 'TEACHER' AS Source FROM tbl_teacher WHERE teacher_ID = '{barcode}'
                    UNION ALL
                    SELECT 'STUDENT' AS Source FROM tbl_students WHERE student_ID = '{barcode}'";

                DataTable dt = addteacher.GetData(checkQuery);

                if (dt.Rows.Count > 0)
                {
                    string usedBy = dt.Rows[0]["Source"].ToString();
                    errorProvider1.SetError(txtBarcode, $"This barcode is already used by a {usedBy.ToLower()}.");
                    MessageBox.Show($"This barcode is already registered under a {usedBy.ToLower()}. Please use a unique barcode.",
                        "Duplicate Barcode Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating barcode:\n" + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (errorcount > 0) return;

            // Confirm and insert
            DialogResult dr = MessageBox.Show("Are you sure you want to add this teacher?",
                                              "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    string insertQuery = $@"
                        INSERT INTO tbl_teacher (teacher_ID, name, subject, date_in)
                        VALUES (
                            '{txtBarcode.Text.Trim()}',
                            '{txtTeacherName.Text.Trim().Replace("'", "''")}',
                            '{cmbSubject.SelectedItem.ToString().Replace("'", "''")}',
                            '{DateTime.Now:yyyy/MM/dd}'
                        )";

                    addteacher.executeSQL(insertQuery);

                    if (addteacher.rowAffected > 0)
                    {
                        string logQuery = $@"
                            INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                            VALUES (
                                '{DateTime.Now:yyyy/MM/dd}',
                                '{DateTime.Now:hh\\:mm tt}',
                                'ADD',
                                'TEACHER MANAGEMENT',
                                '{txtTeacherName.Text.Trim().Replace("'", "''")} ({txtBarcode.Text.Trim()})',
                                '{username}'
                            )";

                        addteacher.executeSQL(logQuery);

                        MessageBox.Show("New teacher added successfully!",
                                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add teacher. Please try again.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding teacher:\n" + ex.Message,
                                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            txtTeacherName.Clear();
            cmbSubject.SelectedIndex = -1;
            errorProvider1.Clear();
            txtBarcode.Focus();
        }

        private void frmAddTeacher_Load(object sender, EventArgs e)
        {
            cmbSubject.Items.Clear();
            cmbSubject.Items.AddRange(new string[]
            {
                "MATHEMATICS",
                "SCIENCE",
                "ENGLISH",
                "FILIPINO",
                "MAPEH",
                "ARALING PANLIPUNAN"
            });
            this.ActiveControl = txtBarcode;
        }
    }
}
