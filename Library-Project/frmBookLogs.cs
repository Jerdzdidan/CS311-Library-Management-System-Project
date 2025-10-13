using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ticket_management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Library_Project
{
    public partial class frmBookLogs : Form
    {
        Class1 booklogs = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        private int currentPage = 1;
        private int pageSize = 20;
        private bool hasNextPage = false;
        public frmBookLogs(string username)
        {
            InitializeComponent();
            this.username = username;

            txtsearch.TextChanged += (s, e) => { currentPage = 1; LoadAllLogs(); };
            cmbList.SelectedIndexChanged += (s, e) => { currentPage = 1; LoadAllLogs(); };
            dtpDate.ValueChanged += (s, e) => { currentPage = 1; LoadAllLogs(); };
        }
        private void frmBookLogs_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Now;
            LoadAllLogs();
            SetupHeaders();
        }
        private void SetupHeaders()
        {
            if (dataGridView1.Columns.Count >= 6)
            {
                dataGridView1.Columns[0].HeaderText = "Date";
                dataGridView1.Columns[1].HeaderText = "Time";
                dataGridView1.Columns[2].HeaderText = "Action";
                dataGridView1.Columns[3].HeaderText = "Module";
                dataGridView1.Columns[4].HeaderText = "Performed To";
                dataGridView1.Columns[5].HeaderText = "Performed By";
            }
        }
        private void LoadAllLogs()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;

                string keyword = txtsearch.Text.Trim().Replace("'", "''");
                string selectedDate = dtpDate.Value.ToString("yyyy/MM/dd");
                string selectedFilter = cmbList.SelectedItem?.ToString();

                List<string> filters = new List<string>
                {
                    $"datelog = '{selectedDate}'" // Always show logs for selected date (today by default)
                };

                if (!string.IsNullOrEmpty(keyword))
                    filters.Add($"(performedto LIKE '%{keyword}%' OR performedby LIKE '%{keyword}%' OR action LIKE '%{keyword}%' OR module LIKE '%{keyword}%')");

                if (!string.IsNullOrEmpty(selectedFilter))
                    filters.Add($"(action = '{selectedFilter}' OR module = '{selectedFilter}')");

                string whereClause = "WHERE " + string.Join(" AND ", filters);

                string query = $@"
                    SELECT datelog, timelog, action, module, performedto, performedby
                    FROM tbl_logs
                    {whereClause}
                    ORDER BY datelog DESC, timelog DESC
                    LIMIT {pageSize + 1} OFFSET {offset};
                ";

                DataTable dt = booklogs.GetData(query);

                hasNextPage = dt.Rows.Count > pageSize;
                if (hasNextPage)
                    dt.Rows.RemoveAt(dt.Rows.Count - 1); // remove extra row

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = hasNextPage;
                lblPageInfo.Text = $"Page {currentPage}" + (hasNextPage ? " →" : "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int row;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                    row = e.RowIndex;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on DataGridView Cell Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtsearch.Text.Trim();
                string query = "SELECT datelog, timelog, action, module, performedto, performedby FROM tbl_logs ";

                if (!string.IsNullOrEmpty(keyword))
                {
                    query += "WHERE performedto LIKE '%" + keyword + "%' " +
                             "OR performedby LIKE '%" + keyword + "%' " +
                             "OR action LIKE '%" + keyword + "%' " +
                             "OR module LIKE '%" + keyword + "%' ";
                }

                query += "ORDER BY datelog DESC, timelog DESC";

                DataTable dt = booklogs.GetData(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReset_Click_1(object sender, EventArgs e)
        {
            txtsearch.Clear();
            cmbList.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
            currentPage = 1;
            LoadAllLogs();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedFilter = cmbList.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(selectedFilter))
                {
                    LoadAllLogs();
                    return;
                }

                string esc = selectedFilter.Replace("'", "''");
                string query = "SELECT datelog, timelog, action, module, performedto, performedby " +
                               "FROM tbl_logs " +
                               "WHERE action = '" + esc + "' OR module = '" + esc + "' " +
                               "ORDER BY datelog DESC, timelog DESC";

                DataTable dt = booklogs.GetData(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering logs: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedDate = dtpDate.Value.ToString("yyyy/MM/dd");

                string query = "SELECT datelog, timelog, action, module, performedto, performedby " +
                               "FROM tbl_logs " +
                               "WHERE datelog = '" + selectedDate + "' " +
                               "ORDER BY datelog DESC, timelog DESC";

                DataTable dt = booklogs.GetData(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering by date: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (hasNextPage)
            {
                currentPage++;
                LoadAllLogs();
            }
        }

        private void lblPageInfo_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadAllLogs();
            }
        }
    }
}

