using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _01._Furniture
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"^[>]{2}(?<item>[A-Za-z]+)[<]{2}(?<price>\d+\.\d+|\d+)!(?<quantity>\d+)";
            List<string> validItems = new List<string>();
            string input;
            double totalSum = 0;
            while ((input = Console.ReadLine()) != "Purchase")
            {
                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    validItems.Add(match.Groups["item"].Value);
                    totalSum += int.Parse(match.Groups["quantity"].Value) * double.Parse(match.Groups["price"].Value);
                }
            }

            Console.WriteLine("Bought furniture:");
            foreach (var itemName in validItems)
            {
                Console.WriteLine(itemName);
            }

            Console.WriteLine($"Total money spend: {totalSum:F2}");
            
        }
    }
}