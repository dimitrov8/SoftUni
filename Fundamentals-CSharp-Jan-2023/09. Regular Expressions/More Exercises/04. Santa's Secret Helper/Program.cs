using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _4._Santas_Secret_Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            int decryptKey = int.Parse(Console.ReadLine());
            StringBuilder decryptedMessage = new StringBuilder();
            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                string encryptedMessage = command;
                foreach (var @char in encryptedMessage)
                {
                    char newChar = (char)(@char - decryptKey);
                    decryptedMessage.Append(string.Join("", newChar));
                }
            }
            string pattern = @"@(?<name>[A-Za-z]+)[^@\-!:>]+[!](?<type>[G])[!]";
            MatchCollection matches = Regex.Matches(decryptedMessage.ToString(), pattern);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Groups["name"]);
            }
        }
    }
}