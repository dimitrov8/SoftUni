using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Maximum_and_Minimum_Element
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfCommands = int.Parse(Console.ReadLine());
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < nOfCommands; i++)
            {
                int[] tokens = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();
                int commandNumber = tokens[0];
                if (commandNumber == 1)
                {
                    int numberToAdd = tokens[1];
                    stack.Push(numberToAdd);
                }
                else if (commandNumber == 2 && stack.Count > 0)
                {
                    stack.Pop();
                }
                else if (commandNumber == 3 && stack.Count > 0)
                {
                    Console.WriteLine(stack.Max());
                }
                else if (commandNumber == 4 & stack.Count > 0)
                {
                    Console.WriteLine(stack.Min());
                }
            }

            while (stack.Count > 0)
            {
                Console.Write(stack.Pop());
                if (stack.Count > 0)
                {
                    Console.Write(", ");
                }
            }
        }
    }
}