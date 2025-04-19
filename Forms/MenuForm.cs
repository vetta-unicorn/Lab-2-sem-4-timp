using Authorization;
using ClassLibrary;
using Forms.Presenters;
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

namespace Forms
{
    public partial class MenuForm : Form, IMenuView
    {

        private MenuPresenter _presenter;

        private int buttonHeight = 40; 
        private int buttonMargin = 10;

        private string fileUser = "User.json";
        private string fileAllUsers = "USERS.txt";

        public MenuForm()
        {
            InitializeComponent();
            _presenter = new MenuPresenter(this);
        }

        public int GetAccesFileLevel(string entryName)
        {
            var jsonString = File.ReadAllText(fileUser);
            var userData = JsonSerializer.Deserialize<User>(jsonString);
            Authorize authorize = new Authorize(fileAllUsers);
            authorize.SetUserList();
            return authorize.GetAccessLevel(entryName, userData);
        }

        public Button FindButton(Tree tree)
        {
            Button treeButton = new Button
            {
                Tag = null
            };

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Button button && button.Text == tree.root.GetName())
                {
                    treeButton = button;
                }
            }

            return treeButton;
        }

        public void DisplayChildren(Tree parent)
        {
            Button parentButton = FindButton(parent);

            // Проверка на null
            if (parentButton == null)
            {
                MessageBox.Show("Ошибка: не найдена кнопка!");
                return;
            }

            // Получаем локальные координаты кнопки
            Point buttonLocation = parentButton.Location;

            // Проверяем, существует ли subPanel для parent
            ButtonTags tags = parentButton.Tag as ButtonTags;
            FlowLayoutPanel subPanel = tags.panel;

            if (subPanel == null || subPanel.Controls.Count == 0)
            {
                // Создаем и настраиваем subPanel
                subPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    Location = new Point(buttonLocation.X, buttonLocation.Y + parentButton.Height),
                    AutoSize = true,
                    Visible = true,
                };

                // Добавляем кнопки для дочерних элементов
                foreach (var child in parent.children)
                {
                    if (child != null)
                    {
                        int status = GetAccesFileLevel(child.root.GetName());
                        var childButton = new Button
                        {
                            Text = child.root.GetName(),
                            Tag = new ButtonTags(child),
                            AutoSize = true
                        };
                        childButton.Click += Button_Click;
                        SetButtonStatus(childButton, status);
                        subPanel.Controls.Add(childButton);
                    }
                }

                // Добавляем subPanel в родительский контейнер только один раз
                flowLayoutPanel1.Controls.Add(subPanel);
                tags.panel = subPanel; // Сохраняем ссылку на subPanel в кнопке
                flowLayoutPanel1.Refresh();
            }
            else
            {
                // Если subPanel уже существует, просто переключаем его видимость
                subPanel.Visible = !subPanel.Visible;
                flowLayoutPanel1.Refresh();
            }
        }


        public void DisplayMenu(ClassLibrary.Menu menu)
        {
            foreach (Tree tree in menu.GetMenu())
            {
                if (tree != null)
                {
                    int status = GetAccesFileLevel(tree.root.GetName());
                    var button = new Button
                    {
                        Text = tree.root.GetName(),
                        Tag = new ButtonTags(tree), 
                        AutoSize = true
                    };
                    button.Click += Button_Click;
                    SetButtonStatus(button, status);
                    
                    if (tree.root.GetLevel() == 0)
                    {
                        flowLayoutPanel1.Controls.Add(button);
                    }
                }
            }
        }

        public void SetButtonStatus(Button button, int status)
        {
            switch (status)
            {
                case 0:
                    button.Visible = true;
                    button.Enabled = true;
                    break;
                case 1:
                    button.Visible = true;
                    button.Enabled = false;
                    break;
                case 2:
                    button.Visible = false;
                    button.Enabled = false;
                    break;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var buttonTag = button.Tag as ButtonTags;
            Tree tree = buttonTag.tree;

            if (tree == null)
            {
                MessageBox.Show("Ошибка: не найдено значение кнопки!");
                return;
            }

            try
            {
                // Проверяем, есть ли у элемента дочерние элементы
                if (tree.children == null || tree.children.Count == 0)
                {
                    // Вызываем метод по имени clickName
                    InvokeMethod(tree.root.GetClickName());
                }
                else
                {
                    DisplayChildren(tree);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void InvokeMethod(string methodName)
        {
            ClassLibrary.Menu menu = new ClassLibrary.Menu(fileAllUsers);
            menu.SetMenu();
            var listTree = menu.GetMenu();

            foreach (Tree tree in listTree)
            {
                if (methodName == tree.root.GetClickName())
                {
                    MessageBox.Show($"You have successfully called {methodName}!");
                }
            }
        }
    }
}
  
