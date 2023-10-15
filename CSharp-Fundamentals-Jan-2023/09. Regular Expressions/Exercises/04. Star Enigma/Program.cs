using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _4._Star_Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            int nMessages = int.Parse(Console.ReadLine());
            string pattern = @"[starSTAR]";
            Dictionary<string, string> planetsData = new Dictionary<string, string>();
            for (int i = 1; i <= nMessages; i++)
            {
                string decryptedMessage = string.Empty;
                int count = 0;
                string inputMessage = Console.ReadLine();
                foreach (char letter in inputMessage)
                {
                    Match match = Regex.Match(letter.ToString(), pattern);
                    if (match.Success)
                    {
                        count++;
                    }
                }

                decryptedMessage = inputMessage.Select(letter => letter - count)
                    .Aggregate(decryptedMessage, (current, replace) => current + (char)replace);
                string planetInfoPattern =
                    @"@(?<name>[A-Z][a-z]+)[^@\!:>]*:(?<population>[0-9]+)[^@\!:>]*!(?<type>[AD])![^@\!:>]*->(?<soldiers>[0-9]+)";
                MatchCollection matches = Regex.Matches(decryptedMessage, planetInfoPattern);
                foreach (Match match in matches)
                {
                    planetsData.Add(match.Groups["name"].Value, match.Groups["type"].Value);
                }
            }

            Console.WriteLine($"Attacked planets: {planetsData.Values.Count(x => x == "A")}");
            foreach (string planet in planetsData.Keys.OrderBy(x => x))
            {
                if (planetsData[planet] == "A")
                {
                    Console.WriteLine($"-> {planet}");
                }
            }

            Console.WriteLine($"Destroyed planets: {planetsData.Values.Count(x => x == "D")}");
            foreach (string planet in planetsData.Keys.OrderBy(x => x))
            {
                if (planetsData[planet] == "D")
                {
                    Console.WriteLine($"-> {planet}");
                }
            }
        }
    }
}