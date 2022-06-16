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
    public partial class adminForm : Form
    {
        public static adminForm instance;
        private bool isCollapsed = true;
        public adminForm()
        {
            InitializeComponent();
            instance = this;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed == true)
            {
                panelDrop.Height += 10;
                if(B1.BorderRadius >= 0) { B1.BorderRadius -= 5; }
                if(panelDrop.Size == panelDrop.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                panelDrop.Height -= 10;
                if (B1.BorderRadius <= 15) { B1.BorderRadius += 5; }
                if (panelDrop.Size == panelDrop.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new loginForm()).Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new registerForm().Show();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
        }
    }
}
