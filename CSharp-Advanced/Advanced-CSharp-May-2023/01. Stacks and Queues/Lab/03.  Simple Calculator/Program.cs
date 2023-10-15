using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Simple_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> stack = new Stack<string>(Console.ReadLine()
                .Split(' ')
                .Reverse());

            while (stack.Count > 1)
            {
                int firstN = int.Parse(stack.Pop());
                string symbol = stack.Pop();
                int secondN = int.Parse(stack.Pop());
                int result = symbol switch
                {
                    "+" => firstN + secondN,
                    "-" => firstN - secondN,
                    _ => 0
                };
                stack.Push(result.ToString());
            }

            Console.WriteLine(stack.Pop());
        }
    }
}