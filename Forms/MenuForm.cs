using Authorization;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Runtime.Remoting.Lifetime;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Forms
{
    public partial class MenuForm : Form
    {
        private ClassLibrary.Menu menu;
        private Authorize authorize;
        private string filePath = "Menu.txt";
        private string allUsersPath = "USERS.txt";
        private string currUserPath = "User.json";

        private delegate void PrintDelegate(string message);

        public MenuForm()
        {
            InitializeComponent();
            menu = new ClassLibrary.Menu(filePath);
            menu.SetMenu();
            authorize = new Authorize(allUsersPath);
            authorize.SetUserList();

            InitializeMenuStrip(menu.menu);
        }

        public void SetStatus(ToolStripMenuItem menuItem, Tree tree)
        {
            string jsonString = File.ReadAllText(currUserPath);
            User currUser = JsonSerializer.Deserialize<User>(jsonString);
            int status = authorize.GetAccessLevel(tree.root.name, currUser);
            switch (status)
            {
                case 0:
                    menuItem.Visible = true;
                    menuItem.Enabled = true;
                    break;
                case 1:
                    menuItem.Visible = true;
                    menuItem.Enabled = false;
                    break;
                case 2:
                    menuItem.Visible = false;
                    menuItem.Enabled = false;
                    break;
                case -1:
                    MessageBox.Show("Unable to set entry status!");
                    break;
            }
        }

        private void InitializeMenuStrip(List<Tree> trees)
        {
            foreach (var tree in trees)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(tree.root.name);
                PrintDelegate printMethod;

                if (tree.children == null || tree.children.Count() == 0)
                {
                    printMethod = (message) =>
                    {
                        MessageBox.Show($"You have calles method: {message}");
                    };
                }

                else
                {
                    printMethod = (message) => { };
                }

                menuItem.Click += (sender, e) => printMethod(tree.root.clickName);
                SetStatus(menuItem, tree);

                if (tree.children != null && tree.children.Count > 0)
                {
                    InitializeSubMenu(menuItem, tree.children);
                }

                menuStrip1.Items.Add(menuItem);
            }
        }

        private void InitializeSubMenu(ToolStripMenuItem parentMenuItem, List<Tree> children)
        {
            foreach (var child in children)
            {
                ToolStripMenuItem childMenuItem = new ToolStripMenuItem(child.root.name);

                PrintDelegate printMethod;
                if (child.children == null || child.children.Count() == 0)
                {
                    printMethod = (message) =>
                    {
                        MessageBox.Show($"You have called method: {message}");
                    };
                }

                else
                {
                    printMethod = (message) => { };
                }

                childMenuItem.Click += (sender, e) =>printMethod(child.root.clickName);
                SetStatus(childMenuItem, child);

                if (child.children != null && child.children.Count > 0)
                {
                    InitializeSubMenu(childMenuItem, child.children);
                }

                parentMenuItem.DropDownItems.Add(childMenuItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }

}

