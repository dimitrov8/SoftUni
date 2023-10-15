using System;
using System.Linq;

namespace _09._Palindrome_Integers
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            while ((command = Console.ReadLine()) != "END" && int.Parse(command) >= 0)
            {
                int number = int.Parse(command);
                int reversedN = int.Parse(ReversedNumber(number));

                Console.WriteLine(CheckIfIntegersArePalindrome(number, reversedN).ToString().ToLower());
            }
        }

        static string ReversedNumber(int a)
        {
            char[] reversedNArray = a.ToString().ToArray();
            Array.Reverse(reversedNArray);
            return new string(reversedNArray);
        }

        static bool CheckIfIntegersArePalindrome(int a, int b)
        {
            if (a == b)
            {
                return true;
            }

            return false;
        }
    }
}