using System;

namespace _02._Knights_of_Honor
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string[]> print = n =>
            {
                foreach (string name in n)
                    Console.WriteLine($"Sir {name}");
            };
            print(Console.ReadLine().Split(' '));
        }
    }
}