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
        private int row;
        private int currentPage = 1;
        private int pageSize = 10;
        public frmAccountsManagement(string username)
        {
            InitializeComponent();
            this.username = username;
            txtsearch.TextChanged += txtsearch_TextChanged_1;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }
        private void frmAccountsManagement_Load(object sender, EventArgs e)
        {
            LoadAccounts();

            if (dataGridView1.Columns.Count >= 5)
            {
                dataGridView1.Columns[0].HeaderText = "Username";
                dataGridView1.Columns[1].HeaderText = "Password";
                dataGridView1.Columns[2].HeaderText = "User Type";
                dataGridView1.Columns[3].HeaderText = "Status";
                dataGridView1.Columns[4].HeaderText = "Created By";
                dataGridView1.Columns[5].HeaderText = "Date Created";
            }
        }
        private void LoadAccounts(string search = "")
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;
                string searchCondition = "";
                string keyword = txtsearch.Text.Trim().Replace("'", "''");

                if (!string.IsNullOrEmpty(keyword))
                {
                    searchCondition = $" WHERE (username LIKE '%{keyword}%' OR usertype LIKE '%{keyword}%')";
                }

                // Load one extra record to check if there's a next page
                string query = $"SELECT username, password, usertype, status, createdby, datecreated FROM tbl_accounts {searchCondition} ORDER BY username ASC LIMIT {pageSize + 1} OFFSET {offset}";
                DataTable dt = accounts.GetData(query);

                bool hasNextPage = dt.Rows.Count > pageSize;
                if (hasNextPage)
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Update pagination buttons
                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = hasNextPage;

                // Update page info
                lblPageInfo.Text = $"Page {currentPage}" + (hasNextPage ? " →" : "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = e.RowIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cell Click Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            frmAddAccount addForm = new frmAddAccount(username);
            addForm.FormClosed += (s, args) =>
            {
                currentPage = 1;
                LoadAccounts();
                LogAction("ADD", "ACCOUNTS MANAGEMENT", "Added new account");
            };
            addForm.ShowDialog();
        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null || dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select an account to update.",
                                "Invalid Selection",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count)
            {
                MessageBox.Show("Please select a valid row to update.",
                                "Invalid Selection",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string editusername = dataGridView1.Rows[rowIndex].Cells["username"].Value.ToString();
            string editpassword = dataGridView1.Rows[rowIndex].Cells["password"].Value.ToString();
            string editusertype = dataGridView1.Rows[rowIndex].Cells["usertype"].Value.ToString();
            string editstatus = dataGridView1.Rows[rowIndex].Cells["status"].Value.ToString();

            frmUpdateAccount updateForm = new frmUpdateAccount(editusername, editpassword, editusertype, editstatus, username);
            updateForm.FormClosed += (s, args) =>
            {
                LoadAccounts();
                LogAction("UPDATE", "ACCOUNTS MANAGEMENT", $"Updated account {editusername}");
            };
            updateForm.ShowDialog();
        }
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (row >= 0 && row < dataGridView1.Rows.Count)
            {
                string usernameToDelete = dataGridView1.Rows[row].Cells["username"].Value.ToString();

                DialogResult dr = MessageBox.Show($"Are you sure you want to delete account '{usernameToDelete}'?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        accounts.executeSQL($"DELETE FROM tbl_accounts WHERE username = '{usernameToDelete}'");

                        if (accounts.rowAffected > 0)
                        {
                            LoadAccounts();
                            LogAction("DELETE", "ACCOUNTS MANAGEMENT", $"Deleted account {usernameToDelete}");
                            MessageBox.Show("Account deleted successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No account was deleted. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting account: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnreset_Click(object sender, EventArgs e)
        {
            frmAccountsManagement_Load(sender, e);
            txtsearch.Clear();
        }

        private void txtsearch_TextChanged_1(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadAccounts(txtsearch.Text);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadAccounts(txtsearch.Text);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadAccounts(txtsearch.Text);
            }
        }
        private void LogAction(string action, string module, string details)
        {
            try
            {
                accounts.executeSQL(
                    $"INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                    $"VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now:hh\\:mm tt}', '{action}', '{module}', '{details}', '{username}')");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Log Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
    }
}
