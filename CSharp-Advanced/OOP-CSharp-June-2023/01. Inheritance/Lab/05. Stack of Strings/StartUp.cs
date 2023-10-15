using System;
using System.Collections.Generic;

namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Stack<string> values = new Stack<string>();
            values.Push("one");
            values.Push("two");
            values.Push("three");
            values.Push("four");
            
            StackOfStrings stack = new StackOfStrings();
            stack.AddRange(values);

            while (!stack.IsEmpty())
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}