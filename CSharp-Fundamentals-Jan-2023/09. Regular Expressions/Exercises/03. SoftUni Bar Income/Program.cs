using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _3._SoftUni_Bar_Income
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern =
                @"\%(?<customer>[A-Z][a-z]+)%[^|$%.]*?<(?<product>\w+)>[^|$%.]*?\|(?<quantity>\d+)\|[^|$%.]*?(?<price>[0-9]+(\.[0-9]+)?)\$";
            string input;
            double totalSum = 0;
            while ((input = Console.ReadLine()) != "end of shift")
            {
                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    string customer = match.Groups["customer"].Value;
                    string product = match.Groups["product"].Value;
                    int quantity = int.Parse(match.Groups["quantity"].Value);
                    double price = double.Parse(match.Groups["price"].Value);

                    totalSum += price * quantity;
                    Console.WriteLine($"{customer}: {product} - {quantity * price:F2}");
                }
            }

            Console.WriteLine($"Total income: {totalSum:F2}");
        }
    }
}