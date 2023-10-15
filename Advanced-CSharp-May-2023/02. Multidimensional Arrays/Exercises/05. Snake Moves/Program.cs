using System;
using System.Collections.Generic;
using System.Linq;

namespace _5._Snake_Moves
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine() // Size of the rows and columns in the matrix
                .Split()
                .Select(int.Parse)
                .ToArray();
            char[,] matrix = new char[rowsAndColumns[0], rowsAndColumns[1]]; // Assign the rows and columns to the matrix
            Queue<char> snake = new Queue<char>(Console.ReadLine().ToCharArray()); // Create a new queue to hold the snake string

            AssignValuesZigZag(matrix, snake);
            PrintTheMatrix(matrix);
        }

        private static void AssignValuesZigZag(char[,] matrix, Queue<char> snake)
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                if (row % 2 == 0) // If we are on a even row 
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        matrix[row, col] = snake.Peek(); // Assign the current snake value in the current column
                        snake.Enqueue(snake
                            .Dequeue()); // Put the current snake value last in the queue and dequeue it from the first position
                    }
                }
                else // If we are on an odd row
                {
                    for (int col = matrix.GetLength(1) - 1; col >= 0; col--)
                    {
                        matrix[row, col] = snake.Peek(); // Assign the current snake value in the current column
                        snake.Enqueue(snake
                            .Dequeue()); // Put the current snake value last in the queue and dequeue it from the first position
                    }
                }
            }
        }

        private static void PrintTheMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++) // For each row
            {
                for (int col = 0; col < matrix.GetLength(1); col++) // For each column
                {
                    Console.Write($"{matrix[row, col]}"); // Print the value in the current row and column
                }

                Console.WriteLine(); // Go on next line for the next row
            }
        }
    }
}