using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Tribonacci_Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<int> numbers = new List<int> { 1, 1, 2 };
            PrintNumbers(n, numbers);
        }

        static void PrintNumbers(int n, List<int> numbers)
        {
            for (int i = 3; i < n; i++)
            {
                numbers.Add(numbers.Skip(i - 3).Take(3).Sum());
            }

            // We use "Take" method to ensure we don't print more numbers than the user requested.
            Console.WriteLine(string.Join(" ", numbers.Take(n)));
        }
    }
}