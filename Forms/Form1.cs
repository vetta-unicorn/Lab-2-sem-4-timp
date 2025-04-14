using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Authorization;

namespace Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // очищает ввод
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
        }

        // вход

        private void button2_Click(object sender, EventArgs e)
        {
            string fileUsers = "USERS.txt";
            string username = "default";
            string password = "default";

            try
            {
                username = Convert.ToString(textBox1.Text);
                password = Convert.ToString(textBox2.Text);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

            Authorize authorize = new Authorize(fileUsers);
            authorize.SetUserList();

            if (authorize.CheckAccount(username, password))
            {
                var Form2 = new Form2(this);
                Form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error! Incorrect password or username!");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
