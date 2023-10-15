using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;

namespace _05._Dragon_Army
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfDragons = int.Parse(Console.ReadLine());
            Dictionary<string, SortedDictionary<string, Dragon>> dragonsDict = new Dictionary<string, SortedDictionary<string, Dragon>>();
            int value = 0;
            for (int i = 1; i <= nOfDragons; i++)
            {
                string[] dragonArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string type = dragonArgs[0];
                string name = dragonArgs[1];
                int damage = int.TryParse(dragonArgs[2], out value) ? value : 45;
                int health = int.TryParse(dragonArgs[3], out value) ? value : 250;
                int armor = int.TryParse(dragonArgs[4], out value) ? value : 10;

                if (dragonsDict.ContainsKey(type) && dragonsDict[type].ContainsKey(name))
                {
                    dragonsDict[type][name].Damage = damage;
                    dragonsDict[type][name].Health = health;
                    dragonsDict[type][name].Armor = armor;
                    continue;
                }

                if (!dragonsDict.ContainsKey(type))
                {
                    dragonsDict[type] = new SortedDictionary<string, Dragon>();
                }

                dragonsDict[type][name] = new Dragon(type, name, damage, health, armor);
            }

            foreach (var kvp in dragonsDict)
            {
                string type = kvp.Key;
                SortedDictionary<string, Dragon> dragons = kvp.Value;
                double totalDamage = 0;
                double totalHealth = 0;
                double totalArmor = 0;

                foreach (var dragon in dragons.Values)
                {
                    totalDamage += dragon.Damage;
                    totalHealth += dragon.Health;
                    totalArmor += dragon.Armor;
                }

                double avgDamage = totalDamage / dragons.Count;
                double avgHealth = totalHealth / dragons.Count;
                double avgArmor = totalArmor / dragons.Count;
                Console.WriteLine($"{type}::({avgDamage:F2}/{avgHealth:F2}/{avgArmor:F2})");

                foreach (var dragon in dragons.Values)
                {
                    Console.WriteLine(dragon);
                }
            }
        }
    }

    public class Dragon
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }

        public Dragon(string type, string name, int damage, int health, int armor)
        {
            Name = name;
            Type = type;
            Damage = damage;
            Health = health;
            Armor = armor;
        }

        public override string ToString()
        {
            return $"-{Name} -> damage: {Damage}, health: {Health}, armor: {Armor}";
        }
    }
}