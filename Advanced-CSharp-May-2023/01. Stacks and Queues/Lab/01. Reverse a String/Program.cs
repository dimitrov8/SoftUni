using System;
using System.Collections.Generic;

namespace _01._Reverse_a_String
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<char> stack = new Stack<char>(Console.ReadLine());
            while (stack.Count > 0)
            {
                Console.Write(stack.Pop());
            }
        }
    }
}