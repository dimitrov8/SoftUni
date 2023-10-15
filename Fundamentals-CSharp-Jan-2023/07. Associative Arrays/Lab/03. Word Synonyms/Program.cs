using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace _03._Word_Synonyms
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Dictionary<string, List<string>> synonyms = new Dictionary<string, List<string>>();
            for (int i = 1; i <= n ; i++)
            {
                string key = Console.ReadLine();
                string value = Console.ReadLine();

                if (!synonyms.ContainsKey(key))
                {
                    synonyms.Add(key, new List<string>());
                }
                synonyms[key].Add(value);
            }

            foreach (var word in synonyms)
            {
                Console.WriteLine($"{word.Key} - {string.Join(", ", word.Value)}");
            }
        }
    }
}