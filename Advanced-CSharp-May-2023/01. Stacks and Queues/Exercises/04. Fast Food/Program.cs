using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Fast_Food
{
    class Program
    {
        static void Main(string[] args)
        {
            int foodQuantity = int.Parse(Console.ReadLine());
            Queue<int> ordersIDs = new Queue<int>(Console.ReadLine()
                .Split(' ')
                .Select(int.Parse));

            Console.WriteLine(ordersIDs.Max());
            while (ordersIDs.Count > 0)
            {
                int currentOrderID = ordersIDs.Peek();
                if (foodQuantity >= currentOrderID)
                {
                    foodQuantity -= currentOrderID;
                    ordersIDs.Dequeue();
                    continue;
                }

                break;
            }

            if (ordersIDs.Count == 0)
            {
                Console.WriteLine("Orders complete");
            }
            else if (ordersIDs.Count > 0)
            {
                Console.Write("Orders left: ");
                while (ordersIDs.Count > 0)
                {
                    Console.Write($"{ordersIDs.Dequeue()}");
                    if (ordersIDs.Count > 0)
                    {
                        Console.Write(' ');
                    }
                }
            }
        }
    }
}