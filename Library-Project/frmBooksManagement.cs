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
    }
}
