using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Shopping_Spree
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();

            string[] peopleData = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            foreach (string person in peopleData)
            {
                string[] personInfo = person.Split("=");
                string name = personInfo[0];
                int money = int.Parse(personInfo[1]);
                
                people.Add(new Person(name, money));
            }

            string[] prodcutsData = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (string product in prodcutsData)
            {
                string[] productInfo = product.Split("=");
                string name = productInfo[0];
                int cost = int.Parse(productInfo[1]);
                products.Add(new Product(name , cost));
            }

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] purchaseInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string personName = purchaseInfo[0];
                string productName = purchaseInfo[1];
                
                Person person = people.Find(p=> p.Name == personName);
                Product product = products.Find(p => p.Name == productName);
                
                person.BuyProducts(product);
            }

            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }
            
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public int Cost { get; set; }

        public Product(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Money { get; set; }

        public List<Product> Bag { get; set; }

        public Person(string name, int money)
        {
            Name = name;
            Money = money;
            Bag = new List<Product>();
        }

        public void BuyProducts(Product product)
        {
            if (Money >= product.Cost)
            {
                Bag.Add(product);
                Money -= product.Cost;
                Console.WriteLine($"{Name} bought {product.Name}");
            }
            else
            {
                Console.WriteLine($"{Name} can't afford {product.Name}");
            }
        }

        public override string ToString()
        {
            if (Bag.Any())
            {
                return $"{Name} - {string.Join(", ", Bag.Select(p => p.Name))}";
            }

            return $"{Name} - Nothing bought";
        }
    }
}