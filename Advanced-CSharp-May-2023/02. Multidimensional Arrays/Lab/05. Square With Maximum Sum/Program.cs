using System;
using System.Linq;

namespace _5._Square_With_Maximum_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine()
                .Split(",")
                .Select(int.Parse)
                .ToArray(); // User input
            int[,] matrix =
                new int[rowsAndColumns[0],
                    rowsAndColumns[1]]; // Create a matrix with rows and columns from the user input
            AssignValuesInTheMatrix(matrix); // Go to method => Assign user input values to the matrix

            int biggestSum = 0;
            int bestRow = -1; // Default value for the best row in the matrix
            int bestCol = -1; // Default value for the best column in the matrix
            
            // We loop through the columns of the matrix, but stop at the second-to-last column
            // => because we need to check the value to the right of the current column to form a 
            // ==> 2x2 square. If we continued to the last column and there was no column to the right 
            // ===> of it, we would get an "IndexOutOfRange" exception.
            for (int col = 0; col < matrix.GetLength(1) - 1; col++)
            {
                // We loop through the rows of the matrix, but stop at the second-to-last row
                // => because we need to check the value below the current row to form a 2x2 square.
                // ==> If we continued to the last row and there was no row below it, we would get an
                // ===> "IndexOutOfRange" exception.
                for (int row = 0; row < matrix.GetLength(0) - 1; row++)
                {
                    // Current row and current column
                    // Current row and next column
                    // Next row and current column
                    // Next row and next column
                    int currSum = matrix[row, col]
                                  + matrix[row, col + 1]
                                  + matrix[row + 1, col]
                                  + matrix[row + 1, col + 1];
                    if (currSum > biggestSum) // If the current sum is bigger than the biggest sum currently
                    {
                        biggestSum = currSum; // Update the biggest sum
                        bestRow = row; // Update the best row
                        bestCol = col; // Update the best column
                    }
                }

            }
            PrintBestSumAndItsValues(matrix, bestRow, bestCol, biggestSum); // Go to method which print tbe best sum and it's values => 
        }

        private static void PrintBestSumAndItsValues(int[,] matrix, int bestRow, int bestCol, int biggestSum)
        {
            Console.WriteLine($"{matrix[bestRow, bestCol]} {matrix[bestRow, bestCol + 1]}");
            Console.WriteLine($"{matrix[bestRow + 1, bestCol]} {matrix[bestRow + 1, bestCol + 1]}");
            Console.WriteLine(biggestSum);
        }

        private static void AssignValuesInTheMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] numbers = Console.ReadLine()
                    .Split(",")
                    .Select(int.Parse)
                    .ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = numbers[col];
                }
            }
        }
    }
}