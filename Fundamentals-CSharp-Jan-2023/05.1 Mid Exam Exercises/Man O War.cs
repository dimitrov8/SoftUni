using System;
using System.Collections.Generic;
using System.Linq;

namespace Man_O_War
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> pirateShip = Console.ReadLine()
                .Split(">")
                .Select(int.Parse)
                .ToList();

            List<int> warShip = Console.ReadLine()
                .Split(">")
                .Select(int.Parse)
                .ToList();

            int maxHealth = int.Parse(Console.ReadLine());

            string command;
            while ((command = Console.ReadLine()) != "Retire")
            {
                if (command == "Status")
                {
                    int count = 0;
                    foreach (int section in pirateShip)
                    {
                        if (section < maxHealth * 0.2)
                        {
                            count++;
                        }
                    }

                    Console.WriteLine($"{count} sections need repair.");
                    continue;
                }

                string[] cmdArgs = command.Split(" ");
                string cmdType = cmdArgs[0];
                int index = int.Parse(cmdArgs[1]);

                if (cmdType == "Fire" && index >= 0 && index < warShip.Count)
                {
                    int damage = int.Parse(cmdArgs[2]);

                    warShip[index] -= damage;

                    if (warShip[index] <= 0)
                    {
                        Console.WriteLine("You won! The enemy ship has sunken.");
                        return;
                    }
                }
                else if (cmdType == "Defend" && index >= 0 && int.Parse(cmdArgs[2]) < pirateShip.Count)
                {
                    int endIndex = int.Parse(cmdArgs[2]);
                    int damage = int.Parse(cmdArgs[3]);

                    for (int i = index; i <= endIndex; i++)
                    {
                        pirateShip[i] -= damage;

                        if (pirateShip[i] <= 0)
                        {
                            Console.WriteLine("You lost! The pirate ship has sunken.");
                            return;
                        }
                    }
                }
                else if (cmdType == "Repair" && index >= 0 && index < pirateShip.Count)
                {
                    pirateShip[index] += int.Parse(cmdArgs[2]);

                    if (pirateShip[index] > maxHealth)
                    {
                        pirateShip[index] = maxHealth;
                    }
                }
            }

            Console.WriteLine($"Pirate ship status: {pirateShip.Sum()}");
            Console.WriteLine($"Warship status: {warShip.Sum()}");
        }
    }
}