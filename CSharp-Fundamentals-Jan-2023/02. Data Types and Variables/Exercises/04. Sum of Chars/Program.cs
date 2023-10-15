using System;

namespace Sum_Of_Chars
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int sum = 0;

            for (int i = 1; i <= rows; i++)
            {
                char input = char.Parse(Console.ReadLine());
                sum += input;
            }
            Console.WriteLine($"The sum equals: {sum}");
        }
    }
}
