using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netcuoiky
{
    public partial class loginForm : Form
    {
        public string userId;
        public static loginForm instance;
        public loginForm()
        {
            InitializeComponent();
            instance = this;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
