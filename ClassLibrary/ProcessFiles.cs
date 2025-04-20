using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    // библиотека работы с файлами
    public class ProcessFile
    {
        private string filePath;

        public ProcessFile(string filePath)
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

        public int GetLevel(string line)
        {
            string[] parts = SplitStrings(line);
            int number = int.Parse(parts[0]);
            return number;
        }

        public string GetPointName(string line)
        {
            string[] parts = SplitStrings(line);
            string entry = "";
            for (int i = 0; i < parts.Length; i++)
            {
                char[] chars = parts[i].ToCharArray();

                if (Letters.IsCyrillic(chars[0]))
                {
                    entry += parts[i];
                    if (i < parts.Length - 1)
                    {
                        char[] nextChars = parts[i + 1].ToCharArray();
                        if (Letters.IsCyrillic(nextChars[0]))
                        {
                            entry += " ";
                        }
                    }
                }
            }

            return entry;
        }

        public string GetMethodName(string line)
        {
            string[] parts = SplitStrings(line);
            string methName = "";

            for (int i = 0; i < parts.Length; i++)
            {
                char[] chars = parts[i].ToCharArray();

                if (Letters.IsLatin(chars[0]))
                {
                    methName = parts[i];
                }
            }

            return methName;
        }
    }

    public static class Letters
    {
        public static bool IsLatin(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }

        public static bool IsCyrillic(char c)
        {
            return (c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я');
        }
    }
}
