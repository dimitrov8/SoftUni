using System.Threading.Channels;

namespace _10._Multiply_Evens_by_Odds
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int num = Math.Abs(int.Parse(Console.ReadLine()));

            int result = findTotalSum(num);
            Console.WriteLine(result);
        }

        // Methods
        static int findSumOfEvens(int num)
        {
            int sumOfEvens = 0;

            while (num > 0)
            {
                int currN = num % 10;
                num /= 10;

                if (currN % 2 == 0)
                {
                    sumOfEvens += currN;
                }
            }

            return sumOfEvens;
        }

        static int findSumOfOdds(int num)
        {
            int sumOfOdds = 0;

            while (num > 0)
            {
                int currN = num % 10;
                num /= 10;

                if (currN % 2 != 0)
                {
                    sumOfOdds += currN;
                }
            }

            return sumOfOdds;
        }

        static int findTotalSum(int num)
        {
            int totalSum = findSumOfEvens(num) * findSumOfOdds(num);
            return totalSum;
        }
    }
}