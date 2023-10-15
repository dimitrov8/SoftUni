using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Bomb_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Number list
            List<int> nSequence = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            int[] specialNumAndPower = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            // Special number
            // Power number
            int specialN = specialNumAndPower[0];
            int power = specialNumAndPower[1];

            // Declare variables

            while (nSequence.Contains(specialN))
            {
                int index = nSequence.IndexOf(specialN);

                int leftIndex = power;
                int rightIndex = power;

                if (index - leftIndex < 0)
                {
                    leftIndex = index;
                }

                if (index + rightIndex >= nSequence.Count)
                {
                    // 0 1 2 3 4 5 6
                    // 1 1 1 1 5 1 1
                    // power 3
                    rightIndex = nSequence.Count - 1 - index;
                }

                nSequence.RemoveRange(index - leftIndex, leftIndex + rightIndex + 1);
            }

            Console.WriteLine(nSequence.Sum());
        }
    }
}