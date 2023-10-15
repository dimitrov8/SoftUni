using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace _06._Replace_Repeating_Chars
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                // If this is the first character or if it is different from the previous character
                if (i == 0 || input[i] != input[i - 1])  
                {
                    // If it is the first character
                    // Or different from the previous character, append it to the output string
                    output.Append(input[i]);
                }
            }

            Console.WriteLine(output);
        }
    }
}