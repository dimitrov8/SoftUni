using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Count_Symbols
{
    class Program
    {
        static void Main(string[] args)
        {
            var charCount = new Dictionary<char, int>();
            string input = Console.ReadLine();
            foreach (var @char in input)
            {
                if (!charCount.ContainsKey(@char))
                {
                    charCount.Add(@char, 0);
                }

                charCount[@char]++;
            }

            foreach (var kvp in charCount.OrderBy(c => c.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} time/s");
            }
        }
    }
}