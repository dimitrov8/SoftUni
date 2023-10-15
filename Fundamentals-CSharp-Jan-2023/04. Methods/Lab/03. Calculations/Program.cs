namespace _03._Calculations
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            string option = Console.ReadLine();

            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());

            switch (option)
            {
                case "add":
                    Add(a, b);
                    break;
                case "subtract":
                    Subtract(a, b);
                    break;
                case "multiply":
                    Multiply(a, b);
                    break;
                case "divide":
                    if (b != 0)
                    {
                        Divide(a, b);
                    }
                    break;
            }
        }
        private static void Add(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        private static void Subtract(int a, int b)
        {
            Console.WriteLine($"{Math.Abs(a - b)}");
        }

        private static void Multiply(int a, int b)
        {
            Console.WriteLine(a * b);
        }

        private static void Divide(int a, int b)
        {
            Console.WriteLine(a / b);
        }
    }
}