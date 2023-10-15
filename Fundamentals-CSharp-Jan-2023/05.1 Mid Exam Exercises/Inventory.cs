using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> items = Console.ReadLine()
                .Split(", ")
                .ToList();

            string command;
            while ((command = Console.ReadLine()) != "Craft!")
            {
                string[] cmdArgs = command.Split(" - ");
                string cmdType = cmdArgs[0];
                string item = cmdArgs[1];

                if (cmdType == "Collect" && !items.Contains(item))
                {
                    items.Add(item);
                }
                else if (cmdType == "Drop" && items.Contains(item))
                {
                    items.Remove(item);
                }
                else if (cmdType == "Combine Items")
                {
                    string[] itemParts = item.Split(":");
                    string oldItem = itemParts[0];
                    string newItem = itemParts[1];
                    if (items.Contains(oldItem))
                    {
                        items.Insert(items.IndexOf(oldItem) + 1, newItem);
                    }
                }
                else if (cmdType == "Renew" && items.Contains(item))
                {
                    items.Remove(item);
                    items.Add(item);
                }
            }

            Console.WriteLine(string.Join(", ", items));
        }
    }
}