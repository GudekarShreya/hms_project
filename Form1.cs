using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hms_new
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            passtext.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            passtext.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     

        private void login_Click(object sender, EventArgs e)
        {
            string username = usertext.Text;
            string pass = passtext.Text;
            if ((username == "username" ) && (pass == "pass123"))
            {
                this.Hide();
                dashboard dash = new dashboard();
                dash.Show();
            }
            else
            {
                MessageBox.Show("You have entered wrong username or password");
            }
        }
    }
}
