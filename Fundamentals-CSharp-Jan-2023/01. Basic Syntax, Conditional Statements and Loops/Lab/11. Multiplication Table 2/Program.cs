namespace Multiplication_Table_2
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int multiply = int.Parse(Console.ReadLine());

            if (multiply > 10)
            {
                Console.WriteLine($"{n} X {multiply} = {n * multiply}");
            }

            for (int i = multiply; i <= 10; i++)
            {
                Console.WriteLine($"{n} X {i} = {n * i}");
            }
            
        }
    }
}
