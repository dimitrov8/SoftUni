using System;
using System.Collections.Generic;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.Products = new List<Product>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");
                this.name = value;
            }
        }

        public decimal Money
        {
            get => this.money;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Money cannot be negative");
                this.money = value;
            }
        }
        public List<Product> Products { get; set; }

        public void AddProduct(Product product)
        {
            if (this.money >= product.Cost)
            {
                this.money -= product.Cost;
                this.Products.Add(product);
                Console.WriteLine($"{this.name} bought {product.Name}");
                return;
            }

            Console.WriteLine($"{this.name} can't afford {product.Name}");
        }
    }
}