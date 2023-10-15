using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Truck_Tour
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfPetrolPumps = int.Parse(Console.ReadLine()); // Number of petrol pumps
            Queue<Pump> petrolPumpsInfo = new Queue<Pump>(); // Queue of all petrol pump information
            EnqueueEveryPetrolPump(nOfPetrolPumps, petrolPumpsInfo); // Goes in the method where we put every petrol pump information in the queue
            var bestIndex = FindTheIndex(nOfPetrolPumps, petrolPumpsInfo); // Goes in the method where we find the best index from where we can complete the route 
            Console.WriteLine(bestIndex); // Print the index of the pump from which we can complete the route
        }

        private static int FindTheIndex(int nOfPetrolPumps, Queue<Pump> petrolPumpsInfo)
        {
            int bestIndex = 0; // The best index from where we can complete the route
            int fuel = 0; // The total fuel value
            for (int i = 0; i < nOfPetrolPumps; i++) // Loop through the petrol pumps
            {
                Pump currentPump = petrolPumpsInfo.Dequeue(); // Get the current pump from the stack and dequeue the current pump
                fuel += currentPump.Fuel - currentPump.Distance; // Calculates if we have enough fuel to go to the next petrol pump
                if (fuel < 0) // If we have enough fuel to go to the next petrol pump
                {
                    fuel = 0; // Reset the fuel
                    bestIndex = i + 1; // And increment the best index because the petrol pump where we at didn't have enough fuel to go to the next petrol pump
                }
            }

            return bestIndex;
        }

        private static void EnqueueEveryPetrolPump(int nOfPetrolPumps, Queue<Pump> petrolPumpsInfo)
        {
            for (int i = 0; i < nOfPetrolPumps; i++) // Enqueues every petrol pum in the queue
            {
                int[] inputPumpValues = Console.ReadLine() // Reads the current input values of the petrol pump information
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();
                petrolPumpsInfo.Enqueue(new Pump(inputPumpValues[0], inputPumpValues[1])); // Adds the petrol pump in the queue
            }
        }
    }

    public class Pump
    {
        public int Fuel { get; set; }
        public int Distance { get; set; }

        public Pump(int fuel, int distance)
        {
            Fuel = fuel;
            Distance = distance;
        }
    }
}