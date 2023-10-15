namespace Pounds_to_Dollars
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            double input = double.Parse(Console.ReadLine());

            Console.WriteLine($"{input * 1.31:f3}");
        }
    }
}
