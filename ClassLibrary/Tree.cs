using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Tree
    {
        public Item root { get; private set; }
        public List<Tree> children { get; private set; }

        public Tree (Item root)
        {
            this.root = root;
            root = new Item ();
            children = new List<Tree> ();
        }

        public Tree() { }

        public void addChild(Tree child)
        {
            children.Add(child);
        }
    }
}
