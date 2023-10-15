using System;

namespace _06._NxN_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            PrintNxNMatrix(input);
        }
        
        static void PrintNxNMatrix(int input)
        {
            for (int currRow = 1; currRow <= input; currRow++)
            {
                for (int currCol = 1; currCol <= input; currCol++)
                {
                    Console.Write(input + " ");
                }

                Console.WriteLine();
            }
        }
    }
}