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
        private string username;
        public frmBooksManagement(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        Class1 books = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private void frmBooksManagement_Load_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = books.GetData("SELECT * FROM tbl_books ORDER BY BookID");
                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on frmBooksManagement_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string borrowedDate = dataGridView1.Rows[row].Cells[5].Value.ToString();
            frmUpdateBook updateBook = new frmUpdateBook(bookID, title, author, category, status, borrowedDate, username);

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
            string bookID = row.Cells["BookID"].Value.ToString();

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    books.executeSQL("DELETE FROM tbl_books WHERE BookID = '" + bookID + "'");

                    if (books.rowAffected > 0)
                    {
                        books.executeSQL("INSERT tbl_logs (datelog, timelog, action, module, performedto, performedby) VALUES ('" +
                            DateTime.Now.ToString("yyyy/MM/dd") + "' , '" +
                            DateTime.Now.ToShortTimeString() + "', 'DELETE', 'BOOKS MANAGEMENT', '" +
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
            string bookID = row.Cells["BookID"].Value.ToString();
            string currentStatus = row.Cells["status"].Value.ToString();
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
                        frmBooksManagement_Load_1(null, null); // Refresh DataGridView
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

        private void btnBorrowed_Click(object sender, EventArgs e)
        {
            UpdateStatus("BORROWED");
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to return.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string bookID = selectedRow.Cells["BookID"].Value.ToString();
            string currentStatus = selectedRow.Cells["status"].Value.ToString();

            if (currentStatus == "AVAILABLE")
            {
                MessageBox.Show("This book is already available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to return this book?",
                                              "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    books.executeSQL("UPDATE tbl_books SET status = 'AVAILABLE' WHERE BookID = '" + bookID + "'");
                    books.executeSQL("UPDATE tbl_transac SET returndate = '" + DateTime.Now.ToString("yyyy/MM/dd") +
                                     "', status = 'RETURNED' WHERE bookCode = '" + bookID + "' AND status = 'BORROWED'");
                    books.executeSQL("INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                                     "VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "', '" +
                                     DateTime.Now.ToShortTimeString() + "', 'RETURN', 'BOOKS MANAGEMENT', '" +
                                     bookID + "', '" + username + "')");
                    if (books.rowAffected > 0)
                    {
                        MessageBox.Show("Book successfully returned.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmBooksManagement_Load_1(null, null); 
                    }
                    else
                    {
                        MessageBox.Show("No changes were made. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on book return", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string bookCode = row.Cells["BookID"].Value.ToString();
            string bookTitle = row.Cells["title"].Value.ToString();
            string author = row.Cells["author"].Value.ToString();
            string category = row.Cells["category"].Value.ToString();

            frmBorrowBooks borrowForm = new frmBorrowBooks(bookCode, bookTitle, author, category, username);

            borrowForm.FormClosed += (s, args) =>
            {
                frmBooksManagement_Load_1(sender, e); // Refresh after borrowing
            };
            borrowForm.ShowDialog();
        }
    }
}


