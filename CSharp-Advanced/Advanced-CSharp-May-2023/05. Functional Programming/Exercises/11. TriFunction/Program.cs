using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._TriFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> names = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            Func<string, int, bool> isEqualOrLargerNameSum = (name, length) => name.Sum(c => c) >= length;
            Func<List<string>, int, Func<string, int, bool>, string> getFirstName = (names, length, func) => names.First(n => func(n, length));

            Console.WriteLine(getFirstName(names, n, isEqualOrLargerNameSum));
        }
    }
}