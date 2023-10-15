using System;
using System.Linq;

namespace _05._Applied_Arithmetics
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            
            Func<string, int, int> f = (c, n) =>
            {
                switch (c)
                {
                    case "add": n += 1; break;
                    case "multiply": n *= 2; break;
                    case "subtract": n -= 1; break;
                }

                return n;
            };
            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                if (command == "print")
                {
                    Console.WriteLine(string.Join(" ", numbers));
                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = f(command, numbers[i]);
                }
            }
        }
    }
}