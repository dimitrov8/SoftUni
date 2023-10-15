using System;
using System.Collections.Generic;
using System.Linq;

namespace BeaverAtWork
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = int.Parse(Console.ReadLine()!); // User input for the length of the matrix
            char[,] matrix = new Char[length, length]; // Create the matrix

            int branchesInThePond = 0; // Gets the number of branches in the pond  
            AssignMatrixValues(matrix, ref branchesInThePond); // Method: => Assigns user input values in the matrix and get the total number of branches in the pond

            List<char> branches = new List<char>(); // Keeps track of the successfully collected branches
            int totalCollectedBranches = 0; // Keeps track of the total branches the Beaver collected even the dropped ones
            Position beaver = GetBeaverPosition(matrix); // Gets the Beaver position

            string command; 
            while ((command = Console.ReadLine()) != "end") // While the command is not "end"
            {
                // Variables to keep track of the position the user wants to move the Beaver to
                int rowToMoveTo = beaver.Row; 
                int colToMoveTo = beaver.Col;

                switch (command) // Depending on the move command update the variables:
                {
                    case "up":
                        rowToMoveTo--;
                        break;
                    case "down":
                        rowToMoveTo++;
                        break;
                    case "left":
                        colToMoveTo--;
                        break;
                    case "right":
                        colToMoveTo++;
                        break;
                }
                // If the Beaver collected all branches in the pond
                if (totalCollectedBranches == branchesInThePond)
                {
                    break; // Break out of the loop
                }
                
                // If the position the user wants to move the Beaver to is not withing the bounds of the pond(matrix)
                if (rowToMoveTo < 0 || rowToMoveTo >= matrix.GetLength(0) || colToMoveTo < 0 || colToMoveTo >= matrix.GetLength(1))
                {
                    if (branches.Any()) // If the Beaver has any branches: => The Beaver drops the last branch collected
                    {
                        branches.RemoveAt(branches.Count - 1); // Remove the last branch from the list
                    }
                    continue; // Continue the loop
                }
                
                // Get the element which is in the new position
                char element = matrix[rowToMoveTo, colToMoveTo];
                
                // Update Beaver position
                UpdateBeaverPosition(matrix, beaver, rowToMoveTo, colToMoveTo);

                // Checks if the new position is at the end of the pond(matrix)
                bool isAtTheEndOfThePond = IsAtTheEndOfThePond(matrix, rowToMoveTo, colToMoveTo);
                
                // If the new position had a fish
                if (element == 'F')
                {
                    {
                        switch (command)
                        {
                            // If the fish was NOT located in the last index, the Beaver swims to the last index in the direction it received. => Logic is reversed
                            case "up":
                                rowToMoveTo = isAtTheEndOfThePond ? 0 : matrix.GetLength(0) - 1;
                                break;
                            case "down":
                                rowToMoveTo = isAtTheEndOfThePond ? 0 : matrix.GetLength(0) - 1;
                                break;
                            case "right":
                                colToMoveTo = isAtTheEndOfThePond ? 0 : matrix.GetLength(1) - 1;
                                break;
                            case  "left":
                                colToMoveTo = isAtTheEndOfThePond ? matrix.GetLength(1) - 1 : 0;
                                break;
                        }
                    }
                    UpdateBeaverPosition(matrix, beaver, rowToMoveTo, colToMoveTo);
                }
                
                // If the new position had a branch
                else if (char.IsLower(element))
                {
                    branches.Add(element); // Add the branch to the list
                    totalCollectedBranches++; // Increment the totalCollectedBranches
                    UpdateBeaverPosition(matrix, beaver, rowToMoveTo, colToMoveTo); // Update the Beaver position            
                }
            }

            // Output
            Console.WriteLine(totalCollectedBranches == branchesInThePond
                ? $"The Beaver successfully collect {branches.Count} wood branches: {string.Join(", ", branches)}."
                : $"The Beaver failed to collect every wood branch. There are {branchesInThePond - totalCollectedBranches} branches left.");
            PrintMatrix(matrix); // Print the matrix
        }

        private static void AssignMatrixValues(char[,] matrix, ref int branchesInThePond) // Methods: Assigns values to the matrix
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                char[] input = Console.ReadLine()!.Split(' ').Select(char.Parse).ToArray(); // Get user input values
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    char currentElement = input[col]; // Get the current element from the input array
                    if (char.IsLower(currentElement)) // If the element is a branch
                    {
                        branchesInThePond++; // Increment the branchesInThePond
                    }
                    matrix[row, col] = currentElement; // Assign the value to the matrix
                }
            }
        }

        private static Position GetBeaverPosition(char[,] matrix) // Method: Gets the Beaver position
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    char currentElement = matrix[row, col]; // Current element
                    if (currentElement == 'B') // If the current element is the Beaver
                    {
                        return new Position(row, col); // Return the current position of the Beaver
                    }
                 
                }
            }

            return null; // Return null if the Beaver is not in the pond(matrix)
        }

        private static bool IsAtTheEndOfThePond(char[,] matrix, int rowToMoveTo, int colToMoveTo) // Method: Checks if the position the user wants to move the Beaver to is at the end of the pond(matrix)
        {
            return rowToMoveTo < 0 || rowToMoveTo >= matrix.GetLength(0) || colToMoveTo < 0 || colToMoveTo >= matrix.GetLength(1);
        }

        private static void UpdateBeaverPosition(char[,] matrix, Position beaver, int rowToMoveTo, int colToMoveTo) // Method: Updates the Beaver position
        {
            matrix[beaver.Row, beaver.Col] = '-'; // Remove the Beaver from its old position
            matrix[rowToMoveTo, colToMoveTo] = 'B'; // Moves the Beaver to its new position

            // Updates the Beaver position class
            beaver.Row = rowToMoveTo; 
            beaver.Col = colToMoveTo;
        }

        private static void PrintMatrix(char[,] matrix) // Method: Prints the matrix
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    Console.Write($"{matrix[row, col]} "); // Print the current element
                }

                Console.WriteLine();
            }
        }
    }

    public class Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
    }
}
