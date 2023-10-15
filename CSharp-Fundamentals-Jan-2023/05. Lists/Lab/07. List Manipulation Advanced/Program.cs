using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._List_Manipulation_Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            string command;

            bool PrintList = false;
            while ((command = Console.ReadLine()) != "end")
            {
                string[] commandArgs = command.Split(" ");
                string commandType = commandArgs[0];
                
                
                if (commandType == "Add")
                {
                    numbers.Add(int.Parse(commandArgs[1]));
                    PrintList = true;
                }

                else if (commandType == "Remove")
                {
                    numbers.Remove(int.Parse(commandArgs[1]));
                    PrintList = true;
                }

                else if (commandType == "RemoveAt")
                {
                    numbers.RemoveAt(int.Parse(commandArgs[1]));
                    PrintList = true;
                }
                else if (commandType == "Insert")
                {
                    int inputNumber = int.Parse(commandArgs[1]);
                    int index = int.Parse(commandArgs[2]);
                    numbers.Insert(index, inputNumber);
                    PrintList = true;
                }

                else if (commandType == "Contains")
                {
                    CheckIfListContains(numbers, commandArgs);
                    Console.WriteLine(CheckIfListContains(numbers, commandArgs)
                        ? "Yes"
                        : "No such number");
                }

                else if (commandType == "PrintOdd")
                {
                    Console.WriteLine(string.Join(" ", numbers.Where(x => x % 2 != 0)));
                }

                else if (commandType == "PrintEven")
                {
                    Console.WriteLine(string.Join(" ", numbers.Where(x => x % 2 == 0)));
                }

                else if (commandType == "GetSum")
                {
                    Console.WriteLine(numbers.Sum());
                }

                else if (commandType == "Filter")
                {
                    FilterMethod(numbers, commandArgs);
                }
            }

            if (PrintList)
            {
                Console.WriteLine(string.Join(" ", numbers));
            }
        }

        static void FilterMethod(List<int> numbers, string[] commandArgs)
        {
            string condition = commandArgs[1];
            int inputNumber = int.Parse(commandArgs[2]);

            switch (condition)
            {
                case ">=":
                    Console.WriteLine(string.Join(" ", numbers.Where(x => x >= inputNumber)));
                    break;
                case "<=":
                    Console.WriteLine(string.Join(" ", numbers.Where(x => x <= inputNumber)));
                    break;
                case ">":
                    Console.WriteLine(string.Join(" ", numbers.Where(x => x > inputNumber)));
                    break;
                case "<":
                    Console.WriteLine(string.Join(" ", numbers.Where(x => x < inputNumber)));
                    break;
            }
        }

        static bool CheckIfListContains(List<int> numbers, string[] commandArgs)
        {
            bool isContaining = numbers.Contains(int.Parse(commandArgs[1]));

            return isContaining;
        }
    }
}