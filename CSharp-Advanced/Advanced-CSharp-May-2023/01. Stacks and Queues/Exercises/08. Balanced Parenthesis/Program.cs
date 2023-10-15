using System;
using System.Collections.Generic;

namespace _08._Balanced_Parenthesis
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); // Read the parentheses as a string
            Stack<char> parentheses = new Stack<char>(); // Create an empty stack to store the parentheses

            bool isBalanced = true; // Default value of isBalanced
            foreach (var bracket in input) // For each bracket in input
            {
                if (bracket == '{' || bracket == '[' || bracket == '(') // If it's an opening bracket
                {
                    parentheses.Push(bracket); // Push the bracket into the stack
                }
                if (parentheses.Count == 0 // If the current bracket is closed and the bracket in the stack is opened
                         || bracket == '}' && parentheses.Pop() != '{'
                         || bracket == ']' && parentheses.Pop() != '['
                         || bracket == ')' && parentheses.Pop() != '(')

                {
                    isBalanced = false; // It's not balanced because it has to be always opening bracket and then closing bracket
                    break; // Break
                }
            }

            Console.WriteLine(isBalanced ? "YES" : "NO"); // Print the result
        }
    }
}