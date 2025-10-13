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
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
        }

        Class1 login = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtusername.Clear();
            txtpassword.Clear();
            txtusername.Focus();
        }

        private void chkshow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkshow.Checked)
            {
                txtpassword.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '*';
            }
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtusername.Text))
                {
                    MessageBox.Show("Please enter your username.", "Missing Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtusername.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtpassword.Text))
                {
                    MessageBox.Show("Please enter your password.", "Missing Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpassword.Focus();
                    return;
                }
                string queryUser = $"SELECT * FROM tbl_accounts WHERE username = '{txtusername.Text}'";
                DataTable dtUser = login.GetData(queryUser);

                if (dtUser.Rows.Count == 0)
                {
                    MessageBox.Show("Username not found. Please check and try again.", "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtusername.Focus();
                    return;
                }
                if (dtUser.Rows[0]["status"].ToString() != "ACTIVE")
                {
                    MessageBox.Show("Your account is inactive. Please contact the administrator.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string storedPassword = dtUser.Rows[0]["password"].ToString();
                if (storedPassword != txtpassword.Text)
                {
                    MessageBox.Show("Incorrect password. Please try again.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpassword.Focus();
                    return;
                }
                frmMain mainForm = new frmMain(txtusername.Text, dtUser.Rows[0].Field<string>("usertype"));
                mainForm.Show();
                this.Hide();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on btnlogin_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnlogin_Click(sender, e);
            }
        }
    }
}
