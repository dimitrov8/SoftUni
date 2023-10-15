using System;
using System.Collections.Generic;
using System.Linq;

namespace Computer_Store
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;

            double priceWithoutTax = 0;
            double tax = 0;
            while ((command = Console.ReadLine()) != "special" && command != "regular")
            {
                double price = double.Parse(command);

                int itemCount = 0;
                if (price < 0)
                {
                    Console.WriteLine("Invalid price!");
                    continue;
                }
                priceWithoutTax += price;
                tax += price * 0.2;
            }
            double totalPrice = priceWithoutTax + tax;

            if (totalPrice == 0)
            {
                Console.WriteLine("Invalid order!");
                return;
            }
            
            if (command == "special")
            {
                totalPrice -= totalPrice * 0.10;
            }

            Console.WriteLine("Congratulations you've just bought a new computer!");
            Console.WriteLine($"Price without taxes: {priceWithoutTax:f2}$");
            Console.WriteLine($"Taxes: {tax:f2}$");
            Console.WriteLine("-----------");
            Console.WriteLine($"Total price: {totalPrice:f2}$");
        }
    }
}