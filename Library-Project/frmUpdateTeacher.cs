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
    public partial class frmUpdateTeacher : Form
    {
        Class1 updateteacher = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string editID, editName, editSubject, username;

        private void btnCancel_Click(object sender, EventArgs e)
        {
               this.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to update this teacher?",
                                                  "Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    string selectedSubject = cmbSubject.SelectedItem?.ToString() ?? "";

                    updateteacher.executeSQL($@"
                UPDATE tbl_teacher
                SET 
                    teacher_ID = '{txtTeacherID.Text.Trim()}',
                    name = '{txtTeacherName.Text.Replace("'", "''")}',
                    subject = '{selectedSubject.Replace("'", "''")}'
                WHERE teacher_ID = '{editID}'");

                    if (updateteacher.rowAffected > 0)
                    {
                        MessageBox.Show("Teacher record updated successfully!",
                                        "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        updateteacher.executeSQL($@"
                    INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                    VALUES ('{DateTime.Now:yyyy/MM/dd}',
                            '{DateTime.Now:hh\\:mm tt}',
                            'UPDATE',
                            'TEACHER MANAGEMENT',
                            '{txtTeacherName.Text.Replace("'", "''")} ({txtTeacherID.Text.Trim()})',
                            '{username}')");

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on btnUpdate_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public frmUpdateTeacher(string username, string editID, string editName, string editSubject)
        {
            InitializeComponent();
            this.username = username;
            this.editID = editID;
            this.editName = editName;
            this.editSubject = editSubject;
        }

        private void frmUpdateTeacher_Load(object sender, EventArgs e)
        {
            txtTeacherID.Text = editID;
            txtTeacherName.Text = editName;

            cmbSubject.Items.Clear();
            cmbSubject.Items.AddRange(new string[]
            {
            "MATHEMATICS", "SCIENCE", "ENGLISH", "FILIPINO", "MAPEH", "ARALING PANLIPUNAN"
            });

            if (!string.IsNullOrEmpty(editSubject) && cmbSubject.Items.Contains(editSubject))
                cmbSubject.SelectedItem = editSubject;
            else
                cmbSubject.Text = editSubject;
        }

    }
}
