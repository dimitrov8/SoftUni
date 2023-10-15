using System;
using System.Linq;

namespace Mu_Online
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] dungeonRooms = Console.ReadLine()
                .Split("|")
                .ToArray();

            int health = 100;
            int bitcoins = 0;

            for (int i = 0; i < dungeonRooms.Length; i++)
            {
                string[] cmdArgs = dungeonRooms[i].Split(' ');
                if (cmdArgs[0] == "potion" || cmdArgs[0] == "chest")
                {
                    if (cmdArgs[0] == "potion")
                    {
                        int healing = int.Parse(cmdArgs[1]);
                        if (healing + health > 100)
                        {
                            healing = 100 - health;
                            health = 100;
                        }
                        else if (healing + health <= 100)
                        {
                            health += healing;
                        }

                        Console.WriteLine($"You healed for {healing} hp.");
                        Console.WriteLine($"Current health: {health} hp.");
                    }
                    else
                    {
                        bitcoins += int.Parse(cmdArgs[1]);
                        Console.WriteLine($"You found {cmdArgs[1]} bitcoins.");
                    }
                }
                else
                {
                    health -= int.Parse(cmdArgs[1]);

                    if (health <= 0)
                    {
                        Console.WriteLine($"You died! Killed by {cmdArgs[0]}.");
                        Console.WriteLine($"Best room: {i + 1}");
                        return;
                    }

                    Console.WriteLine($"You slayed {cmdArgs[0]}.");
                }
            }

            Console.WriteLine("You've made it!");
            Console.WriteLine($"Bitcoins: {bitcoins}");
            Console.WriteLine($"Health: {health}");
        }
    }
}