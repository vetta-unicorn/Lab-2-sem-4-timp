using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class Entry
    {
        public string name { get; private set; }
        public int status { get; private set; }
        public Entry(string name, int status)
        {
            this.name = name;
            this.status = status;
        }
    }

    [Serializable]
    public class User
    {
        public string name { get; set; }
        public string password { get; set; }
        public List<Entry> entries { get; private set; }

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
