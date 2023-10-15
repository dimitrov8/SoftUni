using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Mixed_up_Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> firstList = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            List<int> secondList = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            List<int> concatList = new List<int>();

            int startIndex = 0;

            List<int> range = new List<int>();
            if (firstList.Count > secondList.Count)
            {
                int endIndex = secondList.Count - 1;
                while (startIndex < firstList.Count && endIndex >= 0)
                {
                    concatList.Add(firstList[startIndex]);
                    concatList.Add(secondList[endIndex]);

                    startIndex++;
                    endIndex--;
                }

                range.Add(firstList[^1]);
                range.Add(firstList[^2]);
            }
            else if (secondList.Count > firstList.Count)
            {
                int endIndex = firstList.Count - 1;
                while (startIndex < secondList.Count && endIndex >= 0)
                {
                    concatList.Add(firstList[startIndex]);
                    concatList.Add(secondList[endIndex]);

                    startIndex++;
                    endIndex--;
                }

                concatList.Add(secondList[^1]);
                concatList.Add(secondList[^2]);
                range.Add(secondList[0]);
                range.Add(secondList[1]);
            }

            range.Sort();

            List<int> result = concatList.Where(x => x > range[0] && x < range[1])
                .OrderBy(x => x)
                .ToList();

            Console.WriteLine(string.Join(" ", result));
        }
    }
}