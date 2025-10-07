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
    public partial class frmBooksManagement : Form
    {
        Class1 books = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        public frmBooksManagement(string username)
        {
            InitializeComponent();
            this.username = username;

            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }
        private void frmBooksManagement_Load_1(object sender, EventArgs e)
        {
            try
            {
                dtpFilterDate.ValueChanged += dtpFilterDate_ValueChanged;
                DataTable dt = books.GetData("SELECT * FROM tbl_books ORDER BY BookID");
                dataGridView1.DataSource = dt;
                ApplyRowColor();          
                UpdateButtonStates();
                dataGridView1.Columns[0].HeaderText = "Book ID";
                dataGridView1.Columns[1].HeaderText = "Title";
                dataGridView1.Columns[2].HeaderText = "Author";
                dataGridView1.Columns[3].HeaderText = "Category";
                dataGridView1.Columns[4].HeaderText = "Status";
                dataGridView1.Columns[5].HeaderText = "Added Date";
                dataGridView1.Columns[6].HeaderText = "Quantity";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on frmBooksManagement_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateButtonStates()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                btnAvailable.Enabled = false;
                btnUnavaliable.Enabled = false;
                btnDamage.Enabled = false;
                btnReplace.Enabled = false;
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string status = string.Empty;
            int quantity = 0;

            if (dataGridView1.Columns.Contains("status"))
                status = selectedRow.Cells["status"].Value?.ToString()?.ToUpperInvariant() ?? string.Empty;
            else if (selectedRow.Cells.Count > 4)
                status = selectedRow.Cells[4].Value?.ToString()?.ToUpperInvariant() ?? string.Empty;

            if (dataGridView1.Columns.Contains("quantity"))
                int.TryParse(selectedRow.Cells["quantity"].Value?.ToString(), out quantity);
            else if (selectedRow.Cells.Count > 6)
                int.TryParse(selectedRow.Cells[6].Value?.ToString(), out quantity);

            btnAvailable.Enabled = false;
            btnUnavaliable.Enabled = false;
            btnDamage.Enabled = false;
            btnReplace.Enabled = false;

            switch (status)
            {
                case "AVAILABLE":
                    btnUnavaliable.Enabled = true;
                    btnDamage.Enabled = true;
                    btnReplace.Enabled = false;
                    btnAvailable.Enabled = false;
                    break;

                case "UNAVAILABLE":
                    btnAvailable.Enabled = true;  
                    btnDamage.Enabled = true;
                    btnReplace.Enabled = true;    
                    break;

                case "DAMAGED":
                    btnReplace.Enabled = true;  
                    break;

                default:
                    break;
            }                  
        }
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            frmAddBooks addBookForm = new frmAddBooks(username);
            addBookForm.FormClosed += (s, args) =>
            {
                frmBooksManagement_Load_1(sender, e);
            };
            addBookForm.Show();
        }
        private int row;
        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string bookID = dataGridView1.Rows[row].Cells[0].Value.ToString();
            string title = dataGridView1.Rows[row].Cells[1].Value.ToString();
            string author = dataGridView1.Rows[row].Cells[2].Value.ToString(); ;
            string category = dataGridView1.Rows[row].Cells[3].Value.ToString();
            string status = dataGridView1.Rows[row].Cells[4].Value.ToString();
            string Added_date = dataGridView1.Rows[row].Cells[5].Value.ToString();
            frmUpdateBook updateBook = new frmUpdateBook(bookID, title, author, category, status, Added_date, username);

            updateBook.FormClosed += (s, args) =>
            {
                frmBooksManagement_Load_1(sender, e);
            };
            updateBook.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string query = @"SELECT * FROM tbl_books WHERE BookID LIKE '%" + searchText + @"%' OR title LIKE '%" + searchText + @"%' OR author LIKE '%" + searchText + @"%' 
                                OR category LIKE '%" + searchText + @"%' ORDER BY BookID";
                DataTable dt = books.GetData(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on txtSearch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            private void btnDeleteBook_Click(object sender, EventArgs e)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a book to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string bookID = row.Cells["BookID"].Value?.ToString() ?? string.Empty;

                DialogResult dr = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        books.executeSQL("DELETE FROM tbl_books WHERE BookID = '" + bookID + "'");

                        if (books.rowAffected > 0)
                        {
                            books.executeSQL("INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) VALUES ('" +
                                DateTime.Now.ToString("yyyy/MM/dd") + "' , '" +
                                DateTime.Now.ToShortTimeString() + "', 'DELETE', 'RESOURCES MANAGEMENT', '" +
                                bookID + "', '" + username + "')");

                            MessageBox.Show("Book deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmBooksManagement_Load_1(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("No book deleted. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "ERROR on delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        private void UpdateStatus(string newStatus)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to update the status.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string bookID = row.Cells["BookID"].Value?.ToString() ?? string.Empty;
            string currentStatus = row.Cells["status"].Value?.ToString() ?? string.Empty;

            if (currentStatus == "REPLACED")
            {
                MessageBox.Show("You cannot change the status of a replaced book.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentStatus == "DAMAGED" && newStatus == "AVAILABLE")
            {
                MessageBox.Show("A damaged book cannot be made available directly. Consider replacing it instead.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentStatus == newStatus)
            {
                MessageBox.Show($"The book is already marked as {newStatus}.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show($"Are you sure you want to change status from {currentStatus} to {newStatus}?", "Confirm Status Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    books.executeSQL("UPDATE tbl_books SET status = '" + newStatus + "' WHERE BookID = '" + bookID + "'");

                    if (books.rowAffected > 0)
                    {
                        MessageBox.Show($"Book status updated to {newStatus}.", "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmBooksManagement_Load_1(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No book updated. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on status update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnAvailable_Click(object sender, EventArgs e)
        {
            UpdateStatus("AVAILABLE");
        }

        private void btnUnavaliable_Click(object sender, EventArgs e)
        {
            UpdateStatus("UNAVAILABLE");
        }

        private void btnDamage_Click(object sender, EventArgs e)
        {
            UpdateStatus("DAMAGED");
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a damaged book to replace.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string bookID = selectedRow.Cells["BookID"].Value.ToString();
            string currentStatus = selectedRow.Cells["status"].Value.ToString();

            if (currentStatus != "DAMAGED")
            {
                MessageBox.Show("Only damaged books can be replaced.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to mark this damaged book as AVAILABLE?",
                                              "Confirm Replacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    books.executeSQL("UPDATE tbl_books SET status = 'AVAILABLE' WHERE BookID = '" + bookID + "'");
                    if (books.rowAffected > 0)
                    {
                        books.executeSQL("INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                            "VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "', '" +
                            DateTime.Now.ToShortTimeString() + "', 'REPLACED', 'RESOURCES MANAGEMENT', '" +
                            bookID + "', '" + username + "')");

                        MessageBox.Show("Book successfully replaced and set to AVAILABLE.", "Replaced", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmBooksManagement_Load_1(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No changes were made. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on replace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ApplyRowColor()
        {
            if (dataGridView1.Rows.Count == 0) return;

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.IsNewRow) continue;

                DataGridViewCell statusCell = null;
                DataGridViewCell quantityCell = null;

                if (dataGridView1.Columns.Contains("status"))
                    statusCell = r.Cells["status"];
                else if (dataGridView1.Columns.Count > 4)
                    statusCell = r.Cells[4];

                if (dataGridView1.Columns.Contains("quantity"))
                    quantityCell = r.Cells["quantity"];
                else if (dataGridView1.Columns.Count > 6)
                    quantityCell = r.Cells[6];

                string status = statusCell?.Value?.ToString()?.ToUpperInvariant() ?? string.Empty;
                int quantity = 0;
                int.TryParse(quantityCell?.Value?.ToString(), out quantity);

                switch (status)
                {
                    case "AVAILABLE":
                    case "REPLACED":
                        if (quantity > 0)
                        {
                            r.DefaultCellStyle.BackColor = Color.Honeydew;
                            r.DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            r.DefaultCellStyle.BackColor = Color.LemonChiffon;
                            r.DefaultCellStyle.ForeColor = Color.Black;
                        }
                        break;

                    case "BORROWED":
                        r.DefaultCellStyle.BackColor = Color.Moccasin;
                        r.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    case "DAMAGED":
                        r.DefaultCellStyle.BackColor = Color.LightPink;
                        r.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    default:
                        r.DefaultCellStyle.BackColor = Color.White;
                        r.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateButtonStates();
            row = e.RowIndex;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyRowColor();
            UpdateButtonStates();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            frmBooksManagement_Load_1(sender, e);
            txtSearch.Clear();
        }

        private void btntransac_Click(object sender, EventArgs e)
        {
            frmTransac transactions = new frmTransac(username);
            transactions.FormClosed += (s, args) => frmBooksManagement_Load_1(null, null);
            transactions.Show(); 
        }

        private void dtpFilterDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedDate = dtpFilterDate.Value.ToString("yyyy/MM/dd");
                string query = "SELECT * FROM tbl_books WHERE DATE(Added_date) = '" + selectedDate + "' ORDER BY BookID";
                DataTable dt = books.GetData(query);

                dataGridView1.DataSource = dt;
                ApplyRowColor();
                UpdateButtonStates();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error filtering by date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedStatus = cmbList.SelectedItem?.ToString()?.Trim();

                if (string.IsNullOrWhiteSpace(selectedStatus) || selectedStatus == "ALL")
                {
                    frmBooksManagement_Load_1(null, null);
                    return;
                }

                string query = "SELECT * FROM tbl_books WHERE status = '" + selectedStatus + "' ORDER BY BookID";
                DataTable dt = books.GetData(query);

                dataGridView1.DataSource = dt;
                ApplyRowColor();
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering by status: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                frmBooksManagement_Load_1(sender, e);
                txtSearch.Clear();
                if (cmbList.Items.Count > 0)
                    cmbList.SelectedIndex = -1;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on Refresh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }
    }
}


