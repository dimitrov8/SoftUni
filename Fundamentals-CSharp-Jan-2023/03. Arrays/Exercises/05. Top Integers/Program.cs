namespace TopIntegers
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            // Find all top integers in an array 
            // A top integer is an integer that is > than all elements to its right

            int[] arr = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                bool isTopInt = true;
                int currentNum = arr[i];

                for (int j = i + 1; j < arr.Length; j++)
                {
                    int nextNum = arr[j];

                    if (nextNum >= currentNum)
                    {
                        isTopInt = false;
                    }
                }
                    if (isTopInt)
                    {
                        Console.Write($"{currentNum + " "}");
                    }
                }
            }
        }
    }