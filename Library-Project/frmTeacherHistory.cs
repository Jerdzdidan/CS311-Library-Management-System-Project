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
    public partial class frmTeacherHistory : Form
    {
        Class1 teacherhistory = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string teacherID;
        private string teacherName;
        private string subject;
        public frmTeacherHistory(string id, string name, string subject)
        {
            InitializeComponent();
            this.teacherID = id;
            this.teacherName = name;
            this.subject = subject;
        }

        private void frmTeacherHistory_Load(object sender, EventArgs e)
        {
            txtTeacherID.Text = teacherID;
            txtTeacherName.Text = teacherName;
            txtSubject.Text = subject;

            LoadHistory();
        }
        private void LoadHistory()
        {
            try
            {
                string query = $@"
            SELECT bookCode AS 'Book Code',
                   bookTitle AS 'Book Title',
                   category AS 'Category',
                   'BORROWED' AS 'Action',
                   borrowdate AS 'Date'
            FROM tbl_transac
            WHERE borrower = '{teacherName}'

            UNION ALL

            SELECT bookCode AS 'Book Code',
                   bookTitle AS 'Book Title',
                   category AS 'Category',
                   'RETURNED' AS 'Action',
                   returndate AS 'Date'
            FROM tbl_transac
            WHERE borrower = '{teacherName}' AND returndate IS NOT NULL

            ORDER BY Date DESC;
        ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR on LoadHistory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
