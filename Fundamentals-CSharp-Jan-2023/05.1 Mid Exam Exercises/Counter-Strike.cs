using System;

namespace Counter_Strike
{
    class Program
    {
        static void Main(string[] args)
        {
            int energy = int.Parse(Console.ReadLine());

            string command;
            int win = 0;
            while ((command = Console.ReadLine()) != "End of battle")
            {
                int distance = int.Parse(command);

                if (energy < distance)
                {
                    Console.WriteLine($"Not enough energy! Game ends with {win} won battles and {energy} energy");
                    return;
                }

                energy -= distance;
                win++;
                if (win % 3 == 0)
                {
                    energy += win;
                }

                if (energy < 0)
                {
                    Console.WriteLine($"Not enough energy! Game ends with {win} won battles and {energy} energy");
                    return;
                }
            }

            Console.WriteLine($"Won battles: {win}. Energy left: {energy}");
        }
    }
}