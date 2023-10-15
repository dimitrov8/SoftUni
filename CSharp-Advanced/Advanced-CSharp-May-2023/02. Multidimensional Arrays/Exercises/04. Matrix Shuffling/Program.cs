using System;
using System.Linq;

namespace _4._Matrix_Shuffling
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            string[,] matrix = new String[rowsAndColumns[0], rowsAndColumns[1]];
            AssignValuesInTheMatrix(matrix);

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split();
                if (tokens.Length - 1 > 4 || tokens.Length - 1 < 4 || tokens[0] != "swap")
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                
                int row1 = int.Parse(tokens[1]);
                int col1 = int.Parse(tokens[2]);
                int row2 = int.Parse(tokens[3]);
                int col2 = int.Parse(tokens[4]);

                if (row1 < 0 || row1 > matrix.GetLength(0) - 1 || col1 < 0 || col1 > matrix.GetLength(1) - 1
                    || row2 < 0 || row2 > matrix.GetLength(0) - 1 || col2 < 0 || col2 > matrix.GetLength(1) - 1)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                (matrix[row1, col1], matrix[row2, col2]) = (matrix[row2, col2], matrix[row1, col1]);

                PrintTheModifiedMatrix(matrix);
            }
        }

        private static void PrintTheModifiedMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]} ");
                }

                Console.WriteLine();
            }
        }

        private static void AssignValuesInTheMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string[] values = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = values[col];
                }
            }
        }
    }
}