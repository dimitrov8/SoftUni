using System;
using System.Collections.Generic;
using System.Linq;

namespace Cooking
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new Dictionary<string, int>
            {
                { "Bread", 0 },
                { "Cake", 0 },
                { "Pastry", 0 },
                { "Fruit Pie", 0 }
            };

            var liquids = new Queue<int>(Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            var ingredients = new Stack<int>(Console.ReadLine()!
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));

            while (true)
            {
                int currentLiquid = liquids.Peek();
                int currentIngredient = ingredients.Peek();
                int currentSum = currentLiquid + currentIngredient;

                switch (currentSum)
                {
                    case 25:
                        items["Bread"]++;
                        liquids.Dequeue();
                        ingredients.Pop();
                        break;
                    case 50:
                        items["Cake"]++;
                        liquids.Dequeue();
                        ingredients.Pop();
                        break;
                    case 75:
                        items["Pastry"]++;
                        liquids.Dequeue();
                        ingredients.Pop();
                        break;
                    case 100:
                        items["Fruit Pie"]++;
                        liquids.Dequeue();
                        ingredients.Pop();
                        break;
                    default:
                        liquids.Dequeue();
                        ingredients.Pop();
                        ingredients.Push(currentIngredient + 3);
                        break;
                }

                if (!liquids.Any() || !ingredients.Any())
                {
                    break;
                }
            }

            Console.WriteLine(items.All(c => c.Value > 0)
                ? "Wohoo! You succeeded in cooking all the food!"
                : "Ugh, what a pity! You didn't have enough materials to cook everything.");


            Console.WriteLine(liquids.Any()
                ? $"Liquids left: {string.Join(", ", liquids)}"
                : "Liquids left: none");
            Console.WriteLine(ingredients.Any()
                ? $"Ingredients left: {string.Join(", ", ingredients)}"
                : "Ingredients left: none");

            foreach (var product in items.OrderBy(p => p.Key))
            {
                Console.WriteLine($"{product.Key}: {product.Value}");
            }
        }
    }
}