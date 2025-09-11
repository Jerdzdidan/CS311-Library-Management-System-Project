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
                DataTable dt = login.GetData("SELECT * FROM tbl_accounts WHERE username = '" + txtusername.Text + "'AND password = '" + txtpassword.Text + "'AND status = 'ACTIVE'");
                if (dt.Rows.Count > 0)
                {
                    frmMain mainForm = new frmMain(txtusername.Text, dt.Rows[0].Field<string>("usertype"));
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password or account is inactive.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

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
