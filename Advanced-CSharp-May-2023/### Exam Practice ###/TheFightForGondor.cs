using System;
using System.Collections.Generic;
using System.Linq;
namespace TheFightForGondor
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfWaves = int.Parse(Console.ReadLine()!); // Number of waves

            var plates = new Queue<int>(Console.ReadLine()! // Plates
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            var remainingWarriors = new Stack<int>(); // Store the remaining warriors
            bool isDestroyed = false; // Bool to check if all plates are destroyed
            for (int wave = 1; wave <= nOfWaves; wave++) // For each wave
            {
                var warriors = new Stack<int>(Console.ReadLine()! // Current warriors for the current wave
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse));

                if (wave % 3 == 0) // Every third wave
                {
                    plates.Enqueue(int.Parse(Console.ReadLine()!)); // Add new plate
                }

                while (warriors.Any() && plates.Any()) // While there are warriors and plates
                {
                    if (warriors.Peek() > plates.Peek()) // If the current warrior value is more than the current plate value 
                    {
                        warriors.Push(warriors.Pop() - plates.Dequeue()); // Update the warrior value
                    }
                    else if (warriors.Peek() < plates.Peek()) // If the warrior can't destroy the current plate
                    {
                        // Place the current plate at the beginning of the plates so we continue hitting it
                        var updatedPlates = new Queue<int>();  // Create a new updated plates stack
                        updatedPlates.Enqueue(plates.Dequeue() - warriors.Pop()); // Place the current plate value at the beginning of the new update plates so on the next iteration we continue from the same plate 

                        while (plates.Any()) // While there are plates 
                        {
                            updatedPlates.Enqueue(plates.Dequeue()); // Add them to the new updated plates stack
                        }

                        plates = updatedPlates; // Assign the updated plates to the plates stack
                    }
                    else if (warriors.Peek() == plates.Peek()) // If the current warrior value is equal to the current plate 
                    {
                        warriors.Pop(); // Remove the current warrior 
                        plates.Dequeue(); // Remove the current plate
                    }

                    if (!plates.Any()) // If there are no more plates
                    {
                        isDestroyed = true; // Set the flag to true indicating that all plates have been destroyed
                        remainingWarriors = warriors; // Store the remaining warriors in the remainingWarriors stack
                        break; // Break out of the loop
                    }
                }
            }

            // Print
            Console.WriteLine(isDestroyed
                ? $"The orcs successfully destroyed the Gondor's defense.{Environment.NewLine}Orcs left: {string.Join(", ", remainingWarriors)}"
                : $"The people successfully repulsed the orc's attack.{Environment.NewLine}Plates left: {string.Join(", ", plates)}");
        }
    }
}
