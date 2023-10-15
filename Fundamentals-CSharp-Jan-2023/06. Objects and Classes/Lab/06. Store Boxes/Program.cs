using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Schema;

namespace _06._Store_Boxes
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<Box> boxes = new List<Box>();
            while ((input = Console.ReadLine()) != "end")
            {
                string[] data = input.Split(" ");
                string boxSerialNumber = data[0];
                string boxItemName = data[1];
                decimal price = decimal.Parse(data[3]);
                int boxItemQuantity = int.Parse(data[2]);

                Item item = new Item(boxItemName, price);
                Box box = new Box(boxSerialNumber, item, boxItemQuantity, price);

                boxes.Add(box);
            }

            foreach (Box box in boxes.OrderByDescending(x => x.TotalPrice))
            {
                Console.WriteLine($"{box.SerialNumber}");
                Console.WriteLine($"-- {box.Item.ItemName} - ${box.Item.ItemPrice:F2}: {box.quantity}");
                Console.WriteLine($"-- ${box.TotalPrice:F2}");
            }
        }
    }

    public class Box
    {
        public string SerialNumber { get; set; }
        public Item Item { get; set; }
        public int quantity { get; set; }
        private decimal PricePerBox { get; set; }
        public decimal TotalPrice { get; set; }

        public Box()
        {
            Item = new Item();
        }

        public Box(string boxSerialNumber, Item item, int itemQuantity, decimal pricePerBox)
        {
            SerialNumber = boxSerialNumber;
            Item = item;
            quantity = itemQuantity;
            PricePerBox = pricePerBox;
            TotalPrice = pricePerBox * itemQuantity;
        }
    }

    public class Item
    {
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }

        public Item()
        {
        }

        public Item(string itemName, decimal itemPrice)
        {
            ItemName = itemName;
            ItemPrice = itemPrice;
        }
    }
}