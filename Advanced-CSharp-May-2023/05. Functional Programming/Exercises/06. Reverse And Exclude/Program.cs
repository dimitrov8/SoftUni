using System;
using System.Linq;

namespace _06._Reverse_and_Exclude
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .Reverse()
                .ToArray();
            int nDiv = int.Parse(Console.ReadLine());
            Predicate<int> isDiv = n => n % nDiv == 0;
            Console.WriteLine(string.Join(" ", numbers.Where(n => !isDiv(n))));
        }
    }
}