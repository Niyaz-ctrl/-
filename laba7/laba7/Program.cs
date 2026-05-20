using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab7_Variant5_Simple
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Введите текст:");
            string text = Console.ReadLine();

            string[] words = text.Split(' ');
            string result = "";

            for (int i = 0; i < words.Length; i += 2)
            {
                if (i + 1 < words.Length)
                    result += words[i + 1] + " " + words[i] + " ";
                else
                    result += words[i];
            }

            Console.WriteLine("\nРезультат:");
            Console.WriteLine(result.Trim());

            Console.ReadKey();
        }
    }
}