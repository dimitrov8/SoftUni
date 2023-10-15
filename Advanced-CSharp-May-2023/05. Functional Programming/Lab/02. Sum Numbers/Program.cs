using System;
using System.Linq;

namespace _02._Sum_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Join(Environment.NewLine, // // Write the result of the program to the console as a string joined by a newline
                Console.ReadLine() // Read the input from the console
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries) // Split the input into an array of strings
                    .Select(int.Parse) // Convert each string in the array to an integer
                    .Aggregate((count: 0, sum: 0), (acc, n) => (acc.count + 1, acc.sum + n)) // Calculate the count of the integers and the sum of the integers using Aggregate
                    .ToString() // Convert the results to a string so we can split it 
                    .Split(new char[] { '(', ')', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))); // Split the results 
        }
    }
}