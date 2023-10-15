using System;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace _03._Characters_in_Range
{
    class Program
    {
        static void Main(string[] args)
        {
            char start = char.Parse(Console.ReadLine()); // 97
            char end = char.Parse(Console.ReadLine()); // 

            GetCharsInRange(start, end);
        }
        static void GetCharsInRange(char start, char end)
        {
            int startChar = Math.Min(start, end);
            int endChar = Math.Max(start, end);

            for (int i = startChar + 1; i < endChar; i++)
            {
                Console.Write((char)i + " ");
            }
        }
    }
}