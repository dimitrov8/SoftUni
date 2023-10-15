using System;
using System.Linq;

namespace _6._Jagged_Array_Manipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] jaggedArray = new int[int.Parse(Console.ReadLine())][];

            AssignValuesInTheJaggedArray(jaggedArray); // Method: => Which assigns values in the jagged array
            MultiplyOrDivideElements(jaggedArray); // Method: => Multiply or divide elements in the jagged array base on the length of the jagged of the current row and next row
            ModifyTheJaggedArray(jaggedArray); // Method: => Modify the jagged array ("Add" or "Subtract") based on user input
            PrintTheJaggedArray(jaggedArray); // Method: => Print the jagged array
        }

        private static void PrintTheJaggedArray(int[][] jaggedArray)
        {
            for (int row = 0; row < jaggedArray.GetLength(0); row++) 
            {
                for (int col = 0; col < jaggedArray[row].Length; col++)
                {
                    Console.Write($"{jaggedArray[row][col]} ");
                }

                Console.WriteLine();
            }
        }

        private static void ModifyTheJaggedArray(int[][] jaggedArray)
        {
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string mainCommand = tokens[0];
                int row = int.Parse(tokens[1]);
                int col = int.Parse(tokens[2]);
                int value = int.Parse(tokens[3]);

                // Boolean to indicate whether the user indexes are valid
                bool isValid = row >= 0 && row < jaggedArray.Length && col >= 0 && col < jaggedArray[row].Length;
                if (mainCommand == "Add" && isValid) // If the command is "Add" and the indexes are valid
                {
                    jaggedArray[row][col] += value; // Add the value
                }
                else if (mainCommand == "Subtract" && isValid) // If the command is "Subtract" and the indexes are valid
                {
                    jaggedArray[row][col] -= value; // Subtract the value
                }
            }
        }

        private static void MultiplyOrDivideElements(int[][] jaggedArray)
        {
            for (int row = 0; row < jaggedArray.Length - 1; row++) // For each row 
            {
                if (jaggedArray[row].Length == jaggedArray[row + 1].Length) // If the current row and the next row have the same length
                {
                    for (int col = 0; col < jaggedArray[row].Length; col++) // For each column
                    {
                        jaggedArray[row][col] *= 2; // Multiply
                        jaggedArray[row + 1][col] *= 2; // Multiply
                    }
                }
                else // If the current and next rows are not equal length
                {
                    for (int col = 0; col < jaggedArray[row].Length; col++) // For each column in the current row
                    {
                        jaggedArray[row][col] /= 2; // Divide
                    }

                    for (int col = 0; col < jaggedArray[row + 1].Length; col++) // For each column in the next row
                    {
                        jaggedArray[row + 1][col] /= 2; // Divide
                    }
                }
            }
        }

        private static void AssignValuesInTheJaggedArray(int[][] jaggedArray)
        {
            for (int row = 0; row < jaggedArray.GetLength(0); row++)
            {
                int[] numbers = Console.ReadLine()// User input which in
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                jaggedArray[row] = numbers;
            }
        }
    }
}