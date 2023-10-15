using System;

namespace _10._Top_Number
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());

            for (int i = 1; i <= number; i++)
            {
                if (IsDivisibleBy8(i) && ContainsOddDigit(i)) 
                {
                    Console.WriteLine(i);
                }
            }
        }

        static bool ContainsOddDigit(int number)
        {
            while (number > 0)
            {
                int currN = number % 10;

                if (currN % 2 != 0)
                {
                    return true;
                }

                number /= 10;
            }

            return false;
        }

        static bool IsDivisibleBy8(int number)
        {
            int sum = 0;

            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }

            bool IsDivisible = sum % 8 == 0;
            return IsDivisible;
        }
    }
}