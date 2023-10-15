using System;

namespace _01._Reverse_Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            string reversed = String.Empty;
            while ((input = Console.ReadLine()) != "end")
            {
                char[] charArr = input.ToCharArray();
                Array.Reverse(charArr);
                string reversedInput = new string(charArr);
                Console.WriteLine($"{input} = {reversedInput}");
            }
        }
    }
}