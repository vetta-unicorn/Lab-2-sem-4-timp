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
using System.Collections;

namespace Forms
{
    public partial class MenuForm : Form
    {
        Assembly assemblyAuthorize;
        Assembly assemblyMenu;
        Type authorizeType;
        Type userType;
        Type menuType;
        Type treeType;
        Type listType;

        private string filePath = "Menu.txt";
        private string allUsersPath = "USERS.txt";
        private string currUserPath = "User.json";

        private delegate void PrintDelegate(string message);

        public MenuForm()
        {
            InitializeComponent();

            assemblyAuthorize = Assembly.LoadFrom("Authorisation.dll");
            authorizeType = assemblyAuthorize.GetType("Authorization.Authorize");
            var authorizeInstance = Activator.CreateInstance(authorizeType, allUsersPath);

            userType = assemblyAuthorize.GetType("Authorization.User");

            assemblyMenu = Assembly.LoadFrom("ClassLibrary.dll");

            treeType = assemblyMenu.GetType("ClassLibrary.Tree");
            listType = typeof(List<>).MakeGenericType(treeType);
            var treeInstance = Activator.CreateInstance(treeType);

            menuType = assemblyMenu.GetType("ClassLibrary.Menu");
            var menuInstance = Activator.CreateInstance(menuType, filePath);

            var menuValue = menuType.GetProperty("menu", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).GetValue(menuInstance);
            var menuList = Convert.ChangeType(menuValue, listType);

            InitializeMenuStrip(menuList);
        }

        public void SetStatus(ToolStripMenuItem menuItem, object treeInstance)
        {
            string jsonString = File.ReadAllText(currUserPath);
            Type userType = assemblyAuthorize.GetType("Authorization.User");
            var currUser = JsonSerializer.Deserialize(jsonString, userType);

            var authorizeInstance = Activator.CreateInstance(authorizeType, allUsersPath);

            PropertyInfo rootProperty = treeInstance.GetType().GetProperty("root");
            var root = rootProperty.GetValue(treeInstance);

            PropertyInfo nameProperty = root.GetType().GetProperty("name");
            string treeName = (string)nameProperty.GetValue(root);

            MethodInfo getAccessLevelMethod = authorizeType.GetMethod("GetAccessLevel");
            int status = (int)getAccessLevelMethod.Invoke(authorizeInstance, new object[] { treeName, currUser });

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

        private void InitializeMenuStrip(object treesInstance)
        {
            menuType = assemblyMenu.GetType("ClassLibrary.Menu");
            var menuInstance = Activator.CreateInstance(menuType, filePath);

            PropertyInfo menuProperty = menuInstance.GetType().GetProperty("menu");
            var trees = (IList)menuProperty.GetValue(menuInstance);

            foreach (var tree in trees)
            {
                var thisTree = tree.GetType().GetProperty("root").GetValue(tree);
                Type itemType = thisTree.GetType();

                PropertyInfo nameProperty = itemType.GetProperty("name", BindingFlags.Public | BindingFlags.Instance);
                string treeName = nameProperty != null ? (string)nameProperty.GetValue(thisTree) : null;

                ToolStripMenuItem menuItem = new ToolStripMenuItem(treeName);

                PrintDelegate printMethod;

                PropertyInfo childrenProperty = tree.GetType().GetProperty("children");
                var children = (IList)childrenProperty.GetValue(tree);

                if (children == null || children.Count == 0)
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
               
                PropertyInfo clickProperty = itemType.GetProperty("name", BindingFlags.Public | BindingFlags.Instance);
                string clickName = nameProperty != null ? (string)nameProperty.GetValue(thisTree) : null;
                menuItem.Click += (sender, e) => printMethod(clickName);
                SetStatus(menuItem, tree);

                if (children != null && children.Count > 0)
                {
                    InitializeSubMenu(menuItem, children);
                }

                menuStrip1.Items.Add(menuItem);
            }
        }
        private void InitializeSubMenu(ToolStripMenuItem parentMenuItem, object childrenItems)
        {
            var children = Convert.ChangeType(childrenItems, listType);

            foreach (var child in children as IList)
            {
                var rootProperty = child.GetType().GetProperty("root");
                var rootInstance = rootProperty.GetValue(child);
                string childName = (string)rootInstance.GetType().GetProperty("name").GetValue(rootInstance);

                ToolStripMenuItem childMenuItem = new ToolStripMenuItem(childName);

                PrintDelegate printMethod;
                var childChildrenProperty = child.GetType().GetProperty("children");
                var childChildren = (IList)childChildrenProperty.GetValue(child);

                if (childChildren == null || childChildren.Count == 0)
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

                string clickName = (string)rootInstance.GetType().GetProperty("clickName").GetValue(rootInstance);
                childMenuItem.Click += (sender, e) => printMethod(clickName);
                SetStatus(childMenuItem, child);

                if (childChildren != null && childChildren.Count > 0)
                {
                    InitializeSubMenu(childMenuItem, childChildren);
                }

                parentMenuItem.DropDownItems.Add(childMenuItem);
            }
        }

        // назад
        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }

}

