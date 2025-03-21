using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Form2: Form
    {
        Form1 a;
        public Form2(Form1 back)
        {
            a = back;
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.Show();
            this.Hide();
        }
    }
}
