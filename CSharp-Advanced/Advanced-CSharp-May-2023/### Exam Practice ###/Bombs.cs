using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombs
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> bombs = new Dictionary<string, int>() // Dictionary to keep track of bombs created
            {
                {"Datura Bombs", 0},
                {"Cherry Bombs", 0},
                {"Smoke Decoy Bombs", 0}
            };

            var bombEffect = new Queue<int>(Console.ReadLine()!  // Read bomb effect values from the console and store them in a queue
                .Split(", ",StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));
            
            var bombCasings = new Stack<int>(Console.ReadLine()! // Read bomb casing values from the console and store them in a stack
                .Split(", ",StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            // Main logic of creating bombs
            while (bombEffect.Any() && bombCasings.Any() && bombs.Values.Any(value => value < 3))
            {
                // Get the next bomb effect and bomb casing values
                int bombEffectValue = bombEffect.Peek();
                int bombCasingValue = bombCasings.Peek();
                int sum = bombEffectValue + bombCasingValue;
                // Check the sum of effect and casing values to determine the bomb type
                switch (sum)
                {
                    case 40:
                        bombEffect.Dequeue();
                        bombCasings.Pop();
                        bombs["Datura Bombs"]++;
                        break;
                    case 60:
                        bombEffect.Dequeue();
                        bombCasings.Pop();
                        bombs["Cherry Bombs"]++;
                        break;
                    case 120:
                        bombEffect.Dequeue();
                        bombCasings.Pop();
                        bombs["Smoke Decoy Bombs"]++;
                        break;
                    default:
                        bombCasingValue -= 5;
                        bombCasings.Pop();
                        bombCasings.Push(bombCasingValue);
                        break;
                }
            }

            // Check if enough materials are available to fill the bomb pouch
            if (bombs.Values.All(value => value != 3))
            {
                Console.WriteLine("You don't have enough materials to fill the bomb pouch.");
            }
            else if (bombs.Values.All(value => value >= 3))
            {
                Console.WriteLine("Bene! You have successfully filled the bomb pouch!");
            }

            // Print the remaining bomb effects
            if (!bombEffect.Any())
            {
                Console.WriteLine("Bomb Effects: empty");
            }
            else if (bombEffect.Any())
            {
                Console.WriteLine($"Bomb Effects: {string.Join(", ", bombEffect)}");
            }
            
            // Print the remaining bomb casings
            if (!bombCasings.Any())
            {
                Console.WriteLine("Bomb Casings: empty");
            }
            else if (bombCasings.Any())
            {
                Console.WriteLine($"Bomb Casings: {string.Join(", ", bombCasings)}");
            }

            // Print the count of each bomb type in the pouch
            foreach (var kvp in bombs.OrderBy(n => n.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}