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
    public partial class frmResources : Form
    {
        Class1 resources = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string username;
        private int row;

        private int currentPage = 1;
        private int pageSize = 10;
        public frmResources(string username)
        {
            InitializeComponent();
            this.username = username;

            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
        }
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string bookCode = row.Cells["BookID"].Value.ToString();
            string bookTitle = row.Cells["title"].Value.ToString();
            string author = row.Cells["author"].Value.ToString();
            string category = row.Cells["category"].Value.ToString();
            string status = row.Cells["status"].Value?.ToString()?.ToUpperInvariant() ?? string.Empty;

            int quantity = GetQuantityFromRow(row);

            if (status != "AVAILABLE")
            {
                MessageBox.Show($"This book cannot be borrowed.\nCurrent Status: {status}\n\nOnly books with AVAILABLE status can be borrowed.",
                               "Book Not Available",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("This book is out of stock. No copies are available to borrow.",
                               "Out of Stock",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            frmBorrowBooks borrowForm = new frmBorrowBooks(bookCode, bookTitle, author, category, username);
            borrowForm.FormClosed += (s, args) => { LoadBooks(); };
            borrowForm.ShowDialog();
        }

        private void frmResources_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFilterDate.ValueChanged += dateTimePicker1_ValueChanged;
                LoadBooks();

                dataGridView1.Columns[0].HeaderText = "Book ID";
                dataGridView1.Columns[1].HeaderText = "Title";
                dataGridView1.Columns[2].HeaderText = "Author";
                dataGridView1.Columns[3].HeaderText = "Category";
                dataGridView1.Columns[4].HeaderText = "Status";
                dataGridView1.Columns[5].HeaderText = "Added Date";
                dataGridView1.Columns[6].HeaderText = "Quantity";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "ERROR on frmResources_Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetQuantityFromRow(DataGridViewRow row)
        {
            string qtyString = "";

            if (dataGridView1.Columns.Contains("quantity"))
                qtyString = row.Cells["quantity"].Value?.ToString() ?? "0";
            else if (row.Cells.Count > 6)
                qtyString = row.Cells[6].Value?.ToString() ?? "0";

            int qty;
            if (!int.TryParse(qtyString, out qty))
                qty = 0;

            return qty;
        }

        private void txtsrch_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadBooks();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedDate = dtpFilterDate.Value.ToString("yyyy/MM/dd");
                string query = $"SELECT * FROM tbl_books WHERE DATE(Added_date) = '{selectedDate}' ORDER BY BookID LIMIT {pageSize}";
                DataTable dt = resources.GetData(query);

                dataGridView1.DataSource = dt;
                ApplyRowColor();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error filtering by date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyRowColor()
        {
            if (dataGridView1.Rows.Count == 0) return;

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.IsNewRow) continue;

                DataGridViewCell statusCell = null;
                DataGridViewCell quantityCell = null;

                if (dataGridView1.Columns.Contains("status"))
                    statusCell = r.Cells["status"];
                else if (dataGridView1.Columns.Count > 4)
                    statusCell = r.Cells[4];

                if (dataGridView1.Columns.Contains("quantity"))
                    quantityCell = r.Cells["quantity"];
                else if (dataGridView1.Columns.Count > 6)
                    quantityCell = r.Cells[6];

                string status = statusCell?.Value?.ToString()?.ToUpperInvariant() ?? string.Empty;
                int quantity = 0;
                int.TryParse(quantityCell?.Value?.ToString(), out quantity);

                switch (status)
                {
                    case "AVAILABLE":
                    case "REPLACED":
                        if (quantity > 0)
                        {
                            r.DefaultCellStyle.BackColor = Color.Honeydew;
                            r.DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            r.DefaultCellStyle.BackColor = Color.LemonChiffon;
                            r.DefaultCellStyle.ForeColor = Color.Black;
                        }
                        break;

                    case "BORROWED":
                        r.DefaultCellStyle.BackColor = Color.Moccasin;
                        r.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    case "DAMAGED":
                        r.DefaultCellStyle.BackColor = Color.LightPink;
                        r.DefaultCellStyle.ForeColor = Color.Black;
                        break;

                    default:
                        r.DefaultCellStyle.BackColor = Color.White;
                        r.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyRowColor();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            txtSearch.Clear();
            LoadBooks();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            frmTransac transacForm = new frmTransac(username);

            transacForm.FormClosed += (s, args) =>
            {
                LoadBooks(); // ✅ directly reload the books, don't call frmResources_Load
            };

            transacForm.ShowDialog();
        }
        private void LoadBooks()
        {
            try
            {
                int offset = (currentPage - 1) * pageSize;
                string searchText = txtSearch.Text.Trim().Replace("'", "''");
                string whereClause = "";

                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = $" WHERE (BookID LIKE '%{searchText}%' OR title LIKE '%{searchText}%' OR author LIKE '%{searchText}%' OR category LIKE '%{searchText}%')";
                }

                // ✅ Fetch one extra record to check for next page
                string query = $"SELECT * FROM tbl_books {whereClause} ORDER BY BookID ASC LIMIT {pageSize + 1} OFFSET {offset}";
                DataTable dt = resources.GetData(query);

                bool hasNextPage = dt.Rows.Count > pageSize;
                if (hasNextPage)
                    dt.Rows.RemoveAt(dt.Rows.Count - 1); // remove the extra row

                dataGridView1.DataSource = dt;
                ApplyRowColor();

                // ✅ Enable / Disable buttons properly
                btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = hasNextPage;

                // ✅ Update label info
                lblPageInfo.Text = $"Page {currentPage}" + (hasNextPage ? " →" : "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading resources: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            LoadBooks();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadBooks();
            }
        }
    }
}

