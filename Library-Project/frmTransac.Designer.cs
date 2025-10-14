namespace Library_Project
{
    partial class frmTransac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransac));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnrefres = new System.Windows.Forms.Button();
            this.btnRetrn = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.cmbList = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(13, 62);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1140, 412);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dtpDate
            // 
            this.dtpDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(999, 25);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(153, 22);
            this.dtpDate.TabIndex = 4;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(187, 22);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(443, 27);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "       Search Transaction:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightYellow;
            this.panel1.Controls.Add(this.lblPageInfo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.cmbList);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 114);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1379, 526);
            this.panel1.TabIndex = 8;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfo.Location = new System.Drawing.Point(905, 491);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(54, 22);
            this.lblPageInfo.TabIndex = 40;
            this.lblPageInfo.Text = "Page:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(913, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 19);
            this.label3.TabIndex = 33;
            this.label3.Text = "      Date:";
            // 
            // btnPrev
            // 
            this.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrev.Location = new System.Drawing.Point(964, 484);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(96, 37);
            this.btnPrev.TabIndex = 42;
            this.btnPrev.Text = "&Next  ";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(636, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 19);
            this.label9.TabIndex = 32;
            this.label9.Text = "     Type:";
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(1057, 484);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(96, 37);
            this.btnNext.TabIndex = 41;
            this.btnNext.Text = "  &Prev";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.btnBack);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1174, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(205, 526);
            this.panel3.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnrefres);
            this.groupBox1.Controls.Add(this.btnRetrn);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 53);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(185, 172);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action buttons:";
            // 
            // btnrefres
            // 
            this.btnrefres.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnrefres.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnrefres.FlatAppearance.BorderSize = 0;
            this.btnrefres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefres.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefres.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnrefres.Image = ((System.Drawing.Image)(resources.GetObject("btnrefres.Image")));
            this.btnrefres.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrefres.Location = new System.Drawing.Point(7, 90);
            this.btnrefres.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnrefres.Name = "btnrefres";
            this.btnrefres.Size = new System.Drawing.Size(172, 60);
            this.btnrefres.TabIndex = 10;
            this.btnrefres.Text = "      &Refresh ";
            this.btnrefres.UseVisualStyleBackColor = false;
            this.btnrefres.Click += new System.EventHandler(this.btnrefres_Click);
            // 
            // btnRetrn
            // 
            this.btnRetrn.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRetrn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetrn.FlatAppearance.BorderSize = 0;
            this.btnRetrn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetrn.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetrn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRetrn.Image = ((System.Drawing.Image)(resources.GetObject("btnRetrn.Image")));
            this.btnRetrn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetrn.Location = new System.Drawing.Point(7, 25);
            this.btnRetrn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRetrn.Name = "btnRetrn";
            this.btnRetrn.Size = new System.Drawing.Size(172, 60);
            this.btnRetrn.TabIndex = 11;
            this.btnRetrn.Text = "    &Return";
            this.btnRetrn.UseVisualStyleBackColor = false;
            this.btnRetrn.Click += new System.EventHandler(this.btnRetrn_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(0, 466);
            this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(205, 60);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "              &Back                     ";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(1559, 334);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(108, 36);
            this.btnReturn.TabIndex = 7;
            this.btnReturn.Text = "       &Return              ";
            this.btnReturn.UseVisualStyleBackColor = false;
            // 
            // cmbList
            // 
            this.cmbList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbList.FormattingEnabled = true;
            this.cmbList.Items.AddRange(new object[] {
            "STUDENT ",
            "TEACHER"});
            this.cmbList.Location = new System.Drawing.Point(719, 23);
            this.cmbList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbList.Name = "cmbList";
            this.cmbList.Size = new System.Drawing.Size(185, 24);
            this.cmbList.TabIndex = 5;
            this.cmbList.SelectedIndexChanged += new System.EventHandler(this.cmbList_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(1559, 394);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(108, 36);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1379, 114);
            this.panel2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(121, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(673, 90);
            this.label2.TabIndex = 14;
            this.label2.Text = "TRANSACTIONS";
            // 
            // frmTransac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 640);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmTransac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transactions";
            this.Load += new System.EventHandler(this.frmTransac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.ComboBox cmbList;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnrefres;
        private System.Windows.Forms.Button btnRetrn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}