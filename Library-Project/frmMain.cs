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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace Library_Project
{
    public partial class frmMain : Form
    {
        private string username, usertype;
        public frmMain(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;

            ApplyUserAccess();
        }
        Class1 main = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private void ApplyUserAccess()
        {
            if (usertype.ToUpper() == "STAFF")
            {
                btntransac.Enabled = true;
                btnresources.Enabled = true;
                btnlogout.Enabled = true;
                btnlogs.Enabled = true;

                btnaccounts.Enabled = false;
                btnaccounts.Visible = false;  
            }
            else
            {
                btntransac.Enabled = true;
                btnresources.Enabled = true;
                btnlogout.Enabled = true;
                btnlogs.Enabled = true; 
                btnaccounts.Enabled = true;
                btnaccounts.Visible = true;
            }
        }
        private void btnlogs_Click(object sender, EventArgs e)
        {
            frmBookLogs booklogsform = new frmBookLogs(username);
            booklogsform.MdiParent = this;
            booklogsform.Show();
        }
        private void btnresources_Click(object sender, EventArgs e)
        {
            frmBooksManagement bookmanagementform = new frmBooksManagement(username);
            bookmanagementform.MdiParent = this;
            bookmanagementform.Show();
        }
        private void btnaccounts_Click(object sender, EventArgs e)
        {
            frmAccountsManagement accountsmanagementform = new frmAccountsManagement(username);
            accountsmanagementform.MdiParent = this;
            accountsmanagementform.Show();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                frmlogin login = new frmlogin();
                login.Show();
                this.Hide();
            }
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout aboutfrm = new frmAbout(username);
            aboutfrm.MdiParent = this;
            aboutfrm.Show();
        }
        private void btntransac_Click(object sender, EventArgs e)
        {
            frmTransactions transactionsform = new frmTransactions(username);
            transactionsform.MdiParent = this;
            transactionsform.Show();
        }
    }
}
