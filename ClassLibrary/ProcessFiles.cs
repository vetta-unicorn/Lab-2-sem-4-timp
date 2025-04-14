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
        private string filePath {  get; set; }

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
            string name = parts[1];
            return name;
        }

        public string GetMethodName(string line)
        {
            string[] parts = SplitStrings(line);
            string methName = "";

            if (parts.Length > 3)
            {
                methName = parts[3];
            }
            return methName;
        }
    }
}
