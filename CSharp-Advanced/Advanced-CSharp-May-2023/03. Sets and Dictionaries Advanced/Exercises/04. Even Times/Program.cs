using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Even_Times
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfInputs = int.Parse(Console.ReadLine());
            var valuesCount = new Dictionary<int, int>();
            for (int i = 0; i < nOfInputs; i++)
            {
                int currentN = int.Parse(Console.ReadLine());
                if (!valuesCount.ContainsKey(currentN))
                {
                    valuesCount.Add(currentN, 0);
                }
                valuesCount[currentN]++;
            }
            Console.WriteLine(valuesCount.Single(n => n.Value % 2 == 0).Key);
        }
    }
}