using System;
using System.Collections.Generic;
using System.Linq;

namespace Number
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .OrderByDescending(x => x)
                .ToList();

            numbers = numbers.Where(x => x > numbers.Average()).ToList();

            if (numbers.Count == 0)
            {
                Console.WriteLine("No");
                return;
            }

            Console.WriteLine(string.Join(" ", numbers.Take(Math.Min(5, numbers.Count))));
        }
    }
}