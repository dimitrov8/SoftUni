using System;

namespace _11._Math_operations
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = Math.Abs(double.Parse(Console.ReadLine()));
            char action = char.Parse(Console.ReadLine());
            double b = Math.Abs(double.Parse(Console.ReadLine()));

            double result = Calculate(a, action, b);
            Console.WriteLine(result);
        }

        private static double Calculate(double a, char action, double b)
        {
            double result = 0;
            switch (action)
            {
                case '+':
                    result = a + b;
                    break;
                case '-':
                    result = a - b;
                    break;
                case '*':
                    result = a * b;
                    break;
                case '/':
                    if (b != 0)
                    {
                        result = a / b;
                    }

                    break;
            }

            return result;
        }
    }
}