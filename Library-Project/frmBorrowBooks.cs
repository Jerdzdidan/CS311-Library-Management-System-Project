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
    public partial class frmBorrowBooks : Form
    {
        Class1 bookborrow = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        private string bookCode, bookTitle, author, category;
        public frmBorrowBooks(string bookCode, string bookTitle, string author, string category, string username)
        {
            InitializeComponent();
            this.bookCode = bookCode;
            this.bookTitle = bookTitle;
            this.author = author;
            this.category = category;
            this.username = username;
        }
        private void txtname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtname.Text.Trim();

                if (searchText.Length < 2) 
                {
                    lstSearchResults.Visible = false;
                    return;
                }

                DataTable dt = bookborrow.GetData(
                    "SELECT student_ID, name, grade, section FROM tbl_students " +
                    "WHERE DATE(date_in) = CURDATE() " +  
                    "AND name LIKE '%" + searchText + "%' LIMIT 10"
                );

                if (dt.Rows.Count > 0)
                {
                    lstSearchResults.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        lstSearchResults.Items.Add(row["name"].ToString() +
                            " (" + row["grade"] + "-" + row["section"] + ")");
                    }
                    lstSearchResults.Tag = dt;
                    lstSearchResults.Visible = true;
                }
                else
                {
                    lstSearchResults.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching students: " + ex.Message);
            }
        }
        private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchResults.SelectedIndex >= 0)
            {
                DataTable dt = lstSearchResults.Tag as DataTable;
                DataRow selectedRow = dt.Rows[lstSearchResults.SelectedIndex];

                txtname.Text = selectedRow["name"].ToString();
                txtGRSC.Text = selectedRow["grade"].ToString() + "-" + selectedRow["section"].ToString();
                rbStudent.Checked = true;

                lstSearchResults.Visible = false;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void frmBorrowBooks_Load(object sender, EventArgs e)
        {
            dtpDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtBookCode.Text = bookCode;
            txtTitle.Text = bookTitle;
            txtAuthor.Text = author;
            txtCategory.Text = category;
            txtGRSC.Enabled = false; 
        }
        private void rbStudent_CheckedChanged(object sender, EventArgs e)
        {
            txtGRSC.Enabled = rbStudent.Checked;
            if (!rbStudent.Checked)
            {
                txtGRSC.Clear();
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string borrowerName = txtname.Text.Trim();
                string borrowerType = rbStudent.Checked ? "STUDENT" : "TEACHER";
                string grsc = rbStudent.Checked ? txtGRSC.Text.Trim() : "";
                if (string.IsNullOrWhiteSpace(borrowerName))
                {
                    MessageBox.Show("Please enter borrower name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (rbStudent.Checked && string.IsNullOrWhiteSpace(grsc))
                {
                    MessageBox.Show("Please enter Grade & Section for student borrowers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    string checkQuery = "SELECT bookCode FROM tbl_transac WHERE borrower = '" + borrowerName + "' AND status = 'BORROWED' LIMIT 1";
                    DataTable dtBorrowed = bookborrow.GetData(checkQuery);

                    if (dtBorrowed != null && dtBorrowed.Rows.Count > 0)
                    {
                        string borrowedBook = dtBorrowed.Rows[0]["bookCode"].ToString();
                        MessageBox.Show(
                            "This borrower already has a borrowed book (Book Code: " + borrowedBook + ").\n\n" +
                            "Please return it before borrowing another.\n\n" +
                            "Borrowing limit: 1 book at a time\n" +
                            "Return period: 3 days",
                            "Borrow Limit Reached",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking borrower status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dtQuantity = bookborrow.GetData("SELECT quantity FROM tbl_books WHERE BookID = '" + bookCode + "'");
                int currentQuantity = 0;
                if (dtQuantity.Rows.Count > 0)
                {
                    int.TryParse(dtQuantity.Rows[0]["quantity"].ToString(), out currentQuantity);
                }
                if (currentQuantity <= 0)
                {
                    MessageBox.Show("This book is out of stock. No copies available to borrow.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime borrowDate = DateTime.Now;
                bookborrow.executeSQL(
                    "INSERT INTO tbl_transac (bookCode, bookTitle, author, category, borrowdate, status, borrower, borrowerType, grade_section) " +
                    "VALUES ('" + bookCode + "', '" + bookTitle + "', '" + author + "', '" + category + "', " +
                    "'" + borrowDate.ToString("yyyy/MM/dd") + "', " +
                    "'BORROWED', '" + borrowerName + "', '" + borrowerType + "', '" + grsc + "')"
                );
                bookborrow.executeSQL("UPDATE tbl_books SET quantity = quantity - 1 WHERE BookID = '" + bookCode + "'");
                DataTable dtNewQuantity = bookborrow.GetData("SELECT quantity FROM tbl_books WHERE BookID = '" + bookCode + "'");
                int newQuantity = 0;
                if (dtNewQuantity.Rows.Count > 0)
                {
                    int.TryParse(dtNewQuantity.Rows[0]["quantity"].ToString(), out newQuantity);
                }
                if (newQuantity == 0)
                {
                    bookborrow.executeSQL("UPDATE tbl_books SET status = 'BORROWED' WHERE BookID = '" + bookCode + "'");
                }
                bookborrow.executeSQL(
                    "INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                    "VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "', '" + DateTime.Now.ToShortTimeString() + "', " +
                    "'BORROW', 'BORROW BOOKS', '" + bookCode + "', '" + username + "')"
                );

                MessageBox.Show(
                    "Book borrowed successfully!\n\n" +
                    "Borrower: " + borrowerName + "\n" +
                    "Remaining copies: " + newQuantity,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
