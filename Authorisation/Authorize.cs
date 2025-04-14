using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class Authorize
    {
        private ProcessFileAutho fileHandler { get; set; }
        private List<User> users { get; set; }

        public Authorize(string filePath)
        {
            fileHandler = new ProcessFileAutho(filePath);
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void SetUserList()
        {
            string[] lines = fileHandler.GetFiles();
            foreach (string line in lines)
            {
                char[] chars = line.ToCharArray();
                if (Equals(chars[0], '#'))
                {
                    string name = fileHandler.GetUsername(line);
                    string password = fileHandler.GetPassword(line);
                    User user = new User(name, password);
                    AddUser(user);
                }
                else
                {
                    //string entry_name = fileHandler.GetEntryName(line);
                    //int status = fileHandler.GetAccessStatus(line);
                    (string, int) tuple = fileHandler.GetEntryAndStatus(line);
                    string entry = tuple.Item1;
                    int status = tuple.Item2;
                    users.Last().AddEntry(new Entry(entry, status));
                }
            }
        }
    }
}
