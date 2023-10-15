using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Append_Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            // Split the arrays by "|"
            List<string> arr = Console.ReadLine()
                .Split("|", StringSplitOptions.RemoveEmptyEntries)
                .Reverse()
                .ToList();

            // Foreach number[string] in the array
            foreach (string numString in arr)
            {
                // Add it to a new list and separate the numbers by " ";
                List<string> numbers = numString
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                // Foreach number in the numbers array
                foreach (string num in numbers)
                {
                    // Print the number
                    Console.Write(num + " ");
                }
            }
        }
    }
}