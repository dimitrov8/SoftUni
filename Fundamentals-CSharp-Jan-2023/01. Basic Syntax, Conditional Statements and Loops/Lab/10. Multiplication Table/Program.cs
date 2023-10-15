namespace Multiplication_Table
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int mulitply = 1; mulitply <= 10; mulitply++)
            {
                Console.WriteLine($"{n} X {mulitply} = {n * mulitply}");
            }
        }
    }
}
