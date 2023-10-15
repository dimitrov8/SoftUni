using System;

namespace Refactor_Special_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            bool isSpecial = false;

            for (int operations = 1; operations <= number; operations++)
            {
                int sum = 0;
                int digit = operations;
                while (digit > 0)

                {
                    sum += digit % 10;
                    digit = digit / 10;
                }
                isSpecial = (sum == 5) || (sum == 7) || (sum == 11);
                Console.WriteLine($"{operations} -> {isSpecial}");
            }
        }
    }
}
