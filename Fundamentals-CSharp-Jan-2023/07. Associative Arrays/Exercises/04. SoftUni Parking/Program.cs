using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace _04._SoftUni_Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfCommands = int.Parse(Console.ReadLine());
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = 1; i <= numberOfCommands; i++)
            {
                string[] tokens = Console.ReadLine().Split(" ");
                string command = tokens[0];
                string username = tokens[1];

                bool userIsRegistered = dict.ContainsKey(username);
                if (command == "register")
                {
                    string licensePlateNumber = tokens[2];
                    if (userIsRegistered)
                    {
                        Console.WriteLine(
                            $"ERROR: already registered with plate number {dict[username]}");
                        continue;
                    }

                    dict.Add(username, licensePlateNumber);
                    Console.WriteLine($"{username} registered {licensePlateNumber} successfully");
                }
                else if (command == "unregister")
                {
                    if (!dict.ContainsKey(username))
                    {
                        Console.WriteLine($"ERROR: user {username} not found");
                        continue;
                    }

                    Console.WriteLine($"{username} unregistered successfully");
                    dict.Remove(username);
                }
            }

            foreach (var registration in dict)
            {
                Console.WriteLine($"{registration.Key} => {registration.Value}");
            }
        }
    }
}