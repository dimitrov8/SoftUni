using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Messaging
{
    class Program
    {
        static void Main(string[] args)
        {
            // List of numbers
            List<int> numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            // Declare new message list of CHARS
            List<char> message = new List<char>();

            // Use addRange to store the message as list of chars
            message.AddRange(Console.ReadLine());

            // For loop to calculate every single element of the numbers array
            for (int i = 0; i < numbers.Count; i++) 
            {
                // Current element(number)
                int currN = numbers[i];
                // Create variable for the sum
                int sum = 0;
                
                // Calculate the sum of every digit in the currentN
                while (currN != 0)
                {
                    sum += currN % 10;
                    currN /= 10;
                }

                // Go into our GetIndex Method
                // We need the (sum) and (message) because we use the inside the method to calculate the index
                int index = GetIndex(sum, message);

                // After our method
                // Print the character according to our found index
                Console.Write(message[index]);
                
                // Remove that char at that found index
                message.RemoveAt(index);
            }
        }

        static int GetIndex(int sum, List<char> message)
        {
            // Find the index by:
            // Dividing our sum with our message.count => Where we store the chars
            int index = Math.Max(sum, sum / message.Count);

            // If our index is bigger than our message.Count(where we store the chars)
            if (index > message.Count)
            {
                // Subtract our current found index by our message.Count 
                index -= message.Count;
            }
            return index;
        }
    }
}