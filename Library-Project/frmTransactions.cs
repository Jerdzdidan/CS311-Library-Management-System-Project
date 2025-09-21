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
        public partial class frmTransactions : Form
        {
            Class1 booktransac = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
            private string username;
            public frmTransactions(string username)
            {
                InitializeComponent();
                this.username = username;
            }
            private void frmTransactions_Load(object sender, EventArgs e)
            {
                LoadAllTransactions();
            }
            private void LoadAllTransactions()
            {
                try
                {
                    DataTable dt = booktransac.GetData("SELECT bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
                                                       "FROM tbl_transac ORDER BY borrowdate DESC");
                    dataGridView1.DataSource = dt;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR loading transactions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            private int row;
            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        row = e.RowIndex;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on datagridview1_CellContentClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            private void btnReset_Click(object sender, EventArgs e)
            {
                txtSearch.Clear();
                dtpDate.Value = DateTime.Now;
                LoadAllTransactions();
            }
            private void btnSearch_Click(object sender, EventArgs e)
            {
                try
                {
                    string keyword = txtSearch.Text.Trim();
                    string selectedDate = dtpDate.Value.ToString("yyyy/MM/dd");

                    string query = "SELECT bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
                                    "FROM tbl_transac " + "WHERE (bookCode LIKE '%" + keyword + "%' " + "OR bookTitle LIKE '%" + keyword + "%' " + "OR author LIKE '%" + keyword + "%' " +
                                    "OR category LIKE '%" + keyword + "%' " + "OR borrower LIKE '%" + keyword + "%' " + "OR borrowerType LIKE '%" + keyword + "%' " + "OR grade_section LIKE '%" + keyword + "%' " +
                                    "OR borrowdate LIKE '%" + keyword + "%' " + "OR returndate LIKE '%" + keyword + "%') " + "AND borrowdate = '" + selectedDate + "' " + "ORDER BY borrowdate DESC";
                DataTable dt = booktransac.GetData(query);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            private void dtpDate_ValueChanged(object sender, EventArgs e)
            {
                btnSearch_Click(sender, e);
            }
            private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    string keyword = txtSearch.Text.Trim();
                string selectedDate = dtpDate.Value.ToString("yyyy/MM/dd");
                string query = "SELECT bookCode, bookTitle, author, category, borrowdate, returndate, status, borrower, borrowerType, grade_section " +
                                "FROM tbl_transac " +
                                "WHERE (bookCode LIKE '%" + keyword + "%' " +
                                "OR bookTitle LIKE '%" + keyword + "%' " +
                                "OR author LIKE '%" + keyword + "%' " +
                                "OR category LIKE '%" + keyword + "%' " +
                                "OR borrower LIKE '%" + keyword + "%' " +
                                "OR borrowerType LIKE '%" + keyword + "%' " +
                                "OR grade_section LIKE '%" + keyword + "%' " +
                                "OR borrowdate LIKE '%" + keyword + "%' " +      
                                "OR returndate LIKE '%" + keyword + "%') " +   
                                "AND borrowdate = '" + selectedDate + "' " +
                                "ORDER BY borrowdate DESC";

                DataTable dt = booktransac.GetData(query);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on live search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
