namespace _08._Math_Powe
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            double n = double.Parse(Console.ReadLine());
            int power = int.Parse(Console.ReadLine());

            double result = MathPower(n, power);
            Console.WriteLine(result);
        }

        static double MathPower(double n, int power)
        {
            double result = 1;

            for (int i = 0; i < power; i++)
            {
                result *= n;
            }
            return result;
        }
    }
}