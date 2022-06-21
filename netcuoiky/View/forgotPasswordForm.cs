using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using netcuoiky.BLL;
using netcuoiky.DTO;

namespace netcuoiky.View
{
    public partial class forgotPasswordForm : Form
    {
        public int code;
        public forgotPasswordForm()
        {
            InitializeComponent();
            codeTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            confirmPasswordTextBox.Enabled = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            if (User_BLL.Instance.checkValidMail(email))
            {
                Random random = new Random();
                code = random.Next(123123, 999999);
                EmailAccount emailAccount = new EmailAccount();
                string p = emailAccount.Password;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(emailAccount.Email);
                message.To.Add(new MailAddress(email));
                message.Subject = "change password";
                message.Body = "Write this given code on text box\n" + code + "\nThank you!";
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("viethungnguyen2002@gmail.com", p);
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }

                codeTextBox.Enabled = true;
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Lại Email !!!");
                emailTextBox.Clear();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(codeTextBox.Text) == code)
            {
                passwordTextBox.Enabled = true;
                confirmPasswordTextBox.Enabled = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (confirmPasswordTextBox.Text == passwordTextBox.Text)
            {
                string email = emailTextBox.Text;
                Account_BLL.Instance.ChangePassword(passwordTextBox.Text, email);
            }
            else
            {
                MessageBox.Show("Mật khẩu và mật khẩu xác nhận không giống nhau");
                passwordTextBox.Clear();
                confirmPasswordTextBox.Clear();
            }
        }
    }
}
