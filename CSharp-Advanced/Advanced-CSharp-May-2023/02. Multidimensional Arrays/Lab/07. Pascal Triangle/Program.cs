using System;

namespace _7._Pascal_Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            long[][] jaggedArray = new long[rows][];
            for (int row = 0; row < rows; row++)
            {
                jaggedArray[row] = new long[row + 1];
                for (int col = 0; col < row + 1; col++)
                {
                    if (col == 0 || col == row)
                    {
                        jaggedArray[row][col] = 1;
                        continue;
                    }

                    jaggedArray[row][col] = jaggedArray[row - 1][col - 1] + jaggedArray[row - 1][col];
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