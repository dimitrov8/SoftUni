using System;
using System.Security;

namespace _02._Character_Multiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            var sum = CalculateTotalSum(input);

            Console.WriteLine(sum);
        }

        private static int CalculateTotalSum(string[] input)
        {
            string longestString = input[0].Length > input[1].Length ? input[0] : input[1];
            string shortestString = input[0].Length < input[1].Length ? input[0] : input[1];
            int sum = 0;
            for (int i = 0; i < longestString.Length; i++)
            {
                if (i < shortestString.Length)
                {
                    sum += input[0][i] * input[1][i];
                    continue;
                }

                sum += longestString[i];
            }

            return sum;
        }
    }
}