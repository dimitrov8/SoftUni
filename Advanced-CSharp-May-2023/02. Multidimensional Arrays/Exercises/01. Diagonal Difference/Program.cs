using System;
using System.Linq;

namespace _1._Diagonal_Difference
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find the difference between the sums of the square matrix diagonals (absolute value)
            int rowsAndColumns = int.Parse(Console.ReadLine());
            int[,] matrix = new int[rowsAndColumns, rowsAndColumns];

            for (int row = 0; row < rowsAndColumns; row++)
            {
                int[] numbers = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                for (int col = 0; col < rowsAndColumns; col++)
                {
                    matrix[row, col] = numbers[col];
                }
            }

            // Find right diagonal sum
            int primaryDiagonalSum = 0;
            int secondaryDiagonalSum = 0;
            for (int row = 0; row < rowsAndColumns; row++)
            {
                primaryDiagonalSum += matrix[row, row];
                secondaryDiagonalSum += matrix[row, rowsAndColumns - row - 1];
            }

            int difference = Math.Abs(primaryDiagonalSum - secondaryDiagonalSum);
            Console.WriteLine(difference);
        }
    }
}