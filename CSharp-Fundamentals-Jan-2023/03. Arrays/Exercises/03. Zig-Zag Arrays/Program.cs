using System;
using System.Linq;

namespace ZigZagArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a program that creates 2 ARRAYS

            // You are given an int N
            int rows = int.Parse(Console.ReadLine());

            // Array that contains the numbers in the array
            int[] arr1 = new int[rows];
            int[] arr2 = new int[rows];

            // On the next N lines => 
            for (int i = 0; i < rows; i++)
            {
                int[] input = Console.ReadLine() // You get 2 integers as an input
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                // Form 2 new arrays in zig-zag pattern
                if (i % 2 == 0)
                {
                    arr1[i] = input[0];
                    arr2[i] = input[1];
                }

                else
                {
                    arr1[i] = input[1];
                    arr2[i] = input[0];
                }
            }
            Console.WriteLine(string.Join(" ", arr1));
            Console.WriteLine(string.Join(" ", arr2));
        }
    }
}