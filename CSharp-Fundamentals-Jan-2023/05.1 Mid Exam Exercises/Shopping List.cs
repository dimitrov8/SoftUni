using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping_List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> items = Console.ReadLine()
                .Split("!")
                .ToList();

            string command;
            while ((command = Console.ReadLine()) != "Go Shopping!")
            {
                string[] cmdArgs = command.Split(" ").ToArray();
                string cmdType = cmdArgs[0];
                string item = cmdArgs[1];

                if (cmdType == "Urgent" && !items.Contains(item))
                {
                    items.Insert(0, item);
                }

                else if (cmdType == "Unnecessary" && items.Contains(item))
                {
                    items.Remove(item);
                }

                else if (cmdType == "Rearrange" && items.Contains(item))
                {
                    items.Remove(item);
                    items.Add(item);
                }
                else if (cmdType == "Correct" && items.Contains(cmdArgs[1]))
                {
                    string oldItem = cmdArgs[1];
                    string newItem = cmdArgs[2];

                    // Get the index of the old item in the list
                    int index = items.IndexOf(oldItem);

                    // Replace the old item with the new item
                    items[index] = newItem;
                }
            }

            Console.WriteLine(string.Join(", ", items));
        }
    }
}