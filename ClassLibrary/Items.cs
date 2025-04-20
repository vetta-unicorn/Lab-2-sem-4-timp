using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    public class Item
    {
        public int level { get; private set; }
        public string name { get; private set; }
        public string clickName { get; private set; }

        public Item(int lev, string n, string _clickName)
        {
            level = lev;
            name = n;
            clickName = _clickName;
        }

        public Item() { }
    }

}