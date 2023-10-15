using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Basic_Queue_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] tokens = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            Queue<int> queue = new Queue<int>(Console.ReadLine()
                .Split()
                .Select(int.Parse));

            for (int i = 0; i < tokens[1]; i++)
            {
                queue.Dequeue();
            }

            if (queue.Any(x => x == tokens[2]))
            {
                Console.WriteLine("true");
            }
            else if (queue.Count == 0)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(queue.Min());
            }
        }
    }
}