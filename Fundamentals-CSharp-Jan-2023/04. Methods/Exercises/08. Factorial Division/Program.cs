using System;

namespace _08._Factorial_Division
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());

            double n1Factorial = GetFactorial(n1);
            double n2Factorial = GetFactorial(n2);
            double sum = GetSumAfterDividing(n1Factorial,n2Factorial);
            Console.WriteLine($"{sum:F2}");
        }

        static long GetFactorial(int a)
        {
            long factorial = 1;
            for (int i = a; i >= 1; i--)
            {
                factorial *= i;
            }

            return factorial;
        }

        static double GetSumAfterDividing(double a, double b)
        {
            return a / b;
        }
    }
}