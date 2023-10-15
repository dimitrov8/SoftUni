using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _02._Race
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> participants = Console.ReadLine().Split(", ").ToList();
            string input;
            string patternLetters = @"[A-Za-z]";
            string patternDigits = @"[1-9]";
            var ranking = new Dictionary<string, double>();
            while ((input = Console.ReadLine()) != "end of race")
            {
                MatchCollection matchesLetters = Regex.Matches(input, patternLetters);
                MatchCollection matchesDigits = Regex.Matches(input, patternDigits);
                string name = String.Empty;
                foreach (Match letter in matchesLetters)
                {
                    name += letter;
                }

                double distance = 0;
                foreach (Match digit in matchesDigits)
                {
                    distance += double.Parse(digit.Value);
                }

                if (participants.Contains(name))
                {
                    if (ranking.ContainsKey(name))
                    {
                        ranking[name] += distance;
                        continue;
                    }

                    ranking.Add(name, distance);
                }
            }

            var sortedRanking = ranking.OrderByDescending(d => d.Value)
                .Take(3).Select(n => n.Key);
            Console.WriteLine($"1st place: {sortedRanking.ElementAt(0)}");
            Console.WriteLine($"2nd place: {sortedRanking.ElementAt(1)}");
            Console.WriteLine($"3rd place: {sortedRanking.ElementAt(2)}");
        }
    }
}