using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Fashion_Boutique
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> clothesValue = new Stack<int>(Console.ReadLine()
                .Split(' ')
                .Select(int.Parse));
            int rackCapacity = int.Parse(Console.ReadLine());
            int racksUsedCount = 1;
            int sum = 0;
            while (clothesValue.Count > 0) // While the stack is not empty
            {
                if (sum + clothesValue.Peek() < rackCapacity) // If the sum is lower than rackCapacity
                {
                    sum += clothesValue.Pop(); // Keep stacking clothes on the current rack
                }                
                else if (sum + clothesValue.Peek() == rackCapacity) // If the sum is equal to rackCapacity
                {
                    clothesValue.Pop(); // Remove the current clothes from the stack
                    sum = 0; // Reset the sum
                    if (clothesValue.Count > 0) // If the stack is not empty
                    {
                        racksUsedCount++; // Use a new rack
                    }
                }
                else if (sum + clothesValue.Peek() > rackCapacity) // If the sum is greater than rackCapacity
                {
                    sum = 0; // Reset the sum
                    racksUsedCount++; // Use a new rack
                }
            }

            Console.WriteLine(racksUsedCount);
        }
    }
}