using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using netcuoiky.BLL;
using netcuoiky.DTO;

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

        private void signInButton_Click(object sender, EventArgs e)
        {
            string Username = userNameTextBox.Text;
            string Password = passwordTextBox.Text;
            Account loginAccount = Account_BLL.Instance.Login(Username, Password);
            if (loginAccount != null)
            {
                if (loginAccount.role == "Admin")
                {
                    this.Hide();
                    new adminForm().ShowDialog();
                }
                else if(loginAccount.role == "Student")
                {
                    userId = loginAccount.userId;
                    this.Hide();
                    new userForm().ShowDialog();
                }
                else
                {
                    
                }
            }
        }
    }
}
