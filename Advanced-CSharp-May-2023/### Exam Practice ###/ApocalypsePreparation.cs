using System;
using System.Collections.Generic;
using System.Linq;

namespace ApocalypsePreparation
{
    class Program
    {
        static void Main(string[] args)
        {
            var healingItems = new Dictionary<string, int>
            {
                { "Patch", 0 },
                { "Bandage", 0 },
                { "MedKit", 0 }
            };

            var textiles = new Queue<int>(Console.ReadLine()!
                .Split()
                .Select(int.Parse));
            var medicaments = new Stack<int>(Console.ReadLine()!
                .Split()
                .Select(int.Parse));

            while (true)
            {
                int sum = textiles.Peek() + medicaments.Peek();

                switch (sum)
                {
                    case 30:
                        healingItems["Patch"]++;
                        textiles.Dequeue();
                        medicaments.Pop();
                        break;
                    case 40:
                        healingItems["Bandage"]++;
                        textiles.Dequeue();
                        medicaments.Pop();
                        break;
                    case 100:
                        healingItems["MedKit"]++;
                        textiles.Dequeue();
                        medicaments.Pop();
                        break;
                    default:
                        if (sum > 100)
                        {
                            healingItems["MedKit"]++;
                            textiles.Dequeue();
                            sum -= 100;
                            medicaments.Pop();
                            medicaments.Push(medicaments.Pop() + sum);
                        }
                        else
                        {
                            textiles.Dequeue();
                            medicaments.Push(medicaments.Pop() + 10);
                        }
                        break;
                }

                if (!textiles.Any() || !medicaments.Any())
                {
                    break;
                }
            }

            if (!textiles.Any() && !medicaments.Any())
            {
                Console.WriteLine("Textiles and medicaments are both empty.");
            }
            else if (!textiles.Any())
            {
                Console.WriteLine("Textiles are empty.");
            }
            else if (!medicaments.Any())
            {
                Console.WriteLine("Medicaments are empty.");
            }

            foreach (var item in healingItems
                         .Where(i => i.Value > 0)
                         .OrderByDescending(i => i.Value)
                         .ThenBy(i => i.Key))
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            if (textiles.Any())
            {
                Console.WriteLine($"Textiles left: {string.Join(", ", textiles)}");
            }

            else if (medicaments.Any())
            {
                Console.WriteLine($"Medicaments left: {string.Join(", ", medicaments)}");
            }
        }
    }
}
