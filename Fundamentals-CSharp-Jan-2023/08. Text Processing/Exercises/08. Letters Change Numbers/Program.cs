using System;

namespace _08._Letters_Change_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            double result = 0;
            foreach (var dataInput in input) // dataInput is our current data from the input
            {
                char firstLetter = dataInput[0];
                char lastLetter = dataInput[^1];
                double number = double.Parse(dataInput.Substring(1, dataInput.Length - 2)); // Store only the number
                if (char.IsUpper(firstLetter))
                {
                    result += number / (firstLetter - 64);
                    result = CalculateLastLetter(lastLetter, result);
                }
                else if (char.IsLower(firstLetter))
                {
                    result += number * (firstLetter - 96);
                    result = CalculateLastLetter(lastLetter, result);
                }
            }

            Console.WriteLine($"{result:F2}");
        }

        private static double CalculateLastLetter(char lastLetter, double result)
        {
            if (char.IsUpper(lastLetter))
            {
                result -= lastLetter - 64;
            }
            else if (char.IsLower(lastLetter))
            {
                result += lastLetter - 96;
            }

            return result;
        }
    }
}