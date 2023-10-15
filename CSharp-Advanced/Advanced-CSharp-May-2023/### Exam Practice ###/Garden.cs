using System;
using System.Collections.Generic;
using System.Linq;

namespace Garden
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndCols = Console.ReadLine()!
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray(); // 
            int[,] matrix = new int[rowsAndCols[0], rowsAndCols[1]];

            Dictionary<int, int> flowerPositions = new Dictionary<int, int>();
            string command; // Initialize the command
            while ((command = Console.ReadLine()) != "Bloom Bloom Plow") // While the command is not "End"
            {
                string[] indexToPlantFlower = command!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int row = int.Parse(indexToPlantFlower[0]);
                int col = int.Parse(indexToPlantFlower[0]);

                if (row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1))
                {
                    Console.WriteLine("Invalid coordinates.");
                    continue;
                }

                flowerPositions.Add(row, col);
            }

            foreach (var flowerPosition in flowerPositions)
            {
                int flowerRow = flowerPosition.Key;
                int flowerCol = flowerPosition.Value;
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    if (row == flowerRow)
                    {
                        for (int col = 0; col < matrix.GetLength(0); col++)
                        {
                            if (col == flowerRow)
                            {
                                continue;
                            }
                            matrix[row, col]++;
                        } 
                    }

                    
                    matrix[row, flowerCol]++;
                }
            }

            PrintMatrix(matrix);
        }


        private static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row in the matrix
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column in the matrix
                {
                    Console.Write($"{matrix[row, col]} "); // Print the current element
                }

                Console.WriteLine();
            }
        }
    }
}