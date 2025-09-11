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
        }
        Class1 main = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");

        private void accountManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccountsManagement accountsmanagementform = new frmAccountsManagement(username);
            accountsmanagementform.MdiParent = this;
            accountsmanagementform.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                frmlogin login = new frmlogin();
                login.Show();
                this.Hide();
            }
        }

        private void bookManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBooksManagement bookmanagementform = new frmBooksManagement(username);
            bookmanagementform.MdiParent = this;
            bookmanagementform.Show();
        }
    }
}
