using System;
using System.Linq;

namespace _03._Custom_Min_Function
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int[], int> minFunc = numbers =>
            {
                int minValue = int.MaxValue;
                foreach (var number in numbers)
                {
                    if (number < minValue)
                    {
                        minValue = number;
                    }
                }

                return minValue;
            };
            Console.WriteLine(minFunc(
                Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray()));
        }
    }
}