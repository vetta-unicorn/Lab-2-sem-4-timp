using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace Forms.Presenters
{
    public class MenuPresenter
    {
        private readonly IMenuView _view;
        private readonly Menu _menu;

        public MenuPresenter(IMenuView view, Menu menu)
        {
            _view = view;
            _menu = menu;
        }

        public void LoadMenu(string filePath)
        {
            _menu.SetMenu();
            _view.DisplayMenu(_menu.GetMenu());
        }
    }

}
