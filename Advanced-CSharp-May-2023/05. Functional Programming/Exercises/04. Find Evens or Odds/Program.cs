using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Find_Evens_or_Odds
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] bounds = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            string condition = Console.ReadLine();

            Predicate<int> isEvenOrOdd = n => (condition == "even" && n % 2 == 0) ||
                                              (condition == "odd" && n % 2 != 0);
            List<int> numbers = new List<int>();
            for (int i = bounds[0]; i <= bounds[1]; i++)
            {
                if (isEvenOrOdd(i))
                {
                    numbers.Add(i);
                }
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}