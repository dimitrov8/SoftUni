using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Train
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> wagons = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            int maxCapacityWagon = int.Parse(Console.ReadLine());

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                string[] commandArgs = command.Split(" ");

                if (commandArgs.Length == 2)
                {
                    wagons.Add(int.Parse(commandArgs[1]));
                }
                
                else if (commandArgs.Length == 1)
                {
                    int passengers = int.Parse(commandArgs[0]);

                    for (int i = 0; i < wagons.Count; i++)
                    {
                        if (passengers + wagons[i] <= maxCapacityWagon)
                        {
                            wagons.Insert(i, passengers + wagons[i]);
                            wagons.RemoveAt(i + 1);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(string.Join(" ", wagons));
        }
    }
}