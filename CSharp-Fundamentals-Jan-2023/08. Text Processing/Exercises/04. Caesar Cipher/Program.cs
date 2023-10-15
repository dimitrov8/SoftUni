using System;
using System.Linq;
using System.Text;

namespace _04._Caesar_Cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            StringBuilder output = new StringBuilder();
            foreach (char @char in input)
            {
                int shiftedChar = @char + 3;
                output.Append((char)shiftedChar);
            }

            Console.WriteLine(output);
        }
    }
}