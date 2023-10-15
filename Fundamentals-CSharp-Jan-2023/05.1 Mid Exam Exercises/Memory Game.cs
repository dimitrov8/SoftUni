using System;
using System.Collections.Generic;
using System.Linq;

namespace Memory_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> numSeq = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            int moves = 0; // Move counter
            string command; // Command to execute
            while ((command = Console.ReadLine()) != "end")
            {
                moves++; // Move counter++
                
                // Take both indexes from the command and store them in a string
                string[] indexes = command.Split(" ");
                int index1 = int.Parse(indexes[0]); // Parse the first index to int
                int index2 = int.Parse(indexes[1]); // Parse the second index to int
                bool isValid = index1 >= 0 && index2 >= 0 && index1 < numSeq.Count && index2 < numSeq.Count &&
                               index1 < numSeq.Count && index1 != index2; // Check if the indexes are valid
                
                if (!isValid) // If the indexes are not valid
                {
                    numSeq.Insert(numSeq.Count / 2, $"-{moves}a"); // Insert
                    numSeq.Insert(numSeq.Count / 2, $"-{moves}a"); // Insert
                    Console.WriteLine("Invalid input! Adding additional elements to the board"); // Print
                    continue; // Go to the next command
                }

                bool isEqual = numSeq[index1] == numSeq[index2]; // Check if the indexes are equal

                if (isEqual) // If the indexes are equal
                {
                    string element = numSeq[index1]; // Get the element so we can print it
                    numSeq.RemoveAll(x => x == element); // Remove all elements that are equal
                    Console.WriteLine($"Congrats! You have found matching elements - {element}!"); // Print
                }
                else // If the indexes are not equal
                {
                    Console.WriteLine("Try again!"); // Print
                }

                if (numSeq.Count == 0) // If the board is empty
                {
                    Console.WriteLine($"You have won in {moves} turns!"); // Print
                    return; // Stop the program
                }
            }

            // If we get the "end" command
            Console.WriteLine($"Sorry you lose :( {Environment.NewLine}{string.Join(" ", numSeq)}"); // Print
        }
    }
}