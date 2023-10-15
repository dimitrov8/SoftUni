using System;
using System.Linq;

namespace GenericSwapMethodIntegers
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfBoxes = int.Parse(Console.ReadLine()!);
            var integerBox = new Box<int>();
            for (int i = 0; i < nOfBoxes; i++)
            {
                integerBox.Add(int.Parse(Console.ReadLine()!));
            }

            if (integerBox.Count > 0)
            {
                int[] indexesToSwap = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                integerBox.Swap(indexesToSwap[0], indexesToSwap[1]);
                Console.WriteLine(integerBox);
            }
        }
    }
}