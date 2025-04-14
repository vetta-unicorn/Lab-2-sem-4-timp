using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Menu
    {
        private List<Tree> menu;
        private ProcessFile fileHandler;

        public Menu (string filePath)
        {
            menu = new List<Tree>();
            fileHandler = new ProcessFile(filePath);
        }

        public int FindLastRoot(int level)
        {
            int len = menu.Count;
            for (int i = len - 1; i >= 0; i--)
            {
                if (menu[i].root.GetLevel() == (level - 1))
                {
                    return i;
                }
            }

            return -1;
        }

        public void SetMenu()
        {
            int i = 0;
            string[] lines = fileHandler.GetFiles();
            Item prev = new Item();
            Tree prev_tree = new Tree();

            while (i < lines.Length)
            {
                int level = fileHandler.GetLevel(lines[i]);
                string name = fileHandler.GetPointName(lines[i]);
                string methName = fileHandler.GetMethodName(lines[i]);
                Item item = new Item(level, name, methName);
                Tree tree = new Tree(item);

                if (level == 0)
                {
                    menu.Add(tree);
                }

                else if (prev.GetLevel() < level)
                {
                    menu.Last().addChild(tree);
                }

                else if (prev.GetLevel() == level && level != 0)
                {
                    try
                    {
                        menu[FindLastRoot(level)].addChild(tree);
                    }
                    catch 
                    {
                        Console.WriteLine("No root found!");
                    }
                }

                prev = item;
                prev_tree = tree;
                i++;
            }
        }
    }
}
