using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Drum_Set
{
    class Program
    {
        static void Main(string[] args)
        {
            double savings = double.Parse(Console.ReadLine());

            List<int> drumSet = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            string command;

            List<int> drumSetCopy = drumSet.ToList();

            while ((command = Console.ReadLine()) != "Hit it again, Gabsy!")
            {
                int hitPower = int.Parse(command);

                for (int i = 0; i < drumSet.Count; i++)
                {
                    // Damages every drum
                    drumSet[i] -= hitPower;

                    // If any drum is broken
                    if (drumSet[i] <= 0)
                    {
                        // Calculate the current drum price from the original set
                        int price = drumSetCopy[i] * 3;

                        // Replace it if we have money
                        if (savings >= price)
                        {
                            // Remove money from our savings
                            savings -= price;
                            // Insert new drum from the original value index
                            drumSet.Insert(i, drumSetCopy[i]);
                            // Remove the old value
                            drumSet.RemoveAt(i + 1);
                        }
                        // If we don't have money
                        else if (price > savings)
                        {
                            // Remove the drum 
                            drumSet.RemoveAt(i);
                            // Remove the drum from the original drum set 
                            drumSetCopy.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }


            Console.WriteLine(string.Join(" ", drumSet));
            Console.WriteLine($"Gabsy has {savings:f2}lv.");
        }
    }
}