namespace Vending_Machine
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Recieve coins until "Start"
            string input;
            double balance = 0;
            while ((input = Console.ReadLine()) != "Start")
            {
                double coins = double.Parse(input);

                // -> Only valid coins should accumulated: 0.1, 0.2, 0.5, 1, 2
                if (coins == 0.1 || coins == 0.2 || coins == 0.5 || coins == 1 || coins == 2)
                {

                    balance += coins;
                }
                // --> If invalid coin is inserted print "Cannot Accept {coins}" and continue to the next line
                else
                    Console.WriteLine($"Cannot accept {coins}");
                continue;
            }
            // 2.Receive products until "End"
            string product;
            while ((product = Console.ReadLine()) != "End")
            {
                double price = 0;
                // -> Valid products are: Nuts - 2; Water - 0.7; Crisps - 1.5; Soda - 0.8; Coke - 1.0
                if (product == "Nuts" || product == "Water" || product == "Crisps" || product == "Soda" || product == "Coke")
                {
                    // Prices 
                    switch (product)
                    {
                        case "Nuts": price = 2; break;
                        case "Water": price = 0.7; break;
                        case "Crisps": price = 1.5; break;
                        case "Soda": price = 0.8; break;
                        case "Coke": price = 1; break;
                    }
                }
                // --> If invalid product is recieved print "Invalid product"
                else
                {
                    Console.WriteLine("Invalid product");
                    continue;
                }
                //  2.1 If coins are enough to buy selected product, print "Purchased {product name}",
                if (balance >= price)
                {
                    Console.WriteLine($"Purchased {product.ToLower()}");
                    balance -= price;
                }
                // - > If coins are not enough print: "Sorry, not enough money" and continue to the next line
                else
                {
                    Console.WriteLine("Sorry, not enough money");
                    continue;
                }
            }
            // 3. When the "End" command is given print the reminding balance, formatted to the second decimal point: "Change: {money left}.
            Console.WriteLine($"Change: {balance:f2}");
        }
    }
}
        
       