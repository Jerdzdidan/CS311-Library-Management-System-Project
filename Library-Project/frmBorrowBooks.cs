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
                bookborrow.executeSQL("INSERT INTO tbl_transac (bookCode, bookTitle, author, category, borrowdate, status, borrower, borrowerType, grade_section) " +
                    "VALUES ('" + bookCode + "', '" + bookTitle + "', '" + author + "', '" + category + "', '" + DateTime.Now.ToString("yyyy/MM/dd") +
                    "', 'BORROWED', '" + borrowerName + "', '" + borrowerType + "', '" + grsc + "')");
                bookborrow.executeSQL("UPDATE tbl_books SET status = 'BORROWED' WHERE BookID = '" + bookCode + "'");
                bookborrow.executeSQL("INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) VALUES ('" +
                    DateTime.Now.ToString("yyyy/MM/dd") + "', '" +
                    DateTime.Now.ToShortTimeString() + "', 'BORROW', 'BORROW BOOKS', '" +
                    bookCode + "', '" + username + "')");

                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
