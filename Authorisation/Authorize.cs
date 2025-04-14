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
                    AddUser(new User(name, password));
                }
                else
                {
                    (string, int) EntryAndStatus = fileHandler.GetEntryAndStatus(line);
                    users.Last().AddEntry(new Entry(EntryAndStatus.Item1, EntryAndStatus.Item2));
                }
            }
        }

        public bool CheckAccount(string username, string password)
        {
            bool Flag = false;
            foreach(User user in users)
            {
                if (user.CheckAccount(username, password))
                {
                    Flag = true;
                    break;
                }
            }

            return Flag;
        }
    }
}
