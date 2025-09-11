namespace Library_Project
{
    partial class frmBooksManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.btnDamage = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnew = new System.Windows.Forms.Button();
            this.btnUpdateBook = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 74);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(852, 412);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnAddBook
            // 
            this.btnAddBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBook.Location = new System.Drawing.Point(77, 42);
            this.btnAddBook.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(107, 30);
            this.btnAddBook.TabIndex = 1;
            this.btnAddBook.Text = "&Add Book ";
            this.btnAddBook.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 36);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(773, 22);
            this.textBox1.TabIndex = 2;
            // 
            // btnDeleteBook
            // 
            this.btnDeleteBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteBook.Location = new System.Drawing.Point(77, 113);
            this.btnDeleteBook.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(107, 30);
            this.btnDeleteBook.TabIndex = 3;
            this.btnDeleteBook.Text = "&Delete Book";
            this.btnDeleteBook.UseVisualStyleBackColor = true;
            // 
            // btnDamage
            // 
            this.btnDamage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDamage.Location = new System.Drawing.Point(181, 32);
            this.btnDamage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDamage.Name = "btnDamage";
            this.btnDamage.Size = new System.Drawing.Size(107, 30);
            this.btnDamage.TabIndex = 4;
            this.btnDamage.Text = "&Damage";
            this.btnDamage.UseVisualStyleBackColor = true;
            // 
            // btnReplace
            // 
            this.btnReplace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReplace.Location = new System.Drawing.Point(27, 32);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(107, 30);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "&Replace Book";
            this.btnReplace.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnew);
            this.groupBox1.Controls.Add(this.btnDamage);
            this.groupBox1.Controls.Add(this.btnReplace);
            this.groupBox1.Location = new System.Drawing.Point(548, 528);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(316, 143);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status Actions";
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(27, 90);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "&Unavailable";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnew
            // 
            this.btnew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnew.Location = new System.Drawing.Point(181, 90);
            this.btnew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnew.Name = "btnew";
            this.btnew.Size = new System.Drawing.Size(107, 30);
            this.btnew.TabIndex = 6;
            this.btnew.Text = "&New";
            this.btnew.UseVisualStyleBackColor = true;
            // 
            // btnUpdateBook
            // 
            this.btnUpdateBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateBook.Location = new System.Drawing.Point(77, 76);
            this.btnUpdateBook.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdateBook.Name = "btnUpdateBook";
            this.btnUpdateBook.Size = new System.Drawing.Size(107, 30);
            this.btnUpdateBook.TabIndex = 8;
            this.btnUpdateBook.Text = "&Update Book";
            this.btnUpdateBook.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddBook);
            this.groupBox2.Controls.Add(this.btnUpdateBook);
            this.groupBox2.Controls.Add(this.btnDeleteBook);
            this.groupBox2.Location = new System.Drawing.Point(20, 528);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(244, 165);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // frmBooksManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 730);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmBooksManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Management";
            this.Load += new System.EventHandler(this.frmBooksManagement_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnDamage;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnew;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnUpdateBook;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

