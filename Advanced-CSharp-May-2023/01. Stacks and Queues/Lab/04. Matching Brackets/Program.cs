using System;
using System.Collections.Generic;

namespace _04._Matching_Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); // Read the input
            Stack<int> indices = new Stack<int>(); // Stack to store the indices`

            for (int i = 0; i < input.Length; i++) // Loop through the input
            {
                if (input[i] == '(') // If the current char is '('
                {
                    indices.Push(i); // Push the index to the stack
                }
                else if (input[i] == ')') // If the current char is ')
                {
                    int start = indices.Pop(); // Create a variable which to takes the index of the opening bracket
                    string sub = input.Substring(start, i - start + 1); // Substring starting from the opening bracket to the current index - start + 1
                    Console.WriteLine(sub); // Print the substring
                }
            }
        }
    }
}