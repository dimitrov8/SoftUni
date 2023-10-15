using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Sets_of_Elements
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] hashSetCapacity = Console.ReadLine().Split().Select(int.Parse).ToArray();
            HashSet<int> hSet = new HashSet<int>();
            HashSet<int> hSet2 = new HashSet<int>();

            for (int i = 0; i < hashSetCapacity.Sum(); i++)
            {
                int n = int.Parse(Console.ReadLine());
                if (i < hashSetCapacity[0])
                {
                    hSet.Add(n);
                }
                else
                {
                    hSet2.Add(n);
                }
            }

            hSet.IntersectWith(hSet2);
            Console.WriteLine($"{string.Join(" ", hSet)}");
        }
    }
}