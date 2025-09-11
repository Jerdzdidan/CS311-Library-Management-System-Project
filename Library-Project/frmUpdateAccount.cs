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
    public partial class frmUpdateAccount : Form
    {
        Class1 updateaccount = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string editusername, editpassword, editusertype, editstatus, username;

        private void frmUpdateAccount_Load(object sender, EventArgs e)
        {
            txtusername.Text = editusername;
            txtpassword.Text = editpassword;
            if (editusertype == "ADMINISTRATOR")
            {
                cmbusertype.SelectedIndex = 0;
            }
            else if (editusertype == "USER")
            {
                cmbusertype.SelectedIndex = 1;
            }
            else
            {
                cmbusertype.SelectedIndex = -1;
            }
            if (editstatus == "ACTIVE")
            {
                cmbstatus.SelectedIndex = 0;
            }
            else if (editstatus == "INACTIVE")
            {
                cmbstatus.SelectedIndex = 1;
            }
            else
            {
                cmbstatus.SelectedIndex = -1;
            }
        }
        public frmUpdateAccount(string editusername, string editpassword, string editusertype, string editstatus, string username)
        {
            InitializeComponent();
            this.editusername = editusername;
            this.editpassword = editpassword;
            this.editusertype = editusertype;
            this.editstatus = editstatus;
            this.username = username;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure want to edit this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    updateaccount.executeSQL("UPDATE tbl_accounts SET password = '" + txtpassword.Text + "' , usertype = '" + cmbusertype.Text.ToUpper() + "' , status = '" +
                        cmbstatus.Text.ToUpper() + "' WHERE username = '" + txtusername.Text + "'");
                    if (updateaccount.rowAffected > 0)
                    {
                        MessageBox.Show("Account updated.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateaccount.executeSQL("INSERT tbllogs (datelog, timelog, action, module, performedto, performedby) VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "', '" +
                            DateTime.Now.ToShortTimeString() + "' , 'UPDATE', 'ACCOUNTS MANAGEMENT', '" + txtusername.Text + "', '" + username + "')");
                        this.Close();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on btnupdate_click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
