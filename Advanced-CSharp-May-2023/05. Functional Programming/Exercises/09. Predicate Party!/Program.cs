using System;
using System.Collections.Generic;
using System.Linq;
namespace _09._Predicate_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> people = Console.ReadLine().Split().ToList(); // User input of people going to the party
            string command; // Initialize the command
            while ((command = Console.ReadLine()) != "Party!") // While the command is not "Party!"
            {
                string[] tokens = command.Split(); // Split the command into tokens
                string mainCommand = tokens[0]; // Main command: (Double/Remove)
                string condition = tokens[1]; // Condition: (StartsWith/EndsWith/Length)
                string criteria = tokens[2]; // Criteria: (StartsWith: letter) | (EndsWith: letter) | (Length: integer) 

                Func<string, string, string, List<string>> func = (mainCommand, condition, criteria) => // Function to execute 
                {
                    List<string> matchingNames = new List<string>(); // Keeps the matching names so we can double each matching name if the mainCommand is "Double"
                    if (mainCommand == "Double" && condition == "StartsWith")
                    {
                        matchingNames = people.Where(n => n.StartsWith(criteria)).ToList(); // Gets the matching names that start with the criteria from the original list
                        DoubleMatchingNames(matchingNames, people); // For each matching name add the name 2 more times in the original list
                    }
                    else if (mainCommand == "Double" && condition == "EndsWith") 
                    {
                        matchingNames = people.Where(n => n.EndsWith(criteria)).ToList(); // Gets the matching names that end with the criteria from the original list
                        DoubleMatchingNames(matchingNames, people); // For each matching name add the name 2 more times in the original list
                    }
                    else if (mainCommand == "Double" && condition == "Length")
                    {
                        matchingNames = people.Where(n => n.Length == int.Parse(criteria)).ToList(); // Gets the matching names with length equal to the criteria from the original list
                        DoubleMatchingNames(matchingNames, people); // For each matching name add the name 2 more times in the original list
                    }

                    else if (mainCommand == "Remove" && condition == "StartsWith")
                    {
                        people.RemoveAll(n => n.StartsWith(criteria)); // Removes the names that start with the criteria
                    }
                    else if (mainCommand == "Remove" && condition == "EndsWith")
                    {
                        people.RemoveAll(n => n.EndsWith(criteria)); // Removes the names that end with the criteria
                    }
                    else if (mainCommand == "Remove" && condition == "Length")
                    {
                        people.RemoveAll(n => n.Length == int.Parse(criteria)); // Removes the names that have equal length to the criteria
                    }

                    return people; // Returns the modified list of people 
                };
                
                people = func(mainCommand, condition, criteria); // Calls the function
            }

            if (people.Any()) // If there are people in the list
            {
                Console.WriteLine($"{string.Join(", ", people)} are going to the party!"); // Print
                return; // Stop the program
            }

            Console.WriteLine("Nobody is going to the party!"); // If there are no people in the list => Print
        }

        private static void DoubleMatchingNames(List<string> matchingNames, List<string> people)
        {
            foreach (var name in matchingNames)
            {
                int index = matchingNames.IndexOf(name); // Gets the index of the matching name, because we should insert next to it
                people.Insert(index, name);
            }
        }
    }
}