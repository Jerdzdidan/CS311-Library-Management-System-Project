using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ticket_management;

namespace Library_Project
{
    public partial class frmAddAccount : Form
    {
        Class1 addaccount = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        public frmAddAccount(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private int errorcount;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorcount = 0;

            if (string.IsNullOrEmpty(txtusername.Text))
            {
                errorProvider1.SetError(txtusername, "Username is empty.");
                errorcount++;
            }

            if (string.IsNullOrEmpty(txtpassword.Text))
            {
                errorProvider1.SetError(txtpassword, "Password is empty.");
                errorcount++;
            }

            if (cmbUserType.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbUserType, "Select usertype.");
                errorcount++;
            }

            try
            {
                DataTable dt = addaccount.GetData("SELECT * FROM tblaccounts WHERE username = '" + txtusername.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtusername, "Username is already in use.");
                    errorcount++;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on btnAdd_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (errorcount == 0)
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to add this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        addaccount.executeSQL("INSERT INTO tblaccounts (username, password, usertype, status, createdby, datecreated) VALUES ('" + txtusername.Text + "','" + txtpassword.Text + "','" + cmbUserType.Text.ToUpper() + "', 'ACTIVE', '" + username + "', '" + DateTime.Now.ToShortDateString() + "')");
                        if (addaccount.rowAffected > 0)
                        {
                            MessageBox.Show("New account added.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on adding new account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
