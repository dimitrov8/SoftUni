using System;

namespace Town_Info
{
    class Program
    {
        static void Main(string[] args)
        {
            string town = Console.ReadLine();
            int population = int.Parse(Console.ReadLine());
            ushort area = ushort.Parse(Console.ReadLine());

            Console.Write($"Town {town} has population of {population} and area {area} square km.");
        }
    }
}
