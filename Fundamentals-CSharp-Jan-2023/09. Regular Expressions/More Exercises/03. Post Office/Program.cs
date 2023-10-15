using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _03._Post_Office
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputArgs = Console.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);
            string firstPart = inputArgs[0];
            string secondPart = inputArgs[1];
            string thirdPart = inputArgs[2];
            string firstPattern = @"([#$%*&])[A-Z]+\1";

            // Match first pattern
            Match capitalLettersMatch = Regex.Match(firstPart, firstPattern);
            string capitalLetters = capitalLettersMatch.ToString().Substring(1, capitalLettersMatch.Length - 2);
            string secondPattern = @"\d+[:]\d{2}";
            // Match second pattern
            MatchCollection asciiCodeAndLength = Regex.Matches(secondPart, secondPattern);
            // Dict to store the code and length
            Dictionary<string, int> asciiCodeAndLengthDict = new Dictionary<string, int>();
            foreach (Match match in asciiCodeAndLength) // Add each match to the dictionary
            {
                string[] matchArgs = match.ToString().Split(':'); // Split
                string letter = Convert.ToChar(int.Parse(matchArgs[0])).ToString(); // Letter
                int length = int.Parse(matchArgs[1]); // Length
                if (!asciiCodeAndLengthDict.ContainsKey(letter) &&
                    capitalLetters.Contains(letter)) // If the ASCII code is not in the dictionary
                {
                    asciiCodeAndLengthDict.Add(letter, length); // Add the code and length
                }
            }

            string[] words = thirdPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var @char in capitalLetters)
            {
                string letter = @char.ToString();
                foreach (var currWord in words)
                {
                    if (currWord.StartsWith(letter) && currWord.Length - 1 == asciiCodeAndLengthDict[letter])
                    {
                        Console.WriteLine(currWord);
                        break;
                    }
                }
            }
        }
    }
}