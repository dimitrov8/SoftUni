namespace Magic_Sum
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

            int sum = int.Parse(Console.ReadLine());

            for (int i = 0; i < arr.Length; i++)
            {
                int currN = arr[i];

                for (int j = i + 1; j < arr.Length; j++)
                {
                    int nextN = arr[j];

                    if (currN + nextN == sum)
                    {
                        Console.WriteLine($"{currN} {nextN}");
                    }
                }
            }
        }
    }
}