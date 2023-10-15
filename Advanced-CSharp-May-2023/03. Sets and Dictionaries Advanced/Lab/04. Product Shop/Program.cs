using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Product_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            var dict = new Dictionary<string, Dictionary<string, double>>();
            while ((input = Console.ReadLine()) != "Revision")
            {
                string[] tokens = input.Split(", ");
                string shopName = tokens[0];
                string product = tokens[1];
                double price = double.Parse(tokens[2]);

                if (!dict.ContainsKey(shopName))
                {
                    dict.Add(shopName, new Dictionary<string, double>() { { product, price } });
                }
                else if (dict.ContainsKey(shopName))
                {
                    dict[shopName].Add(product, price);
                }
            }

            foreach (var shop in dict.Keys.OrderBy(n => n))
            {
                Console.WriteLine($"{shop}->");
                foreach (var productData in dict[shop])
                {
                        Console.WriteLine($"Product: {productData.Key}, Price: {productData.Value}");
                }
            }
        }
    }
}