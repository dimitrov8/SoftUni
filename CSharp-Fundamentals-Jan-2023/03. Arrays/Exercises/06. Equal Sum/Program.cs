namespace EqualSum
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr.Length == 1) // No elements to the left or right ==>
                {
                    Console.WriteLine(0); // Print
                    return; // Return
                }

                // Storing leftSum:
                int leftSum = 0; // Every loop reset leftSum
                for (int k = i; k > 0; k--)
                {
                    int nextEl = k - 1; // Take nextEl from left
                    if (k > 0) // If it's positive number 
                    {
                        leftSum += arr[nextEl]; // Add it to the leftSum
                    }
                }

                // Storing rightSum:
                int rightSum = 0; // Every loop reset rightSum
                for (int j = i; j < arr.Length; j++)
                {
                    int nextEl = j + 1; // Take nextEl from right
                    if (arr.Length - 1 > j) // if array > our current[index]
                    {
                        rightSum += arr[nextEl]; // Add it to the rightSum
                    }
                }

                if (leftSum == rightSum) // If both sums are equal
                {
                    Console.WriteLine(i); // Print 
                    return; // Return
                }
            }
            Console.WriteLine("no"); // Else Print
        }
    }
}