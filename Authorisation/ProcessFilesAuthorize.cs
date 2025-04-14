using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Authorization
{
    public class ProcessFileAutho
    {
        private string filePath {  get; set; }
        public ProcessFileAutho(string filePath)
        {
            this.filePath = filePath;
        }

        public string[] GetFiles()
        {
            string[] lines = { "" };
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            return lines;
        }

        public string[] SplitStrings(string line)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return parts;
        }

        public string GetUsername(string line)
        {
            string[] parts = SplitStrings(line);
            char[] chars = parts[0].ToCharArray();
            char[] newChars = new char[chars.Length - 1];
            for (int i = 0; i < newChars.Length; i++)
            {
                newChars[i] = chars[i + 1];
            }
            string name = new string(newChars);
            return name;
        }

        public string GetPassword(string line)
        {
            string[] parts = SplitStrings(line);
            string password = parts[1];
            return password;
        }

        //public string GetEntryName(string line)
        //{
        //    string[] parts = SplitStrings(line);
        //    string entry = parts[0];
        //    return entry;
        //}

        public (string, int) GetEntryAndStatus(string line)
        {
            string[] parts = SplitStrings(line);
            string entry = "";
            int status = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                char[] chars = parts[i].ToCharArray();

                if (!Char.IsNumber(chars[0]))
                {
                    entry += parts[i];
                    if (i < parts.Length - 1)
                    {
                        char[] nextChars = parts[i + 1].ToCharArray();
                        if (!Char.IsNumber(nextChars[0]))
                        {
                            entry += " ";
                        }
                    }
                }
                else
                {
                    status = Convert.ToInt32(parts[i]);
                }
            }

            return (entry, status);
        }

        //public int GetAccessStatus(string line)
        //{
        //    string[] parts = SplitStrings(line);
        //    int status = 0;

        //    if (parts.Length > 1)
        //    {
        //        status = Convert.ToInt32(parts[1]);
        //    }

        //    return status;
        //}
    }
}
