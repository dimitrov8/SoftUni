using System;
using System.Linq;

namespace ReverseArrayOfStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = Console.ReadLine().Split().ToArray();

            for (int i = text.Length - 1; i >= 0; i--)
            {
                Console.Write(text[i] + " ");
            }
        }
    }
}
