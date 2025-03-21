using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Item
    {
        private int _level;
        private string _name;
        private string _clickName;

        public int level { get { return _level; } set { _level = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string clickName { get { return _clickName; } set { _clickName = value; } }

        public Item preItem { get; set; }

        public Item(int lev, string n)
        {
            level = lev;
            name = n;
        }

        public Item(Item item, int lev, string n)
        {
            preItem = item;
            level = lev;
            name = n;
        }

    }


    public class Menu
    {

    }

}