using System;
using System.Collections.Generic;
using System.Linq;

namespace Treasure_Hunt
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> loot = Console.ReadLine()
                .Split("|")
                .ToList();

            string command;

            while ((command = Console.ReadLine()) != "Yohoho!")
            {
                string[] cmdArgs = command.Split(" ");
                string cmdType = cmdArgs[0];

                if (cmdType == "Loot")
                {
                    AddLoot(cmdArgs, loot);
                }
                else if (cmdType == "Drop")
                {
                    Drop(cmdArgs, loot);
                }
                else if (cmdType == "Steal")
                {
                    Steal(cmdArgs, loot);
                }
            }

            double avg = CalculateAvgLoot(loot);

            if (loot.Count == 0)
            {
                Console.WriteLine("Failed treasure hunt.");
                return;
            }

            Console.WriteLine($"Average treasure gain: {avg:f2} pirate credits.");
        }

        static void Drop(string[] cmdArgs, List<string> loot)
        {
            int index = int.Parse(cmdArgs[1]);
            if (index > loot.Count - 1 || index < 0)
            {
                return;
            }

            string temp = loot[index];
            loot.RemoveAt(index);
            loot.Add(temp);
        }

        static void AddLoot(string[] cmdArgs, List<string> loot)
        {
            for (int i = 1; i < cmdArgs.Length; i++)
            {
                if (loot.Contains(cmdArgs[i]))
                {
                    continue;
                }

                loot.Insert(0, cmdArgs[i]);
            }
        }

        static double CalculateAvgLoot(List<string> loot)
        {
            double avgLootGain = 0;
            foreach (string word in loot)
            {
                avgLootGain += word.Length;
            }

            return avgLootGain / loot.Count;
        }

        static void Steal(string[] cmdArgs, List<string> loot)
        {
            int count = int.Parse(cmdArgs[1]);
            int removeCount = Math.Min(count, loot.Count);

            List<string> removed = new List<string>();
            for (int i = 0; i < removeCount; i++)
            {
                int index = loot.Count - 1;
                removed.Add(loot[index]);
                loot.RemoveAt(index);
            }

            removed.Reverse();
            Console.WriteLine(string.Join(", ", removed));
            removed.Clear();
        }
    }
}