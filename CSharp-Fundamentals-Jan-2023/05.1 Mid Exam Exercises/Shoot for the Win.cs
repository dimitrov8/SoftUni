using System;
using System.Collections.Generic;
using System.Linq;

namespace Shoot_for_the_Win
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nList = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            int shotTargets = 0;
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                int index = int.Parse(command);

                if (index > nList.Count - 1) // If index input is out of bounds
                {
                    continue; // Read new index
                }

                int currTarget = nList[index]; // Store current index value in new variable
                nList[index] = -1; // Give arr[index] value of -1
                shotTargets++;

                for (int i = 0; i < nList.Count; i++)
                {
                    if (nList[i] == -1)
                    {
                        continue;
                    }

                    if (nList[i] > currTarget)
                    {
                        nList[i] -= currTarget;
                    }
                    else if (nList[i] <= currTarget)
                    {
                        nList[i] += currTarget;
                    }
                }
            }

            Console.WriteLine($"Shot targets: {shotTargets} -> {string.Join(" ", nList)}");
        }
    }
}