using System;
using System.Linq;
using Microsoft.VisualBasic;

namespace _3._Primary_Diagonal
{
    class Program
    {
        static void Main(string[] args)
        {
            // It's a square 3x3
            int size = int.Parse(Console.ReadLine());
            int[,] matrix = new int[size, size];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = numbers[col];
                }
            }

            int primaryDiagonalSum = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                primaryDiagonalSum += matrix[row, row];
            }

            Console.WriteLine(primaryDiagonalSum);
        }
    }
}