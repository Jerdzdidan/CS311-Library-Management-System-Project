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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorcount = 0;

            // === Input Validations ===
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                errorProvider1.SetError(txtBarcode, "Please scan or enter the book barcode.");
                errorcount++;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Title is required.");
                errorcount++;
            }

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                errorProvider1.SetError(txtAuthor, "Author is required.");
                errorcount++;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbCategory, "Select a category.");
                errorcount++;
            }

            int quantity = 0;
            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                errorProvider1.SetError(txtQuantity, "Enter a valid quantity greater than 0.");
                errorcount++;
            }

            if (errorcount > 0)
                return;

            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to add this book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    // Use the scanned barcode as the BookID
                    string bookID = txtBarcode.Text.Trim();

                    // Check if barcode already exists
                    DataTable existing = addaccount.GetData($"SELECT * FROM tbl_books WHERE BookID = '{bookID}'");
                    if (existing.Rows.Count > 0)
                    {
                        MessageBox.Show("A book with this barcode already exists.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert new book record
                    addaccount.executeSQL($@"
                        INSERT INTO tbl_books (BookID, title, author, category, status, Added_date, quantity)
                        VALUES ('{bookID}', '{txtTitle.Text}', '{txtAuthor.Text}', '{cmbCategory.Text}', 
                                'AVAILABLE', '{DateTime.Now:yyyy/MM/dd}', {quantity})");

                    if (addaccount.rowAffected > 0)
                    {
                        MessageBox.Show($"Book successfully added with {quantity} copies.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Log
                        addaccount.executeSQL($@"
                            INSERT INTO tbl_logs (datelog, timelog, action, module, performedto, performedby)
                            VALUES ('{DateTime.Now:yyyy/MM/dd}', '{DateTime.Now:hh\\:mm tt}',
                                    'ADDED NEW BOOK: {txtTitle.Text} (Qty: {quantity})',
                                    'BOOK MANAGEMENT', '{bookID}', '{username}')");

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
