using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Menu
    {
        public List<Tree> menu { get; private set; }
        public ProcessFile fileHandler { get; private set; }

        public Menu (string filePath)
        {
            menu = new List<Tree>();
            fileHandler = new ProcessFile(filePath);
            SetMenu();
        }

        public void SetMenu()
        {
            string[] lines = fileHandler.GetFiles();

            for (int i = 0; i < lines.Length; i++)
            {
                int level = fileHandler.GetLevel(lines[i]);
                string name = fileHandler.GetPointName(lines[i]);
                string methName = fileHandler.GetMethodName(lines[i]);
                Tree tree = new Tree(new Item(level, name, methName));

                if (level == 0)
                {
                    menu.Add(tree);

                    int nextLevel = 0;
                    if (lines[i + 1] != null ) nextLevel = fileHandler.GetLevel(lines[i + 1]);
                    if (nextLevel == 1)
                    {
                        SetSubMenu(tree, i);
                    }
                }
            }
        }

        public void SetSubMenu(Tree root, int position)
        {
            string[] lines = fileHandler.GetFiles();

            for (int i = position + 1; i < lines.Length; i++)
            {
                int level = fileHandler.GetLevel(lines[i]);
                string name = fileHandler.GetPointName(lines[i]);
                string methName = fileHandler.GetMethodName(lines[i]);
                Tree tree = new Tree(new Item(level, name, methName));

                if (tree.root.level == root.root.level + 1)
                {
                    root.children.Add(tree);
                }

                else if (tree.root.level == root.root.level + 2)
                {
                    SetSubMenu(root.children.Last(), i - 1);
                    continue;
                }

                else break;
            }
        }
    }
}
