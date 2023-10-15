using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Remove_Negatives_and_Reverse
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            numbers.RemoveAll(x => x < 0);
            
            if (numbers.Count == 0)
            {
                Console.WriteLine("empty");
                return;
            }
            
            numbers.Reverse();
            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}