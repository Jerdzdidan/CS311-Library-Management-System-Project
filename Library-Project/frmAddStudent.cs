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
    public partial class frmAddStudent : Form
    {
        private readonly Class1 addstudent = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private readonly string username;
        private int errorcount;
        public frmAddStudent(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorcount = 0;

            // Input validation
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                errorProvider1.SetError(txtBarcode, "Student Number (Barcode) is required.");
                errorcount++;
            }

            if (string.IsNullOrWhiteSpace(txtstudentname.Text))
            {
                errorProvider1.SetError(txtstudentname, "Student Name is required.");
                errorcount++;
            }

            if (cmbgrade.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbgrade, "Please select a Grade.");
                errorcount++;
            }

            if (cmbsection.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbsection, "Please select a Section.");
                errorcount++;
            }

            // 🔹 Check barcode in both tbl_students and tbl_teacher
            try
            {
                string barcode = txtBarcode.Text.Trim();

                string checkQuery = $@"
                    SELECT 'STUDENT' AS Source FROM tbl_students WHERE student_ID = '{barcode}'
                    UNION ALL
                    SELECT 'TEACHER' AS Source FROM tbl_teacher WHERE teacher_ID = '{barcode}'";

                DataTable dt = addstudent.GetData(checkQuery);

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

            // Confirm and add student
            DialogResult dr = MessageBox.Show("Are you sure you want to add this student?",
                                              "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    string insertQuery = $@"
                        INSERT INTO tbl_students (student_ID, name, grade, section, date_in)
                        VALUES (
                            '{txtBarcode.Text.Trim()}',
                            '{txtstudentname.Text.Trim().Replace("'", "''")}',
                            '{cmbgrade.Text.ToUpper().Replace("'", "''")}',
                            '{cmbsection.Text.ToUpper().Replace("'", "''")}',
                            '{DateTime.Now:yyyy/MM/dd}'
                        )";

                    addstudent.executeSQL(insertQuery);

                    if (addstudent.rowAffected > 0)
                    {
                        string logQuery = $@"
                            INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                            VALUES (
                                '{DateTime.Now:yyyy/MM/dd}',
                                '{DateTime.Now:hh\\:mm tt}',
                                'ADD',
                                'STUDENT MANAGEMENT',
                                '{txtstudentname.Text.Trim().Replace("'", "''")} ({txtBarcode.Text.Trim()})',
                                '{username}'
                            )";

                        addstudent.executeSQL(logQuery);

                        MessageBox.Show("New student added successfully!",
                                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add student. Please try again.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding student:\n" + ex.Message,
                                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            txtstudentname.Clear();
            cmbgrade.SelectedIndex = -1;
            cmbsection.SelectedIndex = -1;
            errorProvider1.Clear();
            txtBarcode.Focus();
        }

        private void frmAddStudent_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtBarcode;
        }
    }
}
