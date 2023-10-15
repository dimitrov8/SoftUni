namespace Max_Sequence_of_Equal_Elements
{
    using System;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int bestCount = 0;
            int currCount = 1;
            int n = 0;

            for (int i = arr.Length - 1; i > 0; i--)
            {
                int currN = arr[i];
                int nextN = arr[i - 1];

                if (currN == nextN)
                {
                    currCount++;

                    if (currCount >= bestCount)
                    {
                        bestCount = currCount;
                        n = currN;
                    }
                }
                else
                {
                    currCount = 1;
                }
            }
            Console.WriteLine(string.Join(" ", Enumerable.Repeat(n, bestCount)));
        }
    }

}