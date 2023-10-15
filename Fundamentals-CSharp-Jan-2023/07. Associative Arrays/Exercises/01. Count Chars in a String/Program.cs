using System;
using System.Collections.Generic;

namespace _01._Count_Chars_in_a_String
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (char letter in input)
            {
                string currLetter = letter.ToString();
                if (currLetter == " ")
                    continue;

                if (dict.ContainsKey(currLetter))
                {
                    dict[currLetter]++;
                    continue;
                }

                dict.Add(currLetter, 1);
            }

            foreach (var character in dict)
            {
                Console.WriteLine($"{character.Key} -> {character.Value}");
            }
        }
    }
}