using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._List_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] commandArgs = command.Split(" ");
                string cmdType = commandArgs[0];

                if (cmdType == "Add" || cmdType == "Insert" || cmdType == "Remove")
                {
                    if (cmdType == "Add")
                    {
                        int number = int.Parse(commandArgs[1]);
                        numbers.Add(number);
                    }

                    else if (cmdType == "Insert")
                    {
                        int index = int.Parse(commandArgs[2]);
                        if (index > numbers.Count || index < 0)
                        {
                            Console.WriteLine("Invalid index");
                            continue;
                        }

                        int number = int.Parse(commandArgs[1]);
                        numbers.Insert(index, number);
                    }

                    else if (cmdType == "Remove")
                    {
                        int index = int.Parse(commandArgs[1]);
                        if (index > numbers.Count  || index < 0)
                        {
                            Console.WriteLine("Invalid index");
                            continue;
                        }

                        numbers.RemoveAt(index);
                    }
                }
                else if (cmdType == "Shift")
                {
                    string whereToShift = commandArgs[1];

                    int count = int.Parse(commandArgs[2]);

                    if (whereToShift == "left")
                    {
                        while (count != 0)
                        {
                            // TO do:
                            // 3 5 12 42 95 32 8 1
                            //5 12 42 95 32 8 1 3
                            int currN = numbers[0];
                            numbers.RemoveAt(0);
                            numbers.Add(currN);
                            count--;
                        }
                    }
                    else if (whereToShift == "right")
                    {
                        while (count != 0)
                        {
                            // 3 5 12 42 95 32 8 1
                            // 1 3 5 12 42 95 32 8
                            int currN = numbers[numbers.Count - 1];
                            numbers.RemoveAt(numbers.Count - 1);
                            numbers.Insert(0, currN);
                            count--;
                        }
                    }
                }
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}