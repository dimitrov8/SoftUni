using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Basic_Stack_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] tokens = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            Stack<int> stack = new Stack<int>(Console.ReadLine()
                .Split(' ')
                .Select(int.Parse));

            for (int i = 0; i < tokens[1]; i++)
            {
                stack.Pop();
            }

            if (stack.Any(x => x == tokens[2]))
            {
                Console.WriteLine("true");
            }
            else if (stack.Count == 0)
                Console.WriteLine("0");
            else
            {
                Console.WriteLine(stack.Min());
            }
        }
    }
}