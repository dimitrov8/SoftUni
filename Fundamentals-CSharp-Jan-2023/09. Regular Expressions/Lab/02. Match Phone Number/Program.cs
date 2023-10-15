using System;
using System.Text.RegularExpressions;

namespace _02._Match_Phone_Number
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex(@"\+\b359([-| ])2\1\d{3}\1\d{4}\b");
            string input = Console.ReadLine();
            
            MatchCollection matches = regex.Matches(input);
            Console.WriteLine(string.Join(", ", matches));

        }
    }
}