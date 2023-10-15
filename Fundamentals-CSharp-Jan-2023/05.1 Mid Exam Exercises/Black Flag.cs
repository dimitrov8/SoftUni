using System;

namespace Black_Flag
{
    class Program
    {
        static void Main(string[] args)
        {
            int days = int.Parse(Console.ReadLine());
            int dailyPlunder = int.Parse(Console.ReadLine());
            int expectedPlunder = int.Parse(Console.ReadLine());

            double gainedPlunder = 0;

            for (int i = 1; i <= days; i++)
            {
                gainedPlunder += dailyPlunder;

                if (i % 3 == 0)
                {
                    gainedPlunder += (double)dailyPlunder / 2;
                }

                if (i % 5 == 0)
                {
                    gainedPlunder -= gainedPlunder * 0.3;
                }
            }

            if (gainedPlunder >= expectedPlunder)
            {
                Console.WriteLine($"Ahoy! {gainedPlunder:f2} plunder gained.");
            }
            else
            {
                double percent = gainedPlunder / expectedPlunder * 100;
                Console.WriteLine($"Collected only {percent:f2}% of the plunder.");
            }
        }
    }
}