using System.Diagnostics;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class WordCount
    {
        static void Main()
        {
            string wordPath = @"..\..\..\Files\words.txt";
            string textPath = @"..\..\..\Files\text.txt";
            string outputPath = @"..\..\..\Files\output.txt";

            CalculateWordCounts(wordPath, textPath, outputPath);
        }

        public static void CalculateWordCounts(string wordsFilePath, string textFilePath, string outputFilePath)
        {
            HashSet<string> wordList = new HashSet<string>();
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            using (var wordsReader = new StreamReader(wordsFilePath))
            {
                while (!wordsReader.EndOfStream)
                {
                    foreach (var word in wordsReader.ReadLine().Split(' '))
                    {
                        wordList.Add(word);
                    }
                }

                using (var textReader = new StreamReader(textFilePath))
                {
                    string pattern = @"\b[A-z']+\b";
                    MatchCollection matches = Regex.Matches(textReader.ReadToEnd().ToLower(), pattern);
                    foreach (var match in matches)
                    {
                        string currentWord = match.ToString();
                        if (wordList.Contains(currentWord) && !wordCount.ContainsKey(currentWord))
                        {
                            wordCount.Add(currentWord, 1);
                        }                            
                        else if (wordList.Contains(currentWord) && wordCount.ContainsKey(currentWord))
                        {
                            wordCount[currentWord]++;
                        }
                    }
                    using (var writer = new StreamWriter(outputFilePath))
                    {
                        foreach (var kvp in wordCount.OrderByDescending(f => f.Value))
                        {
                            writer.WriteLine($"{kvp.Key} - {kvp.Value}");
                        }
                    }
                }
            }
        }
    }
}