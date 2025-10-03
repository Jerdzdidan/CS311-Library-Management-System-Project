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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBooksManagement));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnUnavaliable = new System.Windows.Forms.Button();
            this.btnAvailable = new System.Windows.Forms.Button();
            this.btnDamage = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.btnUpdateBook = new System.Windows.Forms.Button();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbList = new System.Windows.Forms.ComboBox();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(13, 62);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1026, 564);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(144, 23);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(606, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search Resources:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReplace);
            this.groupBox1.Controls.Add(this.btnUnavaliable);
            this.groupBox1.Controls.Add(this.btnAvailable);
            this.groupBox1.Controls.Add(this.btnDamage);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(20, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 223);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Book Status";
            // 
            // btnReplace
            // 
            this.btnReplace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReplace.FlatAppearance.BorderSize = 0;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplace.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplace.Image = ((System.Drawing.Image)(resources.GetObject("btnReplace.Image")));
            this.btnReplace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReplace.Location = new System.Drawing.Point(10, 171);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(2);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(156, 37);
            this.btnReplace.TabIndex = 10;
            this.btnReplace.Text = " &Replaced             ";
            this.btnReplace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnUnavaliable
            // 
            this.btnUnavaliable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnavaliable.FlatAppearance.BorderSize = 0;
            this.btnUnavaliable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnavaliable.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnavaliable.Image = ((System.Drawing.Image)(resources.GetObject("btnUnavaliable.Image")));
            this.btnUnavaliable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnavaliable.Location = new System.Drawing.Point(10, 74);
            this.btnUnavaliable.Margin = new System.Windows.Forms.Padding(2);
            this.btnUnavaliable.Name = "btnUnavaliable";
            this.btnUnavaliable.Size = new System.Drawing.Size(156, 37);
            this.btnUnavaliable.TabIndex = 9;
            this.btnUnavaliable.Text = "&Unavailable        ";
            this.btnUnavaliable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUnavaliable.UseVisualStyleBackColor = true;
            this.btnUnavaliable.Click += new System.EventHandler(this.btnUnavaliable_Click);
            // 
            // btnAvailable
            // 
            this.btnAvailable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAvailable.FlatAppearance.BorderSize = 0;
            this.btnAvailable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAvailable.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvailable.Image = ((System.Drawing.Image)(resources.GetObject("btnAvailable.Image")));
            this.btnAvailable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAvailable.Location = new System.Drawing.Point(10, 26);
            this.btnAvailable.Margin = new System.Windows.Forms.Padding(2);
            this.btnAvailable.Name = "btnAvailable";
            this.btnAvailable.Size = new System.Drawing.Size(156, 37);
            this.btnAvailable.TabIndex = 6;
            this.btnAvailable.Text = "&Available            ";
            this.btnAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAvailable.UseVisualStyleBackColor = true;
            this.btnAvailable.Click += new System.EventHandler(this.btnAvailable_Click);
            // 
            // btnDamage
            // 
            this.btnDamage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDamage.FlatAppearance.BorderSize = 0;
            this.btnDamage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDamage.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDamage.Image = ((System.Drawing.Image)(resources.GetObject("btnDamage.Image")));
            this.btnDamage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDamage.Location = new System.Drawing.Point(10, 123);
            this.btnDamage.Margin = new System.Windows.Forms.Padding(2);
            this.btnDamage.Name = "btnDamage";
            this.btnDamage.Size = new System.Drawing.Size(156, 37);
            this.btnDamage.TabIndex = 4;
            this.btnDamage.Text = "&Damage              ";
            this.btnDamage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDamage.UseVisualStyleBackColor = true;
            this.btnDamage.Click += new System.EventHandler(this.btnDamage_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddBook);
            this.groupBox2.Controls.Add(this.btnUpdateBook);
            this.groupBox2.Controls.Add(this.btnDeleteBook);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(20, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(176, 185);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Command Button";
            // 
            // btnAddBook
            // 
            this.btnAddBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBook.FlatAppearance.BorderSize = 0;
            this.btnAddBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBook.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddBook.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBook.Image")));
            this.btnAddBook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddBook.Location = new System.Drawing.Point(10, 27);
            this.btnAddBook.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(156, 37);
            this.btnAddBook.TabIndex = 1;
            this.btnAddBook.Text = "&Add Resources     ";
            this.btnAddBook.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // btnUpdateBook
            // 
            this.btnUpdateBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateBook.FlatAppearance.BorderSize = 0;
            this.btnUpdateBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateBook.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateBook.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateBook.Image")));
            this.btnUpdateBook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateBook.Location = new System.Drawing.Point(10, 75);
            this.btnUpdateBook.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateBook.Name = "btnUpdateBook";
            this.btnUpdateBook.Size = new System.Drawing.Size(156, 37);
            this.btnUpdateBook.TabIndex = 8;
            this.btnUpdateBook.Text = "&Update Resources";
            this.btnUpdateBook.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateBook.UseVisualStyleBackColor = true;
            this.btnUpdateBook.Click += new System.EventHandler(this.btnUpdateBook_Click);
            // 
            // btnDeleteBook
            // 
            this.btnDeleteBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteBook.FlatAppearance.BorderSize = 0;
            this.btnDeleteBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBook.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteBook.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteBook.Image")));
            this.btnDeleteBook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteBook.Location = new System.Drawing.Point(10, 125);
            this.btnDeleteBook.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(156, 37);
            this.btnDeleteBook.TabIndex = 3;
            this.btnDeleteBook.Text = "&Delete Resources";
            this.btnDeleteBook.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteBook.UseVisualStyleBackColor = true;
            this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1056, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 635);
            this.panel2.TabIndex = 12;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(25, 592);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(156, 37);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "&Refresh  ";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightYellow;
            this.panel3.Controls.Add(this.cmbList);
            this.panel3.Controls.Add(this.dtpFilterDate);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Controls.Add(this.txtSearch);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 94);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1056, 635);
            this.panel3.TabIndex = 13;
            // 
            // cmbList
            // 
            this.cmbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbList.FormattingEnabled = true;
            this.cmbList.Items.AddRange(new object[] {
            "ALL",
            "AVAILABLE",
            "UNAVAILABLE",
            "DAMAGED"});
            this.cmbList.Location = new System.Drawing.Point(756, 22);
            this.cmbList.Margin = new System.Windows.Forms.Padding(2);
            this.cmbList.Name = "cmbList";
            this.cmbList.Size = new System.Drawing.Size(144, 21);
            this.cmbList.TabIndex = 8;
            this.cmbList.SelectedIndexChanged += new System.EventHandler(this.cmbList_SelectedIndexChanged);
            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilterDate.Location = new System.Drawing.Point(905, 22);
            this.dtpFilterDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.Size = new System.Drawing.Size(90, 20);
            this.dtpFilterDate.TabIndex = 7;
            this.dtpFilterDate.ValueChanged += new System.EventHandler(this.dtpFilterDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(424, 73);
            this.label2.TabIndex = 12;
            this.label2.Text = "RESOURCES";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1166, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(98, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1054, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(112, 94);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 94);
            this.panel1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(504, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(517, 73);
            this.label3.TabIndex = 14;
            this.label3.Text = "MANAGEMENT";
            // 
            // frmBooksManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 729);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmBooksManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resource Management";
            this.Load += new System.EventHandler(this.frmBooksManagement_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnDamage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAvailable;
        private System.Windows.Forms.Button btnUnavaliable;
        private System.Windows.Forms.Button btnUpdateBook;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.ComboBox cmbList;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
    }
}

