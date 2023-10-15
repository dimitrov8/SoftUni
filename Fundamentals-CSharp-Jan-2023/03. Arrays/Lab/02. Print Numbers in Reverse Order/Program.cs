using System;

namespace PrintNumbersInReverseOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows  = int.Parse(Console.ReadLine());

            int[] number = new int [rows];
            for (int i = 0; i < rows; i++)
            {
                number[i] = int.Parse(Console.ReadLine());
            }
            for (int i = rows - 1; i >= 0; i--)
            {
                Console.Write(number[i] + " ");
            }
        }
    }
}