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
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void frmBorrowBooks_Load(object sender, EventArgs e)
        {
            dtpDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtBookCode.Text = bookCode;
            txtTitle.Text = bookTitle;
            txtAuthor.Text = author;
            txtCategory.Text = category;

            txtGRSC.Enabled = false;
            cmbSubject.Items.Clear();
            cmbSubject.Items.AddRange(new string[]
            {
                "MATHEMATICS", "SCIENCE", "ENGLISH", "FILIPINO", "MAPEH", "ARALING PANLIPUNAN"
            });

            txtBarcode.KeyDown += txtBarcode_KeyDown;
            txtBarcode.Focus();
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtname.Text.Trim().Replace("'", "''");
                if (searchText.Length < 2)
                    return;

                string query = @"
                    SELECT 'STUDENT' AS Type, student_ID AS ID, name, grade, section 
                    FROM tbl_students
                    WHERE name LIKE '%" + searchText + @"%'
                    UNION ALL
                    SELECT 'TEACHER' AS Type, teacher_ID AS ID, name, subject AS grade, '' AS section
                    FROM tbl_teacher
                    WHERE name LIKE '%" + searchText + @"%'
                    LIMIT 1;";

                DataTable dt = bookborrow.GetData(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string type = row["Type"].ToString();

                    txtBarcode.Text = row["ID"].ToString(); // autofill barcode/ID
                    txtname.Text = row["name"].ToString();

                    if (type == "STUDENT")
                    {
                        rbStudent.Checked = true;
                        txtGRSC.Text = $"{row["grade"]}-{row["section"]}";
                        txtGRSC.Enabled = true;
                        cmbSubject.Enabled = false;
                        cmbSubject.SelectedIndex = -1;
                    }
                    else if (type == "TEACHER")
                    {
                        rbTeacher.Checked = true;
                        cmbSubject.Enabled = true;
                        cmbSubject.SelectedItem = row["grade"].ToString();
                        txtGRSC.Enabled = false;
                        txtGRSC.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while searching borrower: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent ding sound
                string scannedID = txtBarcode.Text.Trim();
                if (!string.IsNullOrEmpty(scannedID))
                {
                    AutoFillBorrowerData(scannedID);
                }
            }
        }
        private void AutoFillBorrowerData(string scannedID)
        {
            try
            {
                string cleanID = scannedID.Replace("'", "''");
                string today = DateTime.Now.ToString("yyyy/MM/dd");

                // Check if borrower has attendance today
                DataTable attendanceToday = bookborrow.GetData(
                    $"SELECT ID_number, username, usertype FROM tbl_attendance " +
                    $"WHERE ID_number = '{cleanID}' AND Date_in = '{today}'");

                if (attendanceToday.Rows.Count == 0)
                {
                    MessageBox.Show("This ID does not have attendance today. Please check the Attendance form first.",
                                    "No Attendance Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBarcode.Clear();
                    txtBarcode.Focus();
                    return;
                }

                string userType = attendanceToday.Rows[0]["usertype"].ToString().Trim().ToUpper();
                string name = attendanceToday.Rows[0]["username"].ToString();

                // ✅ Fill borrower name
                txtname.Text = name;

                if (userType == "STUDENT")
                {
                    // Fetch student grade and section
                    DataTable student = bookborrow.GetData(
                        $"SELECT grade, section FROM tbl_students WHERE student_ID = '{cleanID}'");

                    if (student.Rows.Count > 0)
                    {
                        string grade = student.Rows[0]["grade"].ToString();
                        string section = student.Rows[0]["section"].ToString();

                        rbStudent.Checked = true;
                        txtGRSC.Enabled = true;
                        cmbSubject.Enabled = false;
                        txtGRSC.Text = $"{grade}-{section}";
                    }
                }
                else if (userType == "TEACHER")
                {
                    // Fetch teacher subject
                    DataTable teacher = bookborrow.GetData(
                        $"SELECT subject FROM tbl_teacher WHERE teacher_ID = '{cleanID}'");

                    if (teacher.Rows.Count > 0)
                    {
                        string subject = teacher.Rows[0]["subject"].ToString();

                        rbTeacher.Checked = true;
                        txtGRSC.Enabled = false;
                        cmbSubject.Enabled = true;
                        cmbSubject.SelectedItem = subject;
                    }
                }
                else
                {
                    MessageBox.Show("Unknown user type. Only STUDENT or TEACHER allowed.",
                                    "Invalid Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Reset barcode box
                txtBarcode.Clear();
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while scanning barcode: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarcode.Clear();
                txtBarcode.Focus();
            }
        }

        private void rbStudent_CheckedChanged(object sender, EventArgs e)
        {
            txtGRSC.Enabled = rbStudent.Checked;
            cmbSubject.Enabled = !rbStudent.Checked;

            if (rbStudent.Checked)
                cmbSubject.SelectedIndex = -1;
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
                    MessageBox.Show("Please scan or enter borrower details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rbStudent.Checked && string.IsNullOrWhiteSpace(grsc))
                {
                    MessageBox.Show("Please enter Grade & Section for students.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🧠 TEACHER RULES: up to 3 books, 5 days
                if (borrowerType == "TEACHER")
                {
                    string escName = borrowerName.Replace("'", "''");
                    string countQuery = $"SELECT * FROM tbl_transac WHERE borrower = '{escName}' AND borrowerType = 'TEACHER' AND status = 'BORROWED'";
                    int borrowedCount = bookborrow.GetData(countQuery).Rows.Count;

                    if (borrowedCount >= 3)
                    {
                        MessageBox.Show("This teacher already has the maximum number of borrowed books.", "Limit reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DateTime borrowDate = DateTime.Now;
                    DateTime returnDate = borrowDate.AddDays(5);
                    string transacID = "TRS-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    bookborrow.executeSQL($@"
                        INSERT INTO tbl_transac 
                        (bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section, transacID)
                        VALUES ('{bookCode}', '{bookTitle}', '{author}', '{category}', 
                                '{borrowDate:yyyy/MM/dd}', '{returnDate:yyyy/MM/dd}', 
                                'BORROWED', '{escName}', 'TEACHER', 'N/A', '{transacID}')");

                    bookborrow.executeSQL($@"
                                            UPDATE tbl_books 
                                            SET quantity = quantity - 1,
                                                status = CASE 
                                                            WHEN quantity - 1 <= 0 THEN 'BORROWED'
                                                            ELSE 'AVAILABLE'
                                                         END
                                            WHERE BookID = '{bookCode}'");

                    bookborrow.executeSQL($@"
                        INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                        VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now:hh\\:mm tt}', 'BORROW', 'BORROW BOOKS', '{bookCode}', '{username}')");

                    MessageBox.Show($"Book borrowed successfully!\n\nReturn on or before {returnDate:yyyy MM, dd}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                // 🧠 STUDENT RULES: 1 book at a time, 3 days
                string checkQuery = $"SELECT bookCode FROM tbl_transac WHERE borrower = '{borrowerName}' AND status = 'BORROWED' LIMIT 1";
                DataTable dtBorrowed = bookborrow.GetData(checkQuery);

                if (dtBorrowed.Rows.Count > 0)
                {
                    string borrowedBook = dtBorrowed.Rows[0]["bookCode"].ToString();
                    MessageBox.Show(
                        $"This borrower already has a borrowed book (Book Code: {borrowedBook}).\n\n" +
                        "Borrowing limit: 1 book at a time\nReturn period: 3 days",
                        "Borrow Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if book available
                DataTable dtQuantity = bookborrow.GetData($"SELECT quantity FROM tbl_books WHERE BookID = '{bookCode}'");
                if (dtQuantity.Rows.Count == 0 || Convert.ToInt32(dtQuantity.Rows[0]["quantity"]) <= 0)
                {
                    MessageBox.Show("This book is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Insert student borrow record
                DateTime borrowDateStudent = DateTime.Now;
                string transacIDStudent = "TRS-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                bookborrow.executeSQL($@"
                    INSERT INTO tbl_transac 
                    (bookCode, bookTitle, author, category, borrowdate, status, borrower, borrowerType, grade_section, transacID)
                    VALUES ('{bookCode}', '{bookTitle}', '{author}', '{category}', 
                            '{borrowDateStudent:yyyy/MM/dd}', 'BORROWED', '{borrowerName}', 
                            '{borrowerType}', '{grsc}', '{transacIDStudent}')");

                                // 🔹 Update quantity and status
                                bookborrow.executeSQL($@"
                    UPDATE tbl_books 
                    SET quantity = quantity - 1,
                        status = CASE 
                                    WHEN quantity - 1 <= 0 THEN 'BORROWED'
                                    ELSE 'AVAILABLE'
                                 END
                    WHERE BookID = '{bookCode}'");

                                // 🔹 Log action
                                bookborrow.executeSQL($@"
                    INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                    VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now.ToShortTimeString()}', 
                            'BORROW', 'BORROW BOOKS', '{bookCode}', '{username}')");

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
