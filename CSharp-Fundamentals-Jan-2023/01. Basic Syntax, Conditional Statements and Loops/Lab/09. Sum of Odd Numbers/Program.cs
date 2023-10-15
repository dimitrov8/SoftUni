namespace Sum_of_Odd_Numbers
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine()); 
            int sum = 0;
            for (int n = 1; n <= rows * 2; n += 2)
            {
                Console.WriteLine($"{n}");
                sum += n;
            }
            Console.WriteLine($"Sum: {sum}");
        }
    }
}
