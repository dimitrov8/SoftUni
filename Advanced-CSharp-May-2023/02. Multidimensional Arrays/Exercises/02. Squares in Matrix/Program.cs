using System;
using System.Linq;

namespace _2._Squares_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine() // User input for the size of the matrix
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            char[,] matrix = new Char[rowsAndColumns[0], rowsAndColumns[1]]; // Assign the rows and columns for the matrix

            AssignValuesInTheMatrix(matrix); // Method: => Which assigns the user values in the matrix
            FindSquareMatricesFoundAndPrint(matrix); // Method: => Which finds if there are any square matrices in the matrix and prints the count of the square matrices
        }

        private static void FindSquareMatricesFoundAndPrint(char[,] matrix)
        {
            int counter = 0; // Count to keep track of all square matrices we have found 
            for (int row = 0; row < matrix.GetLength(0) - 1; row++) // Foreach row 
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++) // Foreach column
                {
                    // Check if there are any square matrices
                    if (matrix[row, col] == matrix[row, col + 1] &&
                        matrix[row, col] == matrix[row + 1, col] &&
                        matrix[row, col] == matrix[row + 1, col + 1])
                    {
                        counter++; // If a square matrix is found then increment counter
                    }
                }
            }

            Console.WriteLine(counter); // Print the number of square matrices found
        }

        private static void AssignValuesInTheMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // Foreach row
            {
                char[] letters = Console.ReadLine() // Read letters
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++) // Foreach column in the current row
                {
                    matrix[row, col] = letters[col]; // Assign the current letter
                }
            }
        }
    }
}