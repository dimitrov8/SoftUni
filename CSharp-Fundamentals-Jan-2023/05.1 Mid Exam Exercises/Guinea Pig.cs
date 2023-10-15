using System;

namespace Guinea_Pig
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal foodQuanityKg = decimal.Parse(Console.ReadLine());
            decimal hayQuantityKg = decimal.Parse(Console.ReadLine());
            decimal coverQuantityKg = decimal.Parse(Console.ReadLine());

            decimal guineaKg = decimal.Parse(Console.ReadLine());

            int dayCount = 1; // Start from day 1 (not 0)
            for (int i = 1; i <= 30; i++)
            {
                foodQuanityKg -= (decimal)0.3;

                if (foodQuanityKg <= 0 || hayQuantityKg <= 0 || coverQuantityKg <= 0)
                {
                    break;
                }

                if (dayCount % 2 == 0)
                {
                    hayQuantityKg -= (decimal)0.05 * foodQuanityKg;
                }

                if (dayCount % 3 == 0)
                {
                    coverQuantityKg -= guineaKg / 3;
                }

                dayCount++;
            }

            if (foodQuanityKg <= 0 || hayQuantityKg <= 0 || coverQuantityKg <= 0)
            {
                Console.WriteLine("Merry must go to the pet store!");
            }
            else
            {
                Console.WriteLine($"Everything is fine! Puppy is happy! Food: {foodQuanityKg:f2}, Hay: {hayQuantityKg:f2}, Cover: {coverQuantityKg:f2}.");
            }
        }
    }
}