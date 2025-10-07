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
        string searchText = txtname.Text.Trim().Replace("'", "''");
        if (searchText.Length < 2)
        {
            lstSearchResults.Visible = false;
            return;
        }

        string query = @"
            SELECT 'STUDENT' AS Type, student_ID AS ID, name, grade, section 
            FROM tbl_students
            WHERE DATE_FORMAT(STR_TO_DATE(date_in, '%Y/%m/%d'), '%Y-%m-%d') = CURDATE()
              AND name LIKE '%" + searchText + @"%'

            UNION ALL

            SELECT 'TEACHER' AS Type, teacher_ID AS ID, name, subject AS grade, '' AS section
            FROM tbl_teacher
            WHERE DATE_FORMAT(STR_TO_DATE(date_in, '%Y/%m/%d'), '%Y-%m-%d') = CURDATE()
              AND name LIKE '%" + searchText + @"%'

            LIMIT 10;";

        DataTable dt = bookborrow.GetData(query);

        if (dt != null && dt.Rows.Count > 0)
        {
            lstSearchResults.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string type = row["Type"].ToString();
                string name = row["name"].ToString();

                if (type == "STUDENT")
                    lstSearchResults.Items.Add($"{name} ({row["grade"]}-{row["section"]})");
                else
                    lstSearchResults.Items.Add($"{name} [Teacher - {row["grade"]}]");
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
        MessageBox.Show("Error searching: " + ex.Message);
    }
        }
        private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchResults.SelectedIndex < 0) return;

            try
            {
                DataTable dt = lstSearchResults.Tag as DataTable;
                if (dt == null || lstSearchResults.SelectedIndex >= dt.Rows.Count) return;

                DataRow selectedRow = dt.Rows[lstSearchResults.SelectedIndex];

                string type = selectedRow.Field<string>("Type") ?? "";
                string name = selectedRow.Field<string>("name") ?? "";
                txtname.Text = name;

                if (type == "STUDENT")
                {
                    string grade = selectedRow.Field<string>("grade") ?? "";
                    string section = selectedRow.Field<string>("section") ?? "";
                    txtGRSC.Text = grade + (string.IsNullOrEmpty(section) ? "" : "-" + section);
                    txtSubject.Clear();

                    rbStudent.Checked = true;
                    txtGRSC.Enabled = true;
                    txtSubject.Enabled = false;
                }
                else // TEACHER
                {
                    txtGRSC.Clear();
                    txtSubject.Text = selectedRow.Field<string>("grade") ?? "";

                    rbTeacher.Checked = true;
                    txtGRSC.Enabled = false;
                    txtSubject.Enabled = true;
                }

                lstSearchResults.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Selection error: " + ex.Message);
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
            txtSubject.Enabled = !rbStudent.Checked;

            if (rbStudent.Checked)
                txtSubject.Clear();
            else
                txtGRSC.Clear();
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

                // === 🧠 TEACHER RULES: Allow 3 books, 5 days to return ===
                if (borrowerType.ToUpper() == "TEACHER")
                {
                    // escape single quotes in name
                    string escName = borrowerName.Replace("'", "''");

                    string countQuery = "SELECT * FROM tbl_transac WHERE borrower = '" + escName +
                                        "' AND borrowerType = 'TEACHER' AND status = 'BORROWED'";
                    int borrowedCount = bookborrow.GetData(countQuery).Rows.Count;

                    if (borrowedCount >= 3)
                    {
                        MessageBox.Show("This teacher already has the maximum number of borrowed books.", "Limit reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Proceed with teacher borrowing (rest of your code)
                    DateTime borrowDate = DateTime.Now;
                    DateTime returnDate = borrowDate.AddDays(5);
                    string transacID = "TRS - " + DateTime.Now.ToString("yyyyMMddHHmmss");
                    
                    bookborrow.executeSQL($@"
                        INSERT INTO tbl_transac 
                        (bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section, transacID)
                        VALUES ('{bookCode}', '{bookTitle}', '{author}', '{category}', 
                                '{borrowDate:yyyy/MM/dd}', '{returnDate:yyyy/MM/dd}', 
                                'BORROWED', '{escName}', 'TEACHER', 'N/A', '{transacID}')");

                    // Decrease book quantity
                    bookborrow.executeSQL($"UPDATE tbl_books SET quantity = quantity - 1 WHERE BookID = '{bookCode}'");

                    // Log
                    bookborrow.executeSQL($@"
                        INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                        VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now:hh\\:mm tt}', 'BORROW', 'BORROW BOOKS', '{bookCode}', '{username}')");

                    MessageBox.Show($"Book borrowed successfully!\n\nReturn on or before {returnDate:MMMM dd, yyyy}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    return;
                }

                // === 🧠 STUDENT RULES: 1 book at a time ===
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

                // Check book quantity
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

                // Proceed with student borrowing
                DateTime borrowDateStudent = DateTime.Now;
                string transacIDStudent = "TRS - " + DateTime.Now.ToString("yyyyMMddHHmmss");

                bookborrow.executeSQL(
                    "INSERT INTO tbl_transac (bookCode, bookTitle, author, category, borrowdate, status, borrower, borrowerType, grade_section, transacID) " +
                    "VALUES ('" + bookCode + "', '" + bookTitle + "', '" + author + "', '" + category + "', " +
                    "'" + borrowDateStudent.ToString("yyyy/MM/dd") + "', " +
                    "'BORROWED', '" + borrowerName + "', '" + borrowerType + "', '" + grsc + "','" + transacIDStudent + "')"
                );

                bookborrow.executeSQL("UPDATE tbl_books SET quantity = quantity - 1 WHERE BookID = '" + bookCode + "'");

                // Log
                bookborrow.executeSQL(
                    "INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " +
                    "VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "', '" + DateTime.Now.ToShortTimeString() + "', " +
                    "'BORROW', 'BORROW BOOKS', '" + bookCode + "', '" + username + "')"
                );

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
