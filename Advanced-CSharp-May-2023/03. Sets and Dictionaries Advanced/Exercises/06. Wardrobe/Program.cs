using System;
using System.Collections.Generic;

namespace _06._Wardrobe
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfInputs = int.Parse(Console.ReadLine());
            var wardrobe = new Dictionary<string, Dictionary<string, int>>();
            for (int i = 0; i < nOfInputs; i++)
            {
                string[] input = Console.ReadLine().Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                string[] clothes = input[1].Split(',');
                string color = input[0];
                
                if (!wardrobe.ContainsKey(color)) // If the color doesn't exist
                {
                    wardrobe.Add(color, new Dictionary<string, int>());
                }
                
                foreach (var currCloth in clothes)
                {
                    // If the color exists but the dress doesn't
                    if (!wardrobe[color].ContainsKey(currCloth))
                    {
                        wardrobe[color].Add(currCloth, 1);
                    }
                    else if (wardrobe[color].ContainsKey(currCloth))
                    {
                        wardrobe[color][currCloth]++;
                    }
                }
            }
            string[] dressToFind = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string desiredColor = dressToFind[0];
            string clothToFind = dressToFind[1];

            foreach (var color in wardrobe.Keys)
            {
                Console.WriteLine($"{color} clothes:");
                foreach (var dress in wardrobe[color])
                {
                    if (color == desiredColor && dress.Key == clothToFind)
                    {
                        Console.WriteLine($"* {dress.Key} - {dress.Value} (found!)");
                    }
                    else
                    {
                        Console.WriteLine($"* {dress.Key} - {dress.Value}");
                    }
                }
            }
        }
    }
}