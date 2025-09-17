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
    }
}
