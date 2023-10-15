using System;
using System.Text.RegularExpressions;

namespace _01._Match_Full_Name
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex(@"\b[A-Z][a-z]+ [A-Z][a-z]+\b");
            string input = Console.ReadLine();
            MatchCollection matches = regex.Matches(input);

            Console.WriteLine(string.Join(" ", matches));
        }
    }
}