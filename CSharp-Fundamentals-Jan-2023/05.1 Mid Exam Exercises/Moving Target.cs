using System;
using System.Collections.Generic;
using System.Linq;

namespace Moving_Target
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> targets = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] cmdArgs = command.Split(' ');
                string cmdType = cmdArgs[0];
                int index = int.Parse(cmdArgs[1]);

                if (cmdType == "Shoot" && index < targets.Count && index >= 0)
                {
                    targets[index] -= int.Parse(cmdArgs[2]);

                    if (targets[index] <= 0)
                    {
                        targets.RemoveAt(index);
                    }
                }
                else if (cmdType == "Add")
                {
                    if (index >= 0 && index < targets.Count)
                    {
                        targets.Insert(index, int.Parse(cmdArgs[2]));
                    }
                    else
                    {
                        Console.WriteLine("Invalid placement!");
                    }
                }
                else if (cmdType == "Strike" && index >= 0 && index < targets.Count)
                {
                    int radius = int.Parse(cmdArgs[2]);
                    // Remove the target at the given index and the
                    // ones before and after it depending on the radius.


                    // If any of the indices in the range is invalid,
                    // print: "Strike missed!" and skip this command.

                    int value = targets[index];
                    bool skip = false;
                    for (int i = index - radius; i < index + radius; i++)
                    {
                        if (i < 0 || i >= targets.Count)
                        {
                            Console.WriteLine("Strike missed!");
                            skip = true;
                            break;
                        }

                        targets.RemoveAt(i);
                    }

                    if (!skip)
                    {
                        targets.Remove(value);
                    }
                }
            }

            Console.WriteLine(string.Join("|", targets));
        }
    }
}