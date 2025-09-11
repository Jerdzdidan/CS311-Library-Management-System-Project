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
    public partial class frmAccountsManagement : Form
    {
        private string username;
        Class1 accounts = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        public frmAccountsManagement(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private void frmAccountsManagement_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = accounts.GetData("SELECT * FROM tbl_accounts ORDER BY username");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["password"].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on frmAccountsManagement_load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = accounts.GetData("SELECT * FROM tbl_accounts WHERE username LIKE '%" + txtsearch.Text + "%' OR usertype LIKE '%" + txtsearch.Text + "%' ORDER BY username");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on txtsearch_TextChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int row;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = (int)e.RowIndex;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on datagridview1_cellclick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnadd_Click(object sender, EventArgs e)
        {

        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            string editusername = dataGridView1.Rows[row].Cells[0].Value.ToString();
            string editpassword = dataGridView1.Rows[row].Cells[1].Value.ToString();
            string editusertype = dataGridView1.Rows[row].Cells[2].Value.ToString();
            string editstatus = dataGridView1.Rows[row].Cells[3].Value.ToString();
            frmUpdateAccount updateaccountform = new frmUpdateAccount(editusername, editpassword, editusertype, editstatus, username);

            updateaccountform.FormClosed += (s, args) =>
            {
                frmAccountsManagement_Load(sender, e);
            };

            updateaccountform.Show();
        }
        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    accounts.executeSQL("DELETE FROM tbl_accounts WHERE username = '" + dataGridView1.Rows[row].Cells[0].Value.ToString() + "'");
                    if (accounts.rowAffected > 0)
                    {
                        MessageBox.Show("Account deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        accounts.executeSQL("INSERT tbllogs (datelog, timelog, action, module, performedto, performedby) VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "' , '" +
                            DateTime.Now.ToShortTimeString() + "' , 'DELETE', 'ACCOUNTS MANAGEMENT', '" + dataGridView1.Rows[row].Cells[0].Value.ToString() + "' , '" + username + "')");

                        frmAccountsManagement_Load(sender, e);
                        txtsearch.Clear();
                    }
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on btndelete_click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnreset_Click(object sender, EventArgs e)
        {
            frmAccountsManagement_Load(sender, e);
            txtsearch.Clear();
        }
    }
}
