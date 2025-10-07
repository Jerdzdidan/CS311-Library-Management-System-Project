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
        public partial class frmTransactions : Form
        {
            Class1 booktransac = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
            private string username;
            public frmTransactions(string username)
            {
                InitializeComponent();
                this.username = username;
                dataGridView1.DataBindingComplete += (s, e) =>
                {
                    ApplyRowColorTransactions();
                };
            }
            private void frmTransactions_Load(object sender, EventArgs e)
            {
                LoadAllTransactions();
                dataGridView1.Columns["bookCode"].HeaderText = "Book Code";
                dataGridView1.Columns["bookTitle"].HeaderText = "Book Title";
                dataGridView1.Columns["author"].HeaderText = "Author";
                dataGridView1.Columns["category"].HeaderText = "Category";
                dataGridView1.Columns["borrowdate"].HeaderText = "Borrowed Date";
                dataGridView1.Columns["returndate"].HeaderText = "Return Date";
                dataGridView1.Columns["status"].HeaderText = "Status";
                dataGridView1.Columns["borrower"].HeaderText = "Borrower";
                dataGridView1.Columns["borrowerType"].HeaderText = "Borrower Type";
                dataGridView1.Columns["grade_section"].HeaderText = "Grade and Section";
        }
        private void ApplyRowColorTransactions()
        {
            if (dataGridView1.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                string status = row.Cells["status"].Value?.ToString() ?? "";

                switch (status.ToUpperInvariant())
                {
                    case "BORROWED":
                        row.DefaultCellStyle.BackColor = Color.MistyRose;  
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    case "RETURNED":
                        row.DefaultCellStyle.BackColor = Color.Honeydew;  
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    default:
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                }
            }
        }
        private void LoadAllTransactions()
        {
            try
            {

                DataTable dt = booktransac.GetData(
                    "SELECT transacID, bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
                    "FROM tbl_transac ORDER BY borrowdate DESC"
                );
                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns.Contains("transacID"))
                    dataGridView1.Columns["transacID"].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR loading transactions", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            private void dtpDate_ValueChanged(object sender, EventArgs e)
            {
                txtSearch_TextChanged(sender, e);
            }
            private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    string keyword = txtSearch.Text.Trim();
                string query = "SELECT transacID, bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
                               "FROM tbl_transac " +
                               "WHERE (bookCode LIKE '%" + keyword + "%' " +
                               "OR bookTitle LIKE '%" + keyword + "%' " +
                               "OR author LIKE '%" + keyword + "%' " +
                               "OR status LIKE '%" + keyword + "%' " +
                               "OR category LIKE '%" + keyword + "%' " +
                               "OR borrower LIKE '%" + keyword + "%' " +
                               "OR borrowerType LIKE '%" + keyword + "%' " +
                               "OR grade_section LIKE '%" + keyword + "%' " +
                               "OR borrowdate LIKE '%" + keyword + "%' " +
                               "OR returndate LIKE '%" + keyword + "%') " +
                               "ORDER BY borrowdate DESC";

                DataTable dt = booktransac.GetData(query);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on live search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedFilter = cmbList.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(selectedFilter))
                {
                    LoadAllTransactions();
                    return;
                }
                string esc = selectedFilter.Replace("'", "''");
                string query = "SELECT transacID, bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
               "FROM tbl_transac " +
               "WHERE status = '" + esc + "' " +
               "ORDER BY borrowdate DESC, returndate DESC";

                DataTable dt = booktransac.GetData(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering transactions: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dtpDate.Value = DateTime.Now;
            LoadAllTransactions();
            if (cmbList.Items.Count > 0)
            {
                cmbList.SelectedIndex = -1;
            }
        }
    }
}

