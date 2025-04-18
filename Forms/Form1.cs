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
using System.Xml;
using Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

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
            string fileCurrentUser = "User.json";
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
                User user = new User(username, password);

                var jsonString = JsonSerializer.Serialize(new User(user.GetName(), user.GetPassword()));
                File.WriteAllText(fileCurrentUser, jsonString);

                var person = JsonSerializer.Deserialize<User>(jsonString);

                var Form2 = new MenuForm();
                Form2.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Error! Incorrect password or username!");
            }
        }

    }
}
