using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Periodic_Table
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfInputs = int.Parse(Console.ReadLine());
            HashSet<string> elements = new HashSet<string>();
            for (int i = 0; i < nOfInputs; i++)
            {
                string[] inputElements = Console.ReadLine().Split();
                foreach (var currentElement in inputElements)
                {
                    elements.Add(currentElement);
                }
            }

            Console.WriteLine(string.Join(" ", elements.OrderBy(e => e)));
        }
    }
}