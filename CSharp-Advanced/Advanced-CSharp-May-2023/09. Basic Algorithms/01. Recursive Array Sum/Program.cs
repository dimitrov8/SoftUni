using System;
using System.Linq;

namespace RecursiveArraySum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine()?.Split().Select(int.Parse).ToArray();
            Console.WriteLine(SumArray(array, 0));
        }
        static int SumArray(int[] array, int index)
        {
            if (index == array.Length - 1)
            {
                return array[index];
            }

            return array[index] + SumArray(array, index + 1);
        }
    }
}