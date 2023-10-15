using System;
using System.Collections.Generic;

namespace ComparingObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<Person> people = new List<Person>();
            while ((input = Console.ReadLine()) != "END")
            {
                string[] personInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                people.Add(new Person(personInfo[0], int.Parse(personInfo[1]), personInfo[2]));
            }

            int personToCompareId = int.Parse(Console.ReadLine()!) - 1;
            Person personToCompare = people[personToCompareId];

            int equalCounter = 0;
            int nonEqualCounter = 0;
            foreach (var person in people)
            {
                if (person.CompareTo(personToCompare) == 0)
                {
                    equalCounter++;
                }
                else
                {
                    nonEqualCounter++;
                }
            }

            Console.WriteLine(equalCounter > 1
                ? $"{equalCounter} {nonEqualCounter} {equalCounter + nonEqualCounter}"
                : "No matches");
        }
    }
}