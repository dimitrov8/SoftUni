using System;
using System.Linq;

namespace _2._Sum_Matrix_Columns
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine()
                .Split(",")
                .Select(int.Parse)
                .ToArray();
            int[,] matrix = new int[rowsAndColumns[0], rowsAndColumns[1]];

            for (int row = 0; row < rowsAndColumns[0]; row++)
            {
                int[] numbers = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                for (int col = 0; col < rowsAndColumns[1]; col++)
                {
                    matrix[row, col] = numbers[col];
                }
            }

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                int sum = 0;
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    sum += matrix[row, col];
                }
                Console.WriteLine(sum);
            }
        }
    }
}