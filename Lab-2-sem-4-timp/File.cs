using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab_2_sem_4_timp
{
    public class ProcessFile
    {
        public ProcessFile() { }

        public string[] GetFiles(string filePath)
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
            // Разделяем строку по пробелам
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
            if (parts[2] != null)
            {
                methName = parts[2];
            }
            return methName;
        }
    }
}
