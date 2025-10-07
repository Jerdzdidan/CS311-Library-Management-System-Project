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
        Class1 addteacher = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        public frmAddTeacher(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private int errorcount;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorcount = 0;

            if (string.IsNullOrEmpty(txtTeacherID.Text))
            {
                errorProvider1.SetError(txtTeacherID, "Teacher ID is empty.");
                errorcount++;
            }

            if (string.IsNullOrEmpty(txtTeacherName.Text))
            {
                errorProvider1.SetError(txtTeacherName, "Teacher Name is empty.");
                errorcount++;
            }

            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                errorProvider1.SetError(txtSubject, "Subject is empty.");
                errorcount++;
            }

            try
            {
                DataTable dt = addteacher.GetData(
                    $"SELECT * FROM tbl_teacher WHERE teacher_ID = '{txtTeacherID.Text.Trim()}'");

                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtTeacherID, "Teacher ID is already registered.");
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR checking duplicates", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (errorcount == 0)
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to add this teacher?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        string insertQuery = $@"
                            INSERT INTO tbl_teacher (teacher_ID, name, subject, date_in)
                            VALUES ('{txtTeacherID.Text.Trim()}',
                                    '{txtTeacherName.Text.Trim()}',
                                    '{txtSubject.Text.Trim()}',
                                    '{DateTime.Now:yyyy/MM/dd}')";

                        addteacher.executeSQL(insertQuery);

                        if (addteacher.rowAffected > 0)
                        {
                            addteacher.executeSQL($@"
                                INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                                VALUES ('{DateTime.Now:yyyy/MM/dd}',
                                        '{DateTime.Now:hh\\:mm tt}',
                                        'ADD',
                                        'TEACHER MANAGEMENT',
                                        '{txtTeacherName.Text.Trim()} ({txtTeacherID.Text.Trim()})',
                                        '{username}')");

                            MessageBox.Show("New teacher added successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add teacher.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR adding teacher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTeacherID.Clear();
            txtTeacherName.Clear();
            txtSubject.Clear();
            errorProvider1.Clear();
            txtTeacherID.Focus();
        }

        private void frmAddTeacher_Load(object sender, EventArgs e)
        {

        }
    }
}
