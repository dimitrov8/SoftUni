using System;
using System.Collections.Generic;
using System.Linq;

namespace ClimbThePeaks
{
    class Program
    {
        static void Main(string[] args)
        {
            var mountainsToClimb = new Dictionary<string, int> // Mountains to climb with their required stamina to be able to climb them
            {
                { "Vihren", 80 },
                { "Kutelo", 90 },
                { "Banski Suhodol", 100 },
                { "Polezhan", 60 },
                { "Kamenitza", 70 },
            };

            var mountainsLeftToClimb = new Queue<string>(); // Create a queue which stores the mountains left to climb 
            foreach (var mountain in mountainsToClimb.Keys) // For each mountain 
            {
                mountainsLeftToClimb.Enqueue(mountain); // Add it to the mountains left to climb
            }

            var successfullyClimbedMountains = new Queue<string>(); // Keeps track of the successfully climbed mountains

            var foodPortions = new Stack<int>(Console.ReadLine()! // User input for the food portions
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            var stamina = new Queue<int>(Console.ReadLine()! // User input for the stamina 
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            while (foodPortions.Any() && stamina.Any() && mountainsLeftToClimb.Any()) // While there is food, stamina and mountains left to climb
            {
                int currentStamina = foodPortions.Pop() + stamina.Dequeue(); // Get the current stamina
                string currentMountain = mountainsLeftToClimb.Peek(); // Get the current mountain to climb
                int requiredStamina = mountainsToClimb[currentMountain]; // Get the required stamina for the current mountain to be able to successfully climb it
                if (currentStamina >= requiredStamina) // If the currentStamina is enough to be able to climb the current mountain
                {
                    successfullyClimbedMountains.Enqueue(mountainsLeftToClimb.Dequeue()); // Add the current mountain to the successfully climbed mountains and remove the current mountain from the mountain left to climb
                }
            }

            // Print
            Console.WriteLine(mountainsLeftToClimb.Count > 0
                ? "Alex failed! He has to organize his journey better next time -> @PIRINWINS"
                : "Alex did it! He climbed all top five Pirin peaks in one week -> @FIVEinAWEEK");

            if (successfullyClimbedMountains.Count <= 0) return;
            Console.WriteLine("Conquered peaks:");
            Console.WriteLine(string.Join(Environment.NewLine, successfullyClimbedMountains));
        }
    }
}
