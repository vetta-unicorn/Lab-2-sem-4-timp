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
        private int level;
        private string name;
        private string clickName;

        public Item(int lev, string n, string _clickName)
        {
            level = lev;
            name = n;
            clickName = _clickName;
        }

        public Item() { }

        public int GetLevel() {  return level; }
        public string GetName() { return name; }
        public string GetClickName() { return clickName; }
    }

}