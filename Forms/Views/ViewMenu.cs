using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public interface IMenuView
    {
        void DisplayMenu(ClassLibrary.Menu menu);
        void DisplayChildren(Tree tree);
    }

    public class ButtonTags
    {
        public Tree tree { get; set; }
        public FlowLayoutPanel panel { get; set; }
        public ButtonTags() { }
        public ButtonTags(Tree tree)
        {
            this.tree = tree;
            panel = new FlowLayoutPanel();
        }
        public ButtonTags(FlowLayoutPanel panel)
        {
            this.panel = panel;
        }
    }
}
