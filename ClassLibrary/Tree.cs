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
        private Item _root;
        private List<Tree> _children;

        public Item root { get { return _root; } set { _root = value; } }
        public List<Tree> children { get { return _children; } set { _children = value; } }

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
