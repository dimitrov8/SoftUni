namespace Division
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int divisibleNum = 0;
            bool isDivisible = false;
            if (n % 10 == 0)
            {
                divisibleNum = 10;
                isDivisible = true;
            }
            else if (n % 7 == 0)
            {
                divisibleNum = 7;
                isDivisible = true;
            }
            else if (n % 6 == 0)
            {
                divisibleNum = 6;
                isDivisible = true;
            }
            else if (n % 3 == 0)
            {
                divisibleNum = 3;
                isDivisible = true;
            }
            else if (n % 2 == 0)
            {
                divisibleNum = 2;
                isDivisible = true;
            }

            if (!isDivisible)
            {
                Console.WriteLine("Not divisible");
                return;
            }

            Console.WriteLine($"The number is divisible by {divisibleNum}");
        }
    }
}
