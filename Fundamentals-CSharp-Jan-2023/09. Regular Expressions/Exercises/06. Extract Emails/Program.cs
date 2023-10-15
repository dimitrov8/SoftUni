using System;
using System.Text.RegularExpressions;

namespace _06._Extract_Emails
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern =
                @"(?<user>\s[a-zA-Z0-9]+[\._\-]*[a-z]*)[@](?<domain>[a-zA-Z]+[\-]?[a-zA-Z]+[.][a-zA-Z]+[.]?[a-zA-Z]+)";
            string input = Console.ReadLine();

            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.ToString().Trim());
            }
        }
    }
}

