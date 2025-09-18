using System;
using System.Windows.Forms;
using ticket_management;

namespace Library_Project
{
    public partial class frmUpdateBook : Form
    {
        Class1 bookUpdate = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");

        private string username;
        private int errorcount;

        public frmUpdateBook(string bookID, string title, string author, string category, string status, string borrowedDate, string username)
        {
            InitializeComponent();
            this.username = username; 
            txtBookCode.Text = bookID;
            txtTitle.Text = title;
            txtAuthor.Text = author;
            cmbCategory.Text = category;
            cmbStatus.Text = status;

            if (DateTime.TryParse(borrowedDate, out DateTime parsedDate))
            {
                dtpDate.Value = parsedDate;
            }
            else
            {
                dtpDate.Value = DateTime.Now;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorcount = 0;

            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Title is empty.");
                errorcount++;
            }
            if (string.IsNullOrEmpty(txtAuthor.Text))
            {
                errorProvider1.SetError(txtAuthor, "Author is empty.");
                errorcount++;
            }
            if (cmbCategory.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbCategory, "Select category.");
                errorcount++;
            }
            if (cmbStatus.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbStatus, "Select status.");
                errorcount++;
            }
            if (string.IsNullOrEmpty(dtpDate.Text))
            {
                errorProvider1.SetError(dtpDate, "Date is empty.");
                errorcount++;
            }

            if (errorcount == 0)
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to update this book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        bookUpdate.executeSQL("UPDATE tbl_books SET " +
                            "title = '" + txtTitle.Text + "', " +
                            "author = '" + txtAuthor.Text + "', " +
                            "category = '" + cmbCategory.Text + "', " +
                            "status = '" + cmbStatus.Text + "', " +
                            "borroweddate = '" + dtpDate.Text + "' " +
                            "WHERE BookID = '" + txtBookCode.Text + "'");

                        if (bookUpdate.rowAffected > 0)
                        {
                            MessageBox.Show("Book updated.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bookUpdate.executeSQL("INSERT tbl_logs (datelog, timelog, action, module, performedto, performedby) VALUES ('" +
                                DateTime.Now.ToString("yyyy/MM/dd") + "', '" +
                                DateTime.Now.ToShortTimeString() + "', 'UPDATE', 'BOOKS MANAGEMENT', '" +
                                txtBookCode.Text + "', '" + username + "')");

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on updating book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public event EventHandler BookUpdated;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
