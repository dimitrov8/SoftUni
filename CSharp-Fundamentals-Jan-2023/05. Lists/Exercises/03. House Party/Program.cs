using System;
using System.Collections.Generic;

namespace _03._House_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            int nCommands = int.Parse(Console.ReadLine());

            List<string> guests = new List<string>();
            for (int i = 1; i <= nCommands; i++)
            {
                string[] command = Console.ReadLine().Split(" ");
                string name = command[0];

                // Is going
                if (command[2] == "going!")
                {
                    if (!guests.Contains(name))
                    {
                        guests.Add(name);
                    }
                    else
                    {
                        Console.WriteLine($"{name} is already in the list!");
                    }
                }
                
                // Not going
               else if (command[2] == "not")
                {
                    if (guests.Contains(name))
                    {
                        guests.Remove(name);
                    }
                    else
                    {
                        Console.WriteLine($"{name} is not in the list!");
                    }
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, guests));
        }
    }
}