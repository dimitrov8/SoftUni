using System;

namespace _4._Symbol_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            // Matrix rows and columns size
            int size = int.Parse(Console.ReadLine());
            string[,] matrix = new string[size, size];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string symbols = Console.ReadLine();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = symbols[col].ToString();
                }
            }

            string symbolToFind = Console.ReadLine();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (symbolToFind == matrix[row, col])
                    {
                        Console.WriteLine($"({row}, {col})");
                        return;
                    }
                }
            }

            Console.WriteLine($"{symbolToFind} does not occur in the matrix");
        }
    }
}