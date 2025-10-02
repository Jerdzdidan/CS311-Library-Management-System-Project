using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ticket_management;

namespace Library_Project
{
    public partial class frmStudentHistory : Form
    {
        Class1 studenthistory = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string studentID;
        private string studentName;
        private string grade;
        private string section;
        public frmStudentHistory(string id, string name, string grade, string section)
        {
            InitializeComponent();
            this.studentID = id;
            this.studentName = name;
            this.grade = grade;
            this.section = section;
        }

        private void frmStudentHistory_Load(object sender, EventArgs e)
        {
            txtStudentID.Text = studentID;
            txtStudentName.Text = studentName;
            txtGrade.Text = grade;
            txtSection.Text = section;

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
            WHERE borrower = '{studentName}'

            UNION ALL

            SELECT bookCode AS 'Book Code',
                   bookTitle AS 'Book Title',
                   category AS 'Category',
                   'RETURNED' AS 'Action',
                   returndate AS 'Date'
            FROM tbl_transac
            WHERE borrower = '{studentName}' AND returndate IS NOT NULL

            ORDER BY Date DESC;
        ";

                DataTable dt = studenthistory.GetData(query);
                dgvHistory.DataSource = dt;
                dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student history: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
