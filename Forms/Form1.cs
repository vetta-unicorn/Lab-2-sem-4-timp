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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Reflection;


namespace Forms
{
    public partial class Form1 : Form
    {
        Assembly assembly;
        Type authorizeType;
        Type userType;
        public Form1()
        {
            InitializeComponent();

            assembly = Assembly.LoadFrom("Authorisation.dll");
            authorizeType = assembly.GetType("Authorization.Authorize");

            userType = assembly.GetType("Authorization.User");
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

            // Создаем экземпляр класса, передавая аргумент в конструктор
            var authorizeInstance = Activator.CreateInstance(authorizeType, fileUsers);

            // Вызываем метод MyMethod с помощью Reflection
            MethodInfo SetUserList = authorizeType.GetMethod("SetUserList");
            SetUserList.Invoke(authorizeInstance, null); // Вызов метода

            //Authorize authorize = new Authorize(fileUsers);
            //authorize.SetUserList();

            MethodInfo CheckAccount = authorizeType.GetMethod("CheckAccount");
            object[] parameters = new object[] { username, password };
            bool Flag = Convert.ToBoolean(CheckAccount.Invoke(authorizeInstance, parameters));

            if (Flag)
            {
                var userInstance = Activator.CreateInstance(userType, username, password);
                //User user = new User(username, password);

                var jsonString = JsonSerializer.Serialize(userInstance);
                File.WriteAllText(fileCurrentUser, jsonString);


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
