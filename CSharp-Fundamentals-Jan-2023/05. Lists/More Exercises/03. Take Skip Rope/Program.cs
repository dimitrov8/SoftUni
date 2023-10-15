using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Take_or_Skip_Rope
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create empty char input list
            List<char> input = new List<char>();

            // Then use the addRange to get the input as char list
            input.AddRange(Console.ReadLine());

            // Numbers list
            List<int> numbers = GetOnlyNumbersInList(input);

            // Non numbers list
            List<string> nonNumbers = GetOnlyTextInAList(input);

            // Take List
            List<int> takeList = TakeList(numbers);

            // Skip List
            List<int> skipList = SkipList(numbers);

            // Decrypt the message
            string result = DecryptTheMessage(takeList, nonNumbers, skipList);

            // Print the final resul
            Console.WriteLine(result);
        }

        static string DecryptTheMessage(List<int> takeList, List<string> nonNumbers, List<int> skipList)
        {
            // Create empty string
            string result = String.Empty;

            // Create index variable to update it in the for loop
            int index = 0;

            for (int i = 0; i < takeList.Count; i++)
            {
                // On each iteration: Create a sub-list of elements from nonNumbers
                // by skipping index number of elements
                // and taking takeList[i] number of elements.
                // This sub-list is stored in the temp list.
                List<string> temp = nonNumbers
                    .Skip(index)
                    .Take(takeList[i])
                    .ToList();

                // Concatenate the elements in the result
                result += string.Concat(temp);

                // The value of index is updated by adding takeList[i] and skipList[i]
                index += takeList[i] + skipList[i];
            }

            return result;
        }

        static List<int> SkipList(List<int> numbers)
        {
            List<int> skipList = numbers
                .Where((x, index) => index % 2 != 0)
                .ToList();
            return skipList;
        }

        static List<int> TakeList(List<int> numbers)
        {
            List<int> takeList = numbers
                .Where((x, index) => index % 2 == 0)
                .ToList();
            return takeList;
        }

        static List<string> GetOnlyTextInAList(List<char> input)
        {
            List<string> nonNumbers = input.Where(x => x < '0' || x > '9')
                .Select(x => x.ToString())
                .ToList();
            return nonNumbers;
        }

        static List<int> GetOnlyNumbersInList(List<char> input)
        {
            List<int> numbers = input.Where(x => x >= '0' && x <= '9')
                .Select(x => int.Parse(x.ToString()))
                .ToList();
            return numbers;
        }
    }
}