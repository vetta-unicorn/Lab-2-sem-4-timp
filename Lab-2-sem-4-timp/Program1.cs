using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata;
using ClassLibrary;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "Menu.txt";
        Menu menu = new Menu(filePath);

        menu.SetMenu();
    }

}