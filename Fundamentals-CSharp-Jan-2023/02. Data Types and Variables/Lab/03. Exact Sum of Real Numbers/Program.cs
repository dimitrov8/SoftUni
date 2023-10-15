using System;

namespace Exact_Sum_Of_RealNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            decimal sum = 0;
            for (int i = 1; i <= rows; i++)
            {
                decimal currentNumber = decimal.Parse(Console.ReadLine());

                sum += currentNumber;
            }
            Console.WriteLine(sum);
        }
    }
}
