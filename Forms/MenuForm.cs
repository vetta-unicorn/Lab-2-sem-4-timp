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

namespace Forms
{
    public partial class MenuForm : Form, IMenuView
    {
        private MenuPresenter _presenter;
        private string filePath;

        public MenuForm()
        {
            InitializeComponent();
            filePath = "Menu.txt";
            var menu = new ClassLibrary.Menu(filePath);
            _presenter = new MenuPresenter(this, menu);
            _presenter.LoadMenu(filePath);
        }

        public void DisplayMenu(List<Tree> menuItems)
        {
            // Очистите старые кнопки, если нужно
            this.Controls.Clear();

            foreach (var tree in menuItems)
            {
                AddButton(tree.root.GetName(), (s, e) => OnButtonClick(tree));
            }
        }

        public void AddButton(string name, EventHandler clickEvent)
        {
            Button button = new Button
            {
                Text = name,
                AutoSize = true
            };
            button.Click += clickEvent;
            this.Controls.Add(button);
        }

        public void ShowChildren(List<Tree> children)
        {
            foreach (var child in children)
            {
                AddButton(child.root.GetName(), (s, e) => OnChildButtonClick(child.root));
            }
        }

        private void OnButtonClick(Tree tree)
        {
            if (tree.children == null || !tree.children.Any())
            {
                // Вызов метода clickName
                // Например, можно использовать Reflection для вызова метода по имени
                label1.Text = "Testing";
            }
            else
            {
                ShowChildren(tree.children);
            }
        }

        private void OnChildButtonClick(Item item)
        {
            // Вызов метода clickName для дочернего элемента
            label1.Text = "Testing";
        }
    }

}
