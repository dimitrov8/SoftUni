using System;
using System.Linq;

namespace _01._Randomize_Words
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine()
                .Split(" ")
                .ToArray();

            var rnd = new Random();
            for (int i = 0; i < input.Length; i++)
            {
                int randomIndex = rnd.Next(0, input.Length);
                string currWord = input[i];
                string nextWord = input[randomIndex];

                input[randomIndex] = currWord;
                input[i] = nextWord;
            }

            foreach (string word in input)
            {
                Console.WriteLine(word);
            }
        }
    }
}