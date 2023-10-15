using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Car_Race
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            // Final Line
            int middle = numbers.Count / 2;

            double left = 0;
            double right = 0;

            left = FindLeftCarTime(middle, numbers, left);

            right = FindRightCarTime(numbers, middle, right);
            
            PrintWinner(right, left);
        }

        private static void PrintWinner(double right, double left)
        {
            if (right < left)
            {
                Console.WriteLine($"The winner is right with total time: {right}");
            }
            else if (right > left)
            {
                Console.WriteLine($"The winner is left with total time: {left}");
            }
        }

        static double FindRightCarTime(List<int> numbers, int middle, double right)
        {
            for (int i = numbers.Count - 1; i > middle; i--)
            {
                if (numbers[i] == 0)
                {
                    right *= 0.8;
                }

                right += numbers[i];
            }

            return right;
        }

        static double FindLeftCarTime(int middle, List<int> numbers, double left)
        {
            for (int i = 0; i < middle; i++)
            {
                if (numbers[i] == 0)
                {
                    left *= 0.8;
                }

                left += numbers[i];
            }

            return left;
        }
    }
}