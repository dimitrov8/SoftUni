using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Count_Same_Values_in_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] input = Console.ReadLine().Split().Select(double.Parse).ToArray();
            var dict = new Dictionary<double, int>();

            foreach (var num in input)
            {
                if (!dict.ContainsKey(num))
                {
                    dict.Add(num, 0);
                }
                dict[num]++;
            }

            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value} times");   
            }
        }
    }
}