using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            var dict = new Dictionary<string, Item>();
            while ((input = Console.ReadLine()) != "buy")
            {
                string[] productData = input.Split(' ');
                string productName = productData[0];
                double price = double.Parse(productData[1]);
                int quantity = int.Parse(productData[2]);

                if (!dict.ContainsKey(productName))
                {
                    Item item = new Item(price, quantity);
                    dict.Add(productName, item);
                }
                else
                {
                    dict[productName].UpdateQuantityAndPrice(price, quantity);
                }
            }

            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key} -> {item.Value.GetTotalPrice():F2}");
            }
        }
    }

    public class Item
    {
        private double Price;
        private double Quantity;
        public Item(double price, double quantity)
        {
            Price = price;
            Quantity = quantity;
        }

        public void UpdateQuantityAndPrice(double price, double newQuantity)
        {
            Price = price;
            Quantity += newQuantity;
        }

        public double GetTotalPrice() => Price * Quantity;
    }
}