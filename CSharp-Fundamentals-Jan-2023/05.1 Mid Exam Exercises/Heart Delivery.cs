using System;
using System.Linq;

namespace Heart_Delivery
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] houses = Console.ReadLine()
                .Split("@")
                .Select(int.Parse)
                .ToArray();

            string command;
            int currHouse = 0;
            while ((command = Console.ReadLine()) != "Love!")
            {
                string[] cmdArgs = command.Split(' ');
                int jump = int.Parse(cmdArgs[1]);
                currHouse += jump;

                if (currHouse > houses.Length - 1)
                {
                    currHouse = 0;
                }

                if (houses[currHouse] == 0)
                {
                    Console.WriteLine($"Place {currHouse} already had Valentine's day.");
                }
                else
                {
                    houses[currHouse] -= 2;
                    if (houses[currHouse] == 0)
                    {
                        Console.WriteLine($"Place {currHouse} has Valentine's day.");
                    }
                }
            }

            Console.WriteLine($"Cupid's last position was {currHouse}.");

            int count = 0;
            foreach (int house in houses)
            {
                if (house > 0)
                {
                    count += 1;
                }
            }

            Console.WriteLine(count == 0 ? "Mission was successful." : $"Cupid has failed {count} places.");
        }
    }
}