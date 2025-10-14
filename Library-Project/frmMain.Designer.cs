namespace Library_Project
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnAttendance = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnResource = new System.Windows.Forms.Button();
            this.btnresources = new System.Windows.Forms.Button();
            this.btnlogs = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnaccounts = new System.Windows.Forms.Button();
            this.btnTeacher = new System.Windows.Forms.Button();
            this.btnStudentsManagement = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnlogout = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnlogout);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 915);
            this.panel1.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnResource);
            this.groupBox3.Controls.Add(this.btnDashboard);
            this.groupBox3.Controls.Add(this.btnAttendance);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox3.Location = new System.Drawing.Point(25, 137);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(320, 177);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attendance And Borrow/Return";
            // 
            // btnDashboard
            // 
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(8, 20);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(300, 46);
            this.btnDashboard.TabIndex = 12;
            this.btnDashboard.Tag = "";
            this.btnDashboard.Text = "Dashboard                       ";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAttendance
            // 
            this.btnAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendance.FlatAppearance.BorderSize = 0;
            this.btnAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendance.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAttendance.Image = ((System.Drawing.Image)(resources.GetObject("btnAttendance.Image")));
            this.btnAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendance.Location = new System.Drawing.Point(8, 68);
            this.btnAttendance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(300, 46);
            this.btnAttendance.TabIndex = 15;
            this.btnAttendance.Tag = "";
            this.btnAttendance.Text = "Attendance                      ";
            this.btnAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAttendance.UseVisualStyleBackColor = true;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnlogs);
            this.groupBox2.Controls.Add(this.btnAbout);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(25, 633);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(320, 155);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "System Logs and About";
            // 
            // btnResource
            // 
            this.btnResource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResource.FlatAppearance.BorderSize = 0;
            this.btnResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResource.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResource.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnResource.Image = ((System.Drawing.Image)(resources.GetObject("btnResource.Image")));
            this.btnResource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResource.Location = new System.Drawing.Point(8, 114);
            this.btnResource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResource.Name = "btnResource";
            this.btnResource.Size = new System.Drawing.Size(300, 46);
            this.btnResource.TabIndex = 13;
            this.btnResource.Tag = "";
            this.btnResource.Text = "Borrow && Return            ";
            this.btnResource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnResource.UseVisualStyleBackColor = true;
            this.btnResource.Click += new System.EventHandler(this.btnResource_Click);
            // 
            // btnresources
            // 
            this.btnresources.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnresources.FlatAppearance.BorderSize = 0;
            this.btnresources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnresources.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnresources.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnresources.Image = ((System.Drawing.Image)(resources.GetObject("btnresources.Image")));
            this.btnresources.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnresources.Location = new System.Drawing.Point(8, 182);
            this.btnresources.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnresources.Name = "btnresources";
            this.btnresources.Size = new System.Drawing.Size(300, 46);
            this.btnresources.TabIndex = 6;
            this.btnresources.Tag = "";
            this.btnresources.Text = "Resources Management ";
            this.btnresources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnresources.UseVisualStyleBackColor = true;
            this.btnresources.Click += new System.EventHandler(this.btnresources_Click);
            // 
            // btnlogs
            // 
            this.btnlogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlogs.FlatAppearance.BorderSize = 0;
            this.btnlogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogs.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogs.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnlogs.Image = ((System.Drawing.Image)(resources.GetObject("btnlogs.Image")));
            this.btnlogs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlogs.Location = new System.Drawing.Point(10, 39);
            this.btnlogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnlogs.Name = "btnlogs";
            this.btnlogs.Size = new System.Drawing.Size(300, 46);
            this.btnlogs.TabIndex = 7;
            this.btnlogs.Tag = "";
            this.btnlogs.Text = "            System Logs          ";
            this.btnlogs.UseVisualStyleBackColor = true;
            this.btnlogs.Click += new System.EventHandler(this.btnlogs_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnaccounts);
            this.groupBox1.Controls.Add(this.btnresources);
            this.groupBox1.Controls.Add(this.btnTeacher);
            this.groupBox1.Controls.Add(this.btnStudentsManagement);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(25, 345);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(320, 267);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Management Menu:";
            // 
            // btnaccounts
            // 
            this.btnaccounts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnaccounts.FlatAppearance.BorderSize = 0;
            this.btnaccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaccounts.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaccounts.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnaccounts.Image = ((System.Drawing.Image)(resources.GetObject("btnaccounts.Image")));
            this.btnaccounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnaccounts.Location = new System.Drawing.Point(7, 26);
            this.btnaccounts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnaccounts.Name = "btnaccounts";
            this.btnaccounts.Size = new System.Drawing.Size(300, 46);
            this.btnaccounts.TabIndex = 0;
            this.btnaccounts.Tag = "";
            this.btnaccounts.Text = "Accounts Management   ";
            this.btnaccounts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnaccounts.UseVisualStyleBackColor = true;
            this.btnaccounts.Click += new System.EventHandler(this.btnaccounts_Click);
            // 
            // btnTeacher
            // 
            this.btnTeacher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTeacher.FlatAppearance.BorderSize = 0;
            this.btnTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeacher.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeacher.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnTeacher.Image = ((System.Drawing.Image)(resources.GetObject("btnTeacher.Image")));
            this.btnTeacher.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTeacher.Location = new System.Drawing.Point(7, 128);
            this.btnTeacher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTeacher.Name = "btnTeacher";
            this.btnTeacher.Size = new System.Drawing.Size(300, 46);
            this.btnTeacher.TabIndex = 14;
            this.btnTeacher.Tag = "";
            this.btnTeacher.Text = "Teachers Management   ";
            this.btnTeacher.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTeacher.UseVisualStyleBackColor = true;
            this.btnTeacher.Click += new System.EventHandler(this.btnTeacher_Click_1);
            // 
            // btnStudentsManagement
            // 
            this.btnStudentsManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStudentsManagement.FlatAppearance.BorderSize = 0;
            this.btnStudentsManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentsManagement.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudentsManagement.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnStudentsManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnStudentsManagement.Image")));
            this.btnStudentsManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStudentsManagement.Location = new System.Drawing.Point(7, 78);
            this.btnStudentsManagement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStudentsManagement.Name = "btnStudentsManagement";
            this.btnStudentsManagement.Size = new System.Drawing.Size(300, 46);
            this.btnStudentsManagement.TabIndex = 11;
            this.btnStudentsManagement.Tag = "";
            this.btnStudentsManagement.Text = "Students Management    ";
            this.btnStudentsManagement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStudentsManagement.UseVisualStyleBackColor = true;
            this.btnStudentsManagement.Click += new System.EventHandler(this.btnStudentsManagement_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.Location = new System.Drawing.Point(15, 93);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(295, 46);
            this.btnAbout.TabIndex = 10;
            this.btnAbout.Tag = "";
            this.btnAbout.Text = "    About     ";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnlogout
            // 
            this.btnlogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnlogout.FlatAppearance.BorderSize = 0;
            this.btnlogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogout.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogout.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnlogout.Image = ((System.Drawing.Image)(resources.GetObject("btnlogout.Image")));
            this.btnlogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlogout.Location = new System.Drawing.Point(0, 869);
            this.btnlogout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnlogout.Name = "btnlogout";
            this.btnlogout.Size = new System.Drawing.Size(371, 46);
            this.btnlogout.TabIndex = 8;
            this.btnlogout.Tag = "";
            this.btnlogout.Text = "Logout    ";
            this.btnlogout.UseVisualStyleBackColor = true;
            this.btnlogout.Click += new System.EventHandler(this.btnlogout_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(371, 118);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(257, 9);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(131, 84);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(111, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Management";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(111, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Library";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(105, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 915);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnaccounts;
        private System.Windows.Forms.Button btnlogout;
        private System.Windows.Forms.Button btnlogs;
        private System.Windows.Forms.Button btnresources;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnStudentsManagement;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnResource;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnTeacher;
        private System.Windows.Forms.Button btnAttendance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}