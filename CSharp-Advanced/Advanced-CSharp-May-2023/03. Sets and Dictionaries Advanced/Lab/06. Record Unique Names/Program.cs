using System;
using System.Collections.Generic;

namespace _6._Record_Unique_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            int nInputs = int.Parse(Console.ReadLine());
            HashSet<string> names = new HashSet<string>();

            for (int i = 0; i < nInputs; i++)
            {
                string inputName = Console.ReadLine();
                names.Add(inputName);
            }

            foreach (var uniqueName in names)
            {
                Console.WriteLine(uniqueName);
            }
        }
    }
}