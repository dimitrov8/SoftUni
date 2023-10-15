using System;

namespace _02._Vowels_Count
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Console.WriteLine(CountVowels(input));
        }

        static int CountVowels(string input)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string currLetter = input[i].ToString().ToLower();

                if (currLetter == "a" || currLetter == "e" || currLetter == "i" || currLetter == "o" ||
                    currLetter == "u")
                {
                    count++;
                }
            }

            return count;
        }
    }
}