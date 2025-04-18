using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class Entry
    {
        private string name { get; set; }
        private int status { get; set; }
        public Entry(string name, int status)
        {
            this.name = name;
            this.status = status;
        }
    }

    public class User
    {
        private string name { get; set; }
        private string password { get; set; }
        private List<Entry> entries { get; set; }

        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
            entries = new List<Entry>();
        }
        public User()
        {
            entries = new List<Entry>();
        }

        public List<Entry> GetEntries() { return entries; }
        public string GetName() { return name; }

        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        public bool CheckAccount(string username, string password)
        {
            if (username == name && password == this.password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
