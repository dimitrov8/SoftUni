using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _05._Nether_Realms
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(new char[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            var demons = new SortedDictionary<string, Dictionary<int, double>>();
            string healthPattern = @"(?<health>[^\d\+\-*/.])";
            string damagePattern = @"\-?(?<damage>[\d]+\.?[\d]*)";
            for (int i = 0; i < input.Length; i++)
            {
                string name = input[i];
                int health = 0;
                MatchCollection match = Regex.Matches(name, healthPattern);
                foreach (Match @char in match)
                {
                    health += char.Parse(@char.ToString());
                }

                double damage = 0;
                MatchCollection numbers = Regex.Matches(name, damagePattern);
                foreach (Match digit in numbers)
                {
                    damage += double.Parse(digit.ToString());
                }

                foreach (char @char in name)
                {
                    if (@char == '*')
                    {
                        damage *= 2;
                    }

                    if (@char == '/')

                    {
                        damage /= 2;
                    }
                }

                demons.Add(name, new Dictionary<int, double>() { { health, damage } });
            }

            foreach (var demon in demons)
            {
                Console.WriteLine($"{demon.Key} - {demon.Value.Keys.First()} health, {demon.Value.Values.First():F2} damage");
            }
        }
    }
}