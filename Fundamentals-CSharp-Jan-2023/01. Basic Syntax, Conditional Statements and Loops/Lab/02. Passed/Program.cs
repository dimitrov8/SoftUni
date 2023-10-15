namespace Passed
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            double input = double.Parse(Console.ReadLine());

            if (input  >= 3.00)
            {
                Console.WriteLine("Passed!");
            }
        }
    }
}
