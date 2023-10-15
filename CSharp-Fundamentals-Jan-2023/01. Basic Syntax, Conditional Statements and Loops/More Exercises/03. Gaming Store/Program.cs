namespace Gaming_Store
{
    using System;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            double balance = double.Parse(Console.ReadLine());

            double price = 0;
            double spend = 0;
            string input;
            while ((input = Console.ReadLine()) != "Game Time")
            {
                switch (input)
                {
                    case "OutFall 4": price = 39.99; break;
                    case "CS: OG": price = 15.99; break;
                    case "Zplinter Zell": price = 19.99; break;
                    case "Honored 2": price = 59.99; break;
                    case "RoverWatch": price = 29.99; break;
                    case "RoverWatch Origins Edition": price = 39.99; break;
                    default:
                        Console.WriteLine("Not Found"); continue;
                }
                if (balance >= price)
                {
                    spend += price;
                    balance -= price;
                    Console.WriteLine($"Bought {input}");
                    if (balance <= 0)
                    {
                        Console.WriteLine("Out of money!");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Too Expensive");
                    continue;
                }
            }
            Console.WriteLine($"Total spent: ${spend:f2}. Remaining: ${balance:f2}");
        }
    }
}