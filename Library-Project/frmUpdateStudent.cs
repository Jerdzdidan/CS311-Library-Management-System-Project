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
    public partial class frmUpdateStudent : Form
    {
        Class1 updateform = new Class1("127.0.0.1", "cs311_library_proj", "benidigs", "aquino");
        private string editID, editname, editgrade, editsection, username;
        public frmUpdateStudent(string username, string editID, string editname, string editgrade, string editsection)
        {
            InitializeComponent();
            this.editID = editID;
            this.editname = editname;
            this.editgrade = editgrade;
            this.editsection = editsection;
            this.username = username;
        }
    }
}
