using System;

namespace Special_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int theNumber = int.Parse(Console.ReadLine()); // 15

            for (int operations = 1; operations <= theNumber; operations++) // 1 to 15 Operations
            {
                // Every loop we sum = 0
                int sum = 0;
                // Digits = current Loop (Operation)
                int digits = operations;

                // While our operations are > 0
                while (digits > 0)
                {
                    // We get the last digit by % current operation with 10 and add it to the sum
                    sum += digits % 10;
                    digits /= 10; // We divide current operation by 10
                }


                if ( sum == 5 || sum == 7 || sum == 11)
                {
                    Console.WriteLine($"{operations} -> True");
                }
                else
                {
                    Console.WriteLine($"{operations} -> False");
                }
                
               
            }
        }
    }
}
