using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ticket_management;

namespace Library_Project
{
    public partial class frmUpdateStudent : Form
    {
        Class1 updatestudent = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string editID, editname, editgrade, editsection, username;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmUpdateStudent(string username, string editID, string editname, string editgrade, string editsection)
        {
            InitializeComponent();
            this.username = username;
            this.editID = editID;
            this.editname = editname;
            this.editgrade = editgrade;
            this.editsection = editsection;
        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to update this student?",
                                                  "Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    updatestudent.executeSQL("UPDATE tbl_students SET " +
                        "student_ID = '" + txtstudentno.Text + "', " +
                        "studentname = '" + txtstudentname.Text.Replace("'", "''") + "', " +
                        "grade = '" + cmbgrade.Text.ToUpper() + "', " +
                        "section = '" + cmbsection.Text.ToUpper() + "' " +
                        "WHERE student_ID = '" + editID + "'");

                    if (updatestudent.rowAffected > 0)
                    {
                        MessageBox.Show("Student record updated.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updatestudent.executeSQL(
                            "INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                            "VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "', '" +
                            DateTime.Now.ToShortTimeString() + "', 'UPDATE', 'STUDENTS MANAGEMENT', '" +
                            txtstudentname.Text.Replace("'", "''") + "', '" + username + "')");

                        this.Close();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on btnupdate_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmUpdateStudent_Load(object sender, EventArgs e)
        {
            txtstudentno.Text = editID;
            txtstudentname.Text = editname;
            if (editgrade == "1")
            {
                cmbgrade.SelectedIndex = 0;
            }
            else if (editgrade == "2")
            {
                cmbgrade.SelectedIndex = 1;
            }
            else if (editgrade == "3")
            {
                cmbgrade.SelectedIndex = 2;
            }
            else if (editgrade == "4")
            {
                cmbgrade.SelectedIndex = 3;
            }
            else if (editgrade == "5")
            {
                cmbgrade.SelectedIndex = 4;
            }
            else if (editgrade == "6")
            {
                cmbgrade.SelectedIndex = 5;
            }
            else
            {
                cmbgrade.SelectedIndex = -1;
            }
            if (editsection == "BONIFACIO")
            {
                cmbsection.SelectedIndex = 0;
            }
            else if (editsection == "PYTHAGORAS")
            {
                cmbsection.SelectedIndex = 1;
            }
            else
            {
                cmbsection.SelectedIndex = -1;
            }
        }
    }
}
