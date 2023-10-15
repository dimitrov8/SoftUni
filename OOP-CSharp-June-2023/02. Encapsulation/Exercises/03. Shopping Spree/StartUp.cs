using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();

            string[] peopleInfo = Console.ReadLine()!.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var currentPerson in peopleInfo)
            {
                string[] tokens = currentPerson.Split('=', StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                decimal money = decimal.Parse(tokens[1]);

                try
                {
                    Person person = new Person(name, money);
                    if (people.All(n => n.Name != name))
                        people.Add(person);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            string[] productInfo = Console.ReadLine()!.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var currentProduct in productInfo)
            {
                string[] tokens = currentProduct.Split('=', StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                decimal cost = decimal.Parse(tokens[1]);

                try
                {
                    Product product = new Product(name, cost);
                    if (products.All(n => n.Name != name))
                        products.Add(product);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command!.Split();
                Person person = people.First(n => n.Name == tokens[0]);
                Product product = products.First(n => n.Name == tokens[1]);
                person.AddProduct(product);
            }

            foreach (var person in people)
            {
                Console.WriteLine(person.Products.Count == 0 
                ? $"{person.Name} - Nothing bought"
                : $"{person.Name} - {string.Join(", ", person.Products.Select(p => p.Name))}");
            }
        }
    }
}