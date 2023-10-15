using System;
using System.Linq;

namespace The_Lift
{
    class Program
    {
        static void Main(string[] args)
        {
            int people = int.Parse(Console.ReadLine());
            int[] wagons = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            for (int i = 0; i < wagons.Length; i++)
            {
                while (wagons[i] < 4 && people > 0)
                {
                    people--;
                    wagons[i]++;
                }
            }

            if (people == 0 && wagons.Any(x => x != 4))
            {
                Console.WriteLine("The lift has empty spots!");
                Console.WriteLine(string.Join(" ", wagons));
            }
            else if (people > 0)
            {
                Console.WriteLine($"There isn't enough space! {people} people in a queue!");
                Console.WriteLine(string.Join(" ", wagons));
            }

            else if (people == 0 && wagons.All(x => x == 4))
            {
                Console.WriteLine(string.Join(" ", wagons));
            }
        }
    }
}