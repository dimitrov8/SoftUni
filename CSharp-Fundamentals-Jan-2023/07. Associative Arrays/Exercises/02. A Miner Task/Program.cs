using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace _02._A_Miner_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Dictionary<string, int> resources = new Dictionary<string, int>();
            while ((input = Console.ReadLine()) != "stop")
            {
                int quantity = int.Parse(Console.ReadLine());
                if (resources.ContainsKey(input))
                {
                    resources[input] += quantity;
                    continue;
                }
                resources.Add(input, quantity);
            }
            
            foreach (var resource in resources)
                Console.WriteLine($"{resource.Key} -> {resource.Value}");
        }
    }
}