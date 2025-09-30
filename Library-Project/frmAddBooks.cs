using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ticket_management;

namespace Library_Project
{
    public partial class frmAddBooks : Form
    {
        Class1 addaccount = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");

        private string username;
        public frmAddBooks(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private int errorcount;
        private void GenerateBookCode()
        {
            if (cmbCategory.SelectedIndex >= 0)
            {
                string categoryCode = cmbCategory.Text.Length >= 7 ? cmbCategory.Text.Substring(0, 7).ToUpper() : cmbCategory.Text.ToUpper();
                string yearCode = dtpDate.Value.Year.ToString();
                string dateCode = DateTime.Now.ToString("MMddyyyyHHmmss");

                txtBookCode.Text = "LIB-" + categoryCode + "-" + yearCode + "-" + dateCode;
            }
        }
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateBookCode();
        }
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            GenerateBookCode();
        }
        private void btnAdd_Click(object sender, EventArgs e)
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
            if (dtpDate.Value == null)
            {
                errorProvider1.SetError(dtpDate, "Please select a date.");
                errorcount++;
            }

            int quantity = 0;
            if (string.IsNullOrEmpty(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                errorProvider1.SetError(txtQuantity, "Please enter a valid quantity (must be greater than 0).");
                errorcount++;
            }

            if (errorcount == 0)
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to add this book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        addaccount.executeSQL(
                            "INSERT INTO tbl_books (BookID, title, author, category, status, Added_date, quantity) " +
                            "VALUES ('" + txtBookCode.Text + "', " +
                            "'" + txtTitle.Text + "', " +
                            "'" + txtAuthor.Text + "', " +
                            "'" + cmbCategory.Text + "', " +
                            "'AVAILABLE', " +
                            "'" + dtpDate.Value.ToString("yyyy/MM/dd") + "', " +
                            quantity + ")");

                        if (addaccount.rowAffected > 0)
                        {
                            MessageBox.Show("New Book Added with " + quantity + " copies.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            addaccount.executeSQL("INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby) " + "VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd") + "', " + "'" + DateTime.Now.ToShortTimeString() + "', " +
                                                  "'ADDED NEW BOOK: " + txtTitle.Text + " (Qty: " + quantity + ")', " + "'RESOURCES MANAGEMENT', " + "'" + txtBookCode.Text + "', " + "'" + username + "')");
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "ERROR on adding new book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
