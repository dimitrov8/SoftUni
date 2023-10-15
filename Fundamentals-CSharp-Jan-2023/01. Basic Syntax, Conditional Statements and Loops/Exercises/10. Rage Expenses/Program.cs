namespace Rage_Expenses
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    class Program
    {
        static void Main(string[] args)
        {
            int lostGamesCount = int.Parse(Console.ReadLine());
            double headsetPrice = double.Parse(Console.ReadLine());
            double mousePrice = double.Parse(Console.ReadLine());
            double keyboardPrice = double.Parse(Console.ReadLine());
            double displayPrice = double.Parse(Console.ReadLine());

            int count = 0;
            double price = 0;

            for (int i = 1; i <= lostGamesCount; i++)
            {
                bool hsTrashed = false;
                bool mouseTrashed = false;
                if (i % 2 == 0) // Every second game
                {
                    hsTrashed = true; // Headset is trashed
                    price += headsetPrice;
                }
                if (i % 3 == 0) // Every third game
                {
                    mouseTrashed = true; // Mouse is trashed
                    price += mousePrice;

                    // If both are trashed
                    // In the same game 
                    if (hsTrashed == true && mouseTrashed == true)  
                    {
                        // Counter how many times both headset and mouse are trashed
                        count++; 
                        price += keyboardPrice;

                        // Every second time when keyboard is trashed (using the counter)
                        // Trash Display 
                        if (count % 2 == 0) 
                        {
                            price += displayPrice;
                        }

                    }
                }
            }
            Console.WriteLine($"Rage expenses: {price:f2} lv.");
        }
    }
}

