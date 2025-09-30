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
        Class1 addstudent = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        public frmAddStudent(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private int errorcount;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorcount = 0;
            if (string.IsNullOrEmpty(txtstudentname.Text))
            {
                errorProvider1.SetError(txtstudentname, "Student Name is empty.");
                errorcount++;
            }

            if (string.IsNullOrEmpty(txtstudentno.Text))
            {
                errorProvider1.SetError(txtstudentno, "Student Number is empty.");
                errorcount++;
            }

            if (cmbgrade.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbgrade, "Select Grade.");
                errorcount++;
            }

            if (cmbsection.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbsection, "Select Section.");
                errorcount++;
            }
            try
            {
                DataTable dt = addstudent.GetData(
                    $"SELECT * FROM tbl_students WHERE student_ID = '{txtstudentno.Text.Trim()}'");

                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtstudentno, "Student Number is already registered.");
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR checking duplicates",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (errorcount == 0)
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to add this student?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        string insertQuery = $@"
                    INSERT INTO tbl_students (student_ID, name, grade, section, date_in)
                    VALUES ('{txtstudentno.Text.Trim()}',
                            '{txtstudentname.Text.Trim()}',
                            '{cmbgrade.Text.ToUpper()}',
                            '{cmbsection.Text.ToUpper()}',
                            '{DateTime.Now:yyyy/MM/dd}')";

                        addstudent.executeSQL(insertQuery);

                        if (addstudent.rowAffected > 0)
                        {
                            addstudent.executeSQL($@"
                        INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                        VALUES ('{DateTime.Now:yyyy/MM/dd}',
                                '{DateTime.Now:hh\\:mm tt}',
                                'ADD',
                                'STUDENT MANAGEMENT',
                                '{txtstudentname.Text.Trim()} ({txtstudentno.Text.Trim()})',
                                '{username}')");

                            MessageBox.Show("New student account added successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add student.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR adding student",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
