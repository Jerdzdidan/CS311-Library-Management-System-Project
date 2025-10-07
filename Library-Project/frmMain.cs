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
        private void frmMain_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            ApplyUserAccess();
        }
        private void ApplyUserAccess()
        {
            if (usertype.ToUpper() == "STAFF")
            {
                btntransac.Enabled = true;
                btnresources.Enabled = true;
                btnlogout.Enabled = true;
                btnlogs.Enabled = true;
                btnStudentsManagement.Enabled = true;

                btnaccounts.Enabled = false;
                btnaccounts.Visible = false;
            }
            else
            {
                btntransac.Enabled = true;
                btnresources.Enabled = true;
                btnlogout.Enabled = true;
                btnlogs.Enabled = true;
                btnStudentsManagement.Enabled = true;
                btnaccounts.Enabled = true;
                btnaccounts.Visible = true;
            }
        }
        private void RemoveChildren()
        {
            foreach (Form child in this.MdiChildren)
            {
                child.MdiParent = null;
                child.Close();
            }
        }
        private void btnlogs_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmBookLogs logs = new frmBookLogs(username);
            logs.MdiParent = this;
            logs.Dock = DockStyle.Fill;
            logs.Show();
        }
        private void btnresources_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmBooksManagement books = new frmBooksManagement(username);
            books.MdiParent = this;
            books.Dock = DockStyle.Fill;
            books.Show();
        }
        private void btnaccounts_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmAccountsManagement accounts = new frmAccountsManagement(username);
            accounts.MdiParent = this;
            accounts.Dock = DockStyle.Fill;
            accounts.Show();
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmAbout about = new frmAbout(username);
            about.MdiParent = this;
            about.Dock = DockStyle.Fill;
            about.Show();
        }
        private void btntransac_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmTransactions transac = new frmTransactions(username);
            transac.MdiParent = this;
            transac.Dock = DockStyle.Fill;
            transac.Show();
        }

        private void btnStudentsManagement_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmStudentManagement studentmanagement = new frmStudentManagement(username);
            studentmanagement.MdiParent = this;
            studentmanagement.Dock = DockStyle.Fill;
            studentmanagement.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmDashboard frmDashboard = new frmDashboard();
            frmDashboard.MdiParent = this;
            frmDashboard.Dock = DockStyle.Fill;
            frmDashboard.Show();
        }

        private void btnResource_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmResources resources = new frmResources(username);
            resources.MdiParent = this;
            resources.Dock = DockStyle.Fill;
            resources.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RemoveChildren();
            frmTeachersManagement teachers = new frmTeachersManagement(username);
            teachers.MdiParent = this;
            teachers.Dock = DockStyle.Fill;
            teachers.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            RemoveChildren();
            frmAttendance attendance = new frmAttendance(username);
            attendance.MdiParent = this;
            attendance.Dock = DockStyle.Fill;
            attendance.Show();
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
    }
}
