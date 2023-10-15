using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Cards_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> p1List = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            List<int> p2List = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            while (p1List.Count != 0 && p2List.Count != 0)
            {
                if (p1List[0] > p2List[0])
                {
                    p1List.Add(p1List[0]);
                    p1List.Add(p2List[0]);
                    p1List.RemoveAt(0);
                    p2List.RemoveAt(0);
                }

                else if (p2List[0] > p1List[0])
                {
                    p2List.Add(p2List[0]);
                    p2List.Add(p1List[0]);
                    p2List.RemoveAt(0);
                    p1List.RemoveAt(0);
                }

                else if (p1List[0] == p2List[0])
                {
                    p1List.RemoveAt(0);
                    p2List.RemoveAt(0);
                }
            }

            if (p1List.Count > p2List.Count)
            {
                Console.WriteLine($"First player wins! Sum: {p1List.Sum()}");
            }
            else if (p2List.Count > p1List.Count)
            {
                Console.WriteLine($"Second player wins! Sum: {p2List.Sum()}");
            }
        }
    }
}