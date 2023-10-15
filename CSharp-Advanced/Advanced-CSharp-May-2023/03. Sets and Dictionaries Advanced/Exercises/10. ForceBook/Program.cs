using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._ForceBook
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            var sides = new Dictionary<string, HashSet<string>>(); // Key: ForceSide, Value: Members(HashSet<string)>
            while ((input = Console.ReadLine()) != "Lumpawaroo")
            {
                if (input.Contains('|'))
                {
                    string[] tokens = input.Split(" | ", StringSplitOptions.RemoveEmptyEntries);
                    string forceSide = tokens[0];
                    string forceUser = tokens[1];

                    if (!sides.Values.Any(u => u.Contains(forceUser))) // If the forceUser(member) doesn't exist =>
                    {
                        // => If the forceSide(team) doesn't exist =>
                        if (!sides.ContainsKey(forceSide))
                        {
                            // Add a new forceSide(team) to dictionary with no members =>
                            sides.Add(forceSide, new HashSet<string>());
                        }
                        // => Then Add the forceUser(member) to the newly created team
                        sides[forceSide].Add(forceUser);
                    }
                }
                else if (input.Contains("->"))
                {
                    string[] tokens = input.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                    string forceUser = tokens[0];
                    string forceSide = tokens[1];

                    foreach (var kvp in sides) // For loop for each side
                    {
                        if (kvp.Value.Any(m => m == forceUser)) // If any forceSide(team) has a member equal to forceUser
                        {
                            kvp.Value.Remove(forceUser); // Remove the foreUser from the forceSide(team) => 
                            break; // => Breaks the loop
                        }
                    }

                    if (!sides.ContainsKey(forceSide)) // If a forceSide(team) doesn't exist =>
                    {
                        sides.Add(forceSide, new HashSet<string>()); // => Crate a new team with no members =>
                    }

                    sides[forceSide].Add(forceUser); // => Then add the userForce(member) to the corresponding forceSide(team) =>
                    Console.WriteLine($"{forceUser} joins the {forceSide} side!"); // => Print
                }
            }

            // Order the sides by their members count and then by the name of the team 
            foreach (var kvp in sides
                         .OrderByDescending(m => m.Value.Count)
                         .ThenBy(t => t.Key))
            {
                if (kvp.Value.Count > 0) // If there are members in the team =>
                {
                    Console.WriteLine($"Side: {kvp.Key}, Members: {kvp.Value.Count}"); // => Print the team and then the members count: Then =>

                    foreach (var member in kvp.Value.OrderBy(m => m)) // => Foreach every member ordered by members name  =>
                    {
                        Console.WriteLine($"! {member}"); // => Print the member
                    }
                }
            }
        }
    }
}