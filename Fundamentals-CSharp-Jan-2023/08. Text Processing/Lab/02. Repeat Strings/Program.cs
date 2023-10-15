using System;
using System.Linq;

namespace _02._Repeat_Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ').ToArray();
            foreach (var word in input)
            {
                int times = word.Length;
                for (int i = 1; i <= times; i++)
                {
                    Console.Write(word);
                }
            }
        }
    }
}