using System;
using System.Collections.Generic;
using System.Linq;

namespace _12._Cups_and_Bottles
{
    class Program
    {
        private static int _currentBottle;

        static void Main(string[] args)
        {
            Queue<int> cups = new Queue<int>(Console.ReadLine() // FIFO (FIRST IN FIRST OUT)
                .Split()
                .Select(int.Parse));
            Stack<int> bottles = new Stack<int>(Console.ReadLine() // LIFO (LAST IN LAST OUT)
                .Split()
                .Select(int.Parse));

            int wastedLitters = 0; // Variable to hold the wasted water amount
            while (bottles.Any() && cups.Any()) // While there are bottles and cups to fill
            {
                int currentCup = cups.Peek(); // Take the current cup
                int currentBottle = bottles.Peek(); // Take the current bottle

                if (currentCup <= currentBottle) // If the current bottle has more water than the cup capacity
                {
                    wastedLitters += currentBottle - currentCup; // We fill the cup fully and the excessive water becomes wasted
                    cups.Dequeue(); // Remove the current cup, because we filled it successfully
                    bottles.Pop(); // Remove the current bottle, because we used it
                }
                else if (currentCup > currentBottle) // If the current cup capacity is more than the water in the current bottle 
                {
                    while (currentCup > 0) // While the current cup is not filled completely
                    {
                        currentCup -= bottles.Pop(); // Fill the current cup with the next available bottle
                        if (currentCup < 0) // If any bottle we used is above the current capacity
                        {
                            wastedLitters += Math.Abs(currentCup); // The excessive water becomes wasted
                        }
                    }
                    cups.Dequeue(); // When the cup is filled completely then we remove it
                }
            }

            if (bottles.Any()) // If there are any bottles left
            {
                Console.Write("Bottles: "); // Print
                while (bottles.Count > 0) // While there are bottles in the stack
                {
                    Console.Write($"{bottles.Pop()} "); // Print the bottle capacity
                }
            }
            else if (cups.Any()) // Else if there are any cups left 
            {
                Console.Write("Cups: "); // Print
                while (cups.Count > 0) // While there are cups left in the queue
                {
                    Console.Write($"{cups.Dequeue()} "); // Print the cup capacity
                }
            }
            Console.WriteLine(); // Go on a new line
            Console.WriteLine($"Wasted litters of water: {wastedLitters}"); // Print the wasted litters of water
        }
    }
}