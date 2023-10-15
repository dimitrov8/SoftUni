using System;

namespace Water_Overflow
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputRows = int.Parse(Console.ReadLine());

            int totalLiters = 0;
            for (int i = 1; i <= inputRows; i++)
            {
                int litersAdded = int.Parse(Console.ReadLine());
                totalLiters += litersAdded;

                if (totalLiters > 255)
                {
                    Console.WriteLine("Insufficient capacity!");
                    totalLiters -= litersAdded;
                }
            }
            Console.WriteLine(totalLiters);
        }
    }
}
