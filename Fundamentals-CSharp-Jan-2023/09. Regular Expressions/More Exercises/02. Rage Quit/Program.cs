using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _02._Rage_Quit
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine().ToUpper();
            string pattern = @"(?<message>[^\d]+)(?<times>[\d]+)";
            MatchCollection matches = Regex.Matches(input, pattern);
            StringBuilder finalMessage = new StringBuilder();
            foreach (Match match in matches)
            {
                StringBuilder sb = new StringBuilder();
                Enumerable.Range(1, int.Parse(match.Groups["times"].Value)).ToList()
                    .ForEach(i => sb.Append(match.Groups["message"].Value));
                finalMessage.Append(sb.ToString());
            }
            Console.WriteLine($"Unique symbols used: {finalMessage.ToString().Distinct().Count()}");
            Console.WriteLine(finalMessage);
        }
    }
}