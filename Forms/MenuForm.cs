using Authorization;
using ClassLibrary;
//using Forms.Presenters;
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

namespace Forms
{
    public partial class MenuForm : Form
    {
        private ClassLibrary.Menu menu;
        private string filePath = "Menu.txt";

        private delegate void PrintDelegate(string message);

        public MenuForm()
        {
            InitializeComponent();
            menu = new ClassLibrary.Menu(filePath);
            menu.SetMenu(); // Заполнение меню
            InitializeMenuStrip(menu.GetMenu());
        }

        private void InitializeMenuStrip(List<Tree> trees)
        {
            foreach (var tree in trees)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(tree.root.GetName());
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

                menuItem.Click += (sender, e) => printMethod(tree.root.GetClickName());

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
                ToolStripMenuItem childMenuItem = new ToolStripMenuItem(child.root.GetName());

                PrintDelegate printMethod;
                if (child.children == null || child.children.Count() == 0)
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

                childMenuItem.Click += (sender, e) =>printMethod(child.root.GetClickName());

                if (child.children != null && child.children.Count > 0)
                {
                    InitializeSubMenu(childMenuItem, child.children);
                }

                parentMenuItem.DropDownItems.Add(childMenuItem);
            }
        }

        //private void HandleMenuItemClick(Item item)
        //{
        //    // Если у элемента нет подменю, вызываем метод clickName
        //    if (item.GetClickName() != "")
        //    {

        //    }
        //}

        // Пример метода, который может быть вызван
        public void ExampleMethod()
        {
            MessageBox.Show("Example method invoked!");
        }
    }

}

