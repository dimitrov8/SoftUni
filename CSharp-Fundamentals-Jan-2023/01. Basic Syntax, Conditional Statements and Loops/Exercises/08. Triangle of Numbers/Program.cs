﻿namespace Triangle_of_Numbers
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); 

            for (int row = 1; row <= n; row++)
            {
                for (int times = 1; times <= row; times++)
                {
                    Console.Write(row + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
