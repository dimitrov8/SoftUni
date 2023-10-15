using System;
using System.Linq;

namespace GenericSwapMethodString
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfBoxes = int.Parse(Console.ReadLine()!);
            var stringBox = new Box<string>();
            for (int i = 0; i < nOfBoxes; i++)
            {
                stringBox.Add(Console.ReadLine());
            }

            if (stringBox.Count > 0)
            {
                int[] indexesToSwap = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                stringBox.Swap(indexesToSwap[0], indexesToSwap[1]);
                Console.WriteLine(stringBox);
            }
            
        }
    }
}