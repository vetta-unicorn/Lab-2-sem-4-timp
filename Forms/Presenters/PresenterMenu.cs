using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Authorization;
using ClassLibrary;

namespace Forms.Presenters
{
    public class MenuPresenter
    {
        private readonly IMenuView _view;
        private readonly ClassLibrary.Menu _menu;
        private string filePath;
        private string fileAllUsers;
        private readonly Authorize _authorize;

        public MenuPresenter(IMenuView view)
        {
            filePath = "Menu.txt";
            fileAllUsers = "USERS.txt";

            _view = view;
            _menu = new ClassLibrary.Menu(filePath); // Укажите путь к файлу
            _menu.SetMenu();
            _authorize = new Authorize(fileAllUsers);
            _authorize.SetUserList();

            _view.DisplayMenu(_menu);
        }

        public void OnMenuItemClicked(Tree tree)
        {
            if (tree.children != null && tree.children.Count > 0)
            {
                _view.DisplayChildren(tree);
            }
            else
            {
                // Вызов метода с именем clickName
                InvokeClickMethod(tree.root.GetClickName());
            }
        }


        private void InvokeClickMethod(string methodName)
        {
            // Здесь можно использовать рефлексию или другой подход для вызова метода по имени
            // Например, если методы находятся в этом же классе:
            //var method = this.GetType().GetMethod(methodName);
            //method?.Invoke(this, null);
        }
    }


}
