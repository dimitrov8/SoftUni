using System;

namespace Pokemon
{
    class Program
    {
        static void Main(string[] args)
        {
            int pokePower = int.Parse(Console.ReadLine());
            int targetDistance = int.Parse(Console.ReadLine());
            int exhaustionFactor = int.Parse(Console.ReadLine());

            int pokedTargets = 0;
            double percentage = pokePower * 0.5;

            while (pokePower >= targetDistance)
            {
                pokedTargets++; // We succesfully poked a target
                pokePower -= targetDistance; // We subtract
                if (pokePower == percentage && exhaustionFactor != 0) // exhaustionFactor != 0
                                                                      // Because if it's 0 we divide by zero which gives us run time error
                                                                    
                {
                    pokePower /= exhaustionFactor; // pokePower /= exhaustionFactor
                }
            }
            Console.WriteLine(pokePower);
            Console.WriteLine(pokedTargets);
        }
    }
}
