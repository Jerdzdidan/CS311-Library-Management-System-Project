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
    public partial class frmTransac : Form
    {
        Class1 booktransac = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        public frmTransac(string username)
        {
            InitializeComponent();
            this.username = username;
            dataGridView1.DataBindingComplete += (s, e) =>
            {
                ApplyRowColorTransactions();
            };
        }
        private void frmTransac_Load(object sender, EventArgs e)
        {
            LoadAllTransactions();
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
                    "SELECT bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
                    "FROM tbl_transac ORDER BY borrowdate DESC"
                );
                dataGridView1.DataSource = dt;
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
        private void btnrefres_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dtpDate.Value = DateTime.Now;
            LoadAllTransactions();
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
                string query = "SELECT bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
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
        private void btnRetrn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a transaction to return.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string bookCode = selectedRow.Cells["bookCode"].Value.ToString();
            string status = selectedRow.Cells["status"].Value.ToString();

            if (status == "RETURNED")
            {
                MessageBox.Show("This book is already returned.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to return this book?",
                                              "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    // Get current quantity from tbl_books
                    DataTable dtBook = booktransac.GetData($"SELECT quantity FROM tbl_books WHERE BookID = '{bookCode}'");

                    if (dtBook.Rows.Count == 0)
                    {
                        MessageBox.Show("Book not found in tbl_books.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int currentQty = 0;
                    int.TryParse(dtBook.Rows[0]["quantity"].ToString(), out currentQty);
                    int newQty = currentQty + 1;
                    booktransac.executeSQL($"UPDATE tbl_books SET quantity = '{newQty}', status = 'AVAILABLE' WHERE BookID = '{bookCode}'");
                    int affectedBooks = booktransac.rowAffected;
                    booktransac.executeSQL($"UPDATE tbl_transac SET returndate = '{DateTime.Now:yyyy/MM/dd}', status = 'RETURNED' WHERE bookCode = '{bookCode}' AND status = 'BORROWED'");
                    int affectedTransac = booktransac.rowAffected;

                    if (affectedBooks > 0 || affectedTransac > 0)
                    {
                        booktransac.executeSQL("INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                                               $"VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now:hh\\:mm tt}', 'RETURN', " +
                                               $"'TRANSACTIONS', '{bookCode}', '{username}')");

                        MessageBox.Show("Book successfully returned and quantity increased by 1.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllTransactions();
                    }
                    else
                    {
                        MessageBox.Show("No matching borrowed record found for this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on book return", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbList_SelectedIndexChanged(object sender, EventArgs e)
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
                string query = "SELECT bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
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
    }
}
