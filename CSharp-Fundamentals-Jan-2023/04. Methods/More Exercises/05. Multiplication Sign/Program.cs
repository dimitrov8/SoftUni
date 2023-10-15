using System;

namespace _05._Multiplication_Sign
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isPositive = true;
            bool isZero = false;
            int count = 0;
            for (int i = 1; i <= 3; i++)
            {
                double n = double.Parse(Console.ReadLine());

                isPositive = CheckNumber(n, isPositive, ref count, ref isZero);
            }

            if (isZero)
            {
                Console.WriteLine("zero");
            }
            else
            {
                Console.WriteLine(!isPositive ? "negative" : "positive");
            }
        }

        static bool CheckNumber(double n, bool isPositive, ref int count, ref bool isZero)
        {
            if (n < 0) // If n is negative
            {
                isPositive = false;
                count++; // Add counter
                if (count % 2 == 0) // If count is even meaning we got two negative numbers in a row
                {
                    isPositive = true; // Our number is positive because - - equals +
                }
            }
            else if (n == 0) // If n is zero
            {
                isZero = true; // Zero multiplied by any number is zero. ref bool isZero = true;
            }

            return isPositive; // Returns if it's positive or not. In any other case... default value is true;
        }
    }
}