using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._List_Of_Predicates
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Enumerable.Range(1, int.Parse(Console.ReadLine())).ToList();
            int[] divNumbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .Distinct()
                .ToArray();

            foreach (var div in divNumbers)
            {
                Predicate<int> areDivisible = numbers => numbers % div == 0;
                numbers = numbers.FindAll(areDivisible);
            }           
            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}