using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Merging_Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> firstList = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            List<int> secondList = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            int n = Math.Min(firstList.Count, secondList.Count);

            List<int> outputList = new List<int>();

            for (int i = 0; i < n; i++)
            {
                outputList.Add(firstList[i]);
                outputList.Add(secondList[i]);
            }

            if (firstList.Count > secondList.Count)
            {
                for (int i = n; i < firstList.Count; i++)
                {
                    outputList.Add(firstList[i]);
                }
            }
            else if (secondList.Count > firstList.Count)
            {
                for (int i = n; i < secondList.Count; i++)
                {
                    outputList.Add(secondList[i]);
                }
            }

            Console.WriteLine(string.Join(" ", outputList));
        }
    }
}