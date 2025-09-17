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
                DataTable dt = booklogs.GetData("SELECT * FROM tbl_logs ORDER BY datelog DESC, timelog DESC");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on LoadAllLogs", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DataTable dt = booklogs.GetData("SELECT * FROM tblaccounts WHERE username LIKE '%" + txtsearch.Text + "%' OR usertype LIKE '%" + txtsearch.Text + "%' ORDER BY username");
                dataGridView1.DataSource = dt;
              }
            catch (Exception error)
              {
                MessageBox.Show(error.Message, "ERROR on txtsearch_TextChanged", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
            dtpDate.Value = DateTime.Now;
            LoadAllLogs();
        }
    }
}

