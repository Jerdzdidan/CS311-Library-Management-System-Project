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
            catch (Exception ex)
            {
                MessageBox.Show("LoadAllLogs error: " + ex.Message);
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
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tbl_logs WHERE 1=1 ";
                string keyword = txtsearch.Text.Trim();
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
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtsearch.Text.Trim();
                string query = "SELECT datelog, timelog, action, module, performedto, performedby " +
                               "FROM tbl_logs ";

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
                MessageBox.Show(error.Message, "ERROR on search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();                   
            dtpDate.Value = DateTime.Now;        
            LoadAllLogs();
        }
    }
}

