using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace _01._Count_Real_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] numbers = Console.ReadLine()
                .Split()
                .Select(double.Parse)
                .ToArray();

            SortedDictionary<double, int> count = new SortedDictionary<double, int>();

            foreach (double number in numbers)
            {
                if (count.ContainsKey(number))
                {
                    count[number]++;
                }
                else
                {
                    count.Add(number, 1);
                }
            }

            foreach (var number in count)
            {
                Console.WriteLine($"{number.Key} -> {number.Value}");
            }
        }
    }
}