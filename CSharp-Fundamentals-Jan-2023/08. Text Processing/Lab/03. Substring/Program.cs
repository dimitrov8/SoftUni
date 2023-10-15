using System;

namespace _3._Substring
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = Console.ReadLine();
            string input = Console.ReadLine();
            while (input.Contains(key))
            {
                int index = input.IndexOf(key);
                input = input.Remove(index, key.Length);
            }

            Console.WriteLine(input);
        }
    }
}