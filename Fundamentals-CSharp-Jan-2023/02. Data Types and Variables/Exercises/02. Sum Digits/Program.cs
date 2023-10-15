using System;

namespace Sum_Digits
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            int sum = 0;
            int digit;

            while (number > 0)
            {
                digit = number % 10;
                sum += digit;
                number /= 10;
            }
            Console.WriteLine(sum);
        }
    }
}
