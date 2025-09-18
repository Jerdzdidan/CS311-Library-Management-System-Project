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
    public partial class frmBookLogs : Form
    {
        Class1 booklogs = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        public frmBookLogs(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private void frmBookLogs_Load(object sender, EventArgs e)
        {
            LoadAllLogs();
        }
        private void LoadAllLogs()
        {
            try
            {
                DataTable dt = booklogs.GetData("SELECT datelog, timelog, action, module, performedto, performedby FROM tbl_logs ORDER BY datelog DESC, timelog DESC");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR loading logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int row;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    row = e.RowIndex;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on datagridview1_CellContentClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnsearch_Click(sender, e);
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tbl_logs WHERE 1=1 ";
                string keyword = txtSearch.Text.Trim();
                string query = "SELECT datelog, timelog, action, module, performedto, performedby " +
                               "FROM tbl_logs " +
                               "WHERE performedto LIKE '%" + keyword + "%' " +
                               "OR performedby LIKE '%" + keyword + "%' " +
                               "OR action LIKE '%" + keyword + "%' " +
                               "OR module LIKE '%" + keyword + "%' " +
                               "ORDER BY datelog DESC, timelog DESC";

                string selectedDate = dtpDate.Value.ToString("MM/dd/yyyy");
                sql += "AND datelog = '" + selectedDate + "' ";
                sql += "ORDER BY datelog DESC, timelog DESC";
                DataTable dt = booklogs.GetData(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dtpDate.Value = DateTime.Now;
            LoadAllLogs();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                string query = "SELECT datelog, timelog, action, module, performedto, performedby " + "FROM tbl_logs " + "WHERE performedto LIKE '%" + keyword + "%' " + "OR performedby LIKE '%" + keyword + "%' " +
                               "OR action LIKE '%" + keyword + "%' " + "OR module LIKE '%" + keyword + "%' " + "ORDER BY datelog DESC, timelog DESC";
                DataTable dt = booklogs.GetData(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on live search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book log to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this log entry?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Get the row index
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                    // Get fresh data from database
                    DataTable dt = booklogs.GetData("SELECT datelog, timelog, action, module, performedto, performedby FROM tbl_logs ORDER BY datelog DESC, timelog DESC");

                    if (selectedRowIndex < dt.Rows.Count)
                    {
                        DataRow rowToDelete = dt.Rows[selectedRowIndex];

                        // Delete using all fields to ensure exact match
                        string query = $"DELETE FROM tbl_logs WHERE " +
                                     $"datelog = '{rowToDelete["datelog"]}' AND " +
                                     $"timelog = '{rowToDelete["timelog"]}' AND " +
                                     $"action = '{rowToDelete["action"]}' AND " +
                                     $"module = '{rowToDelete["module"]}' AND " +
                                     $"performedto = '{rowToDelete["performedto"]}' AND " +
                                     $"performedby = '{rowToDelete["performedby"]}' LIMIT 1";

                        booklogs.executeSQL(query);

                        if (booklogs.rowAffected > 0)
                        {
                            MessageBox.Show("Log deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAllLogs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the log entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

