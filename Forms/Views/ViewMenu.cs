using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms
{
    public interface IMenuView
    {
        void DisplayMenu(List<Tree> menuItems);
        void AddButton(string name, EventHandler clickEvent);
        void ShowChildren(List<Tree> children);
    }



}
