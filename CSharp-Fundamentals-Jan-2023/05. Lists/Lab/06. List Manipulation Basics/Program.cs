using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._List_Manipulation_Basics
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

            while ((command = Console.ReadLine()) != "end")
            {

                string[] commandArgs = command.Split(" ");
                string commandType = commandArgs[0];
                int inputNumber = int.Parse(commandArgs[1]);
                
                if (commandType == "Add")
                {
                    numbers.Add(inputNumber);
                }

                else if (commandType == "Remove")
                {
                    numbers.Remove(inputNumber);
                }
                
                else if (commandType == "RemoveAt")
                {
                    numbers.RemoveAt(inputNumber);
                }
                else if (commandType == "Insert")
                {
                    int index = int.Parse(commandArgs[2]);
                    numbers.Insert(index, inputNumber);
                }
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}