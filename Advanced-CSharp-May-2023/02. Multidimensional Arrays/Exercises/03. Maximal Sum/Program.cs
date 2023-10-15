using System;
using System.Linq;

namespace _3._Maximal_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine() // Read the number of rows and columns
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int[,] matrix = new int[rowsAndColumns[0], rowsAndColumns[1]]; // Assign the rows and columns in the matrix

            AssignValuesInTheMatrix(matrix); // Method: => Assigns user input values in the matrix
            FindTheMaximalSumAndPrintIt(matrix, out var rowAndColumStart); // Method: => Finds the maximal sum and prints it
            PrintMaximalSumValues(matrix, rowAndColumStart); // Method: => Prints the values which form the maximal sum
        }

        private static void PrintMaximalSumValues(int[,] matrix, int[] rowAndColumStart)
        {
            // Prints 3 rows and 3 columns
            for (int row = 0; row < 3; row++) 
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($"{matrix[row + rowAndColumStart[0], col + rowAndColumStart[1]]} ");
                }

                Console.WriteLine();
            }
        }

        private static void FindTheMaximalSumAndPrintIt(int[,] matrix, out int[] rowAndColumStart)
        {
            int maximalSum = 0; // Initialize the maximal sum value
            rowAndColumStart = new int[2]; // Variable which holds the starting row and column values that form the maximal sum
            for (int row = 0; row < matrix.GetLength(0) - 2; row++) // It's - 2, because we make sure we can get 3x3 
            {
                for (int col = 0; col < matrix.GetLength(1) - 2; col++) // It's -2, because we make sure we can get 3x3
                {
                    // Calculates the current sum
                    int currentSum = matrix[row, col] + matrix[row, col + 1] + matrix[row, col + 2]
                                     + matrix[row + 1, col] + matrix[row + 1, col + 1] + matrix[row + 1, col + 2]
                                     + matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];
                    if (currentSum > maximalSum) // If the current sum is greater than the current maximal sum
                    {
                        rowAndColumStart = new[] { row, col }; // Store the starting indices of the row and column
                        maximalSum = currentSum; // Store the maximal sum
                    }
                }
            }

            Console.WriteLine($"Sum = {maximalSum}"); // Print the maximal sum
        }

        private static void AssignValuesInTheMatrix(int[,] matrix) 
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // Foreach row
            {
                int[] numbers = Console.ReadLine() // User input to put in the current row and column
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++) // Foreach column
                {
                    matrix[row, col] = numbers[col]; // Assign current value to the current row and column in the matrix
                }
            }
        }
    }
}