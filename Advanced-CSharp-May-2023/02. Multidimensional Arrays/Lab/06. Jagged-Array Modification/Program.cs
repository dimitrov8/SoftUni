using System;
using System.Linq;

namespace _6._Jagged_Array_Modification
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] jaggedArray = new int[int.Parse(Console.ReadLine())][];

            for (int row = 0; row < jaggedArray.GetLength(0); row++)
            {
                int[] numbers = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                jaggedArray[row] = new int[numbers.Length];
                for (int col = 0; col < numbers.Length; col++)
                {
                    jaggedArray[row][col] = numbers[col];
                }
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split();
                string mainCommand = tokens[0];
                int row = int.Parse(tokens[1]);
                int col = int.Parse(tokens[2]);
                int value = int.Parse(tokens[3]);

                if (row < 0 || row > jaggedArray.GetLength(0) -1 || col < 0 || col > jaggedArray[row].Length - 1)
                {
                    Console.WriteLine("Invalid coordinates");
                    continue;
                }
                
                if (mainCommand == "Add")
                {
                    jaggedArray[row][col] += value;
                }
                else if (mainCommand == "Subtract")
                {
                    jaggedArray[row][col] -= value;
                }
            }

            for (int row = 0; row < jaggedArray.GetLength(0); row++)
            {
                for (int col = 0; col < jaggedArray[row].Length; col++)
                {
                    Console.Write($"{jaggedArray[row][col]} ");
                }

                Console.WriteLine();
            }
        }
    }
}