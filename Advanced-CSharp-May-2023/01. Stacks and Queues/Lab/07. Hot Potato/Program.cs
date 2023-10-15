using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Hot_Potato
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> names = new Queue<string>(Console.ReadLine().Split(' '));
            int eliminationNumber = int.Parse(Console.ReadLine());
            int nCount = 1;

            while (names.Count > 1)
            {
                if (nCount == eliminationNumber)
                {
                    Console.WriteLine($"Removed {names.Dequeue()}");
                    nCount = 1;
                    continue;
                }

                names.Enqueue(names.Dequeue());
                nCount++;
            }
            Console.WriteLine($"Last is {names.Dequeue()}");
        }
    }
}