using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Stack_Sum_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>(Console.ReadLine()
                .Split(' ')
                .Select(int.Parse));

            string command;
            while ((command = Console.ReadLine().ToLower()) != "end")
            {
                string[] tokens = command.Split();
                string mainCommand = tokens[0];
                if (mainCommand == "add")
                {
                    stack.Push(int.Parse(tokens[1]));
                    stack.Push(int.Parse(tokens[2]));
                }
                else if (mainCommand == "remove")
                {
                    int count = int.Parse(tokens[1]);
                    if (stack.Count > count)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            stack.Pop();
                        }
                    }
                }
            }

            Console.WriteLine($"Sum: {stack.Sum()}");
        }
    }
}