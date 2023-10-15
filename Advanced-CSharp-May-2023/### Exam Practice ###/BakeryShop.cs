using System;
using System.Collections.Generic;
using System.Linq;

namespace BakeryShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> bakedProducts = new Dictionary<string, int>
            {
                { "Croissant", 0 },
                { "Muffin", 0 },
                { "Baguette", 0 },
                { "Bagel", 0 }
            };

            var water = new Queue<double>(Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse));

            var flour = new Stack<double>(Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse));

            while (water.Any() && flour.Any())
            {
                double currentWater = water.Dequeue();
                double currentFlour = flour.Pop();
                double currentResources = currentWater + currentFlour;
                double currentWaterPercent = (currentWater * 100) / currentResources;
                double currentFlourPercent = (currentFlour * 100) / currentResources;

                switch (currentWaterPercent)
                {
                    case 50.0 when currentFlourPercent == 50.0:
                        bakedProducts["Croissant"]++;
                        break;
                    case 40.0 when currentFlourPercent == 60.0:
                        bakedProducts["Muffin"]++;
                        break;
                    case 30.0 when currentFlourPercent == 70.0:
                        bakedProducts["Baguette"]++;
                        break;
                    case 20.0 when currentFlourPercent == 80.0:
                        bakedProducts["Bagel"]++;
                        break;
                    default:
                        flour.Push(currentFlour - currentWater);
                        bakedProducts["Croissant"]++;
                        break;
                }
            }

            foreach (var product in bakedProducts
                         .Where(p => p.Value > 0)
                         .OrderByDescending(c => c.Value)
                         .ThenBy(p => p.Key))
            {
                Console.WriteLine($"{product.Key}: {product.Value}");
            }

            Console.WriteLine(!water.Any() ? "Water left: None" : $"Water left: {string.Join(", ", water)}");

            Console.WriteLine(!flour.Any() ? "Flour left: None" : $"Flour left: {string.Join(", ", flour)}");
        }
    }
}
