using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._MOBA_Challenger
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Dictionary<string, Dictionary<string, int>> ladder = new Dictionary<string, Dictionary<string, int>>();
            while ((input = Console.ReadLine()) != "Season end")
            {
                if (input.Split(" ").Length == 5) // Means we add players to the ladder
                {
                    string[] tokens = input.Split(" -> ");
                    string player = tokens[0];
                    string position = tokens[1];
                    int skill = int.Parse(tokens[2]);

                    if (!ladder.ContainsKey(player)) // If player is not in the ladder
                    {
                        ladder.Add(player, new Dictionary<string, int> { { position, skill } }); // Add him
                    }
                    else if (ladder.ContainsKey(player)) // If player is in the ladder
                    {
                        if (!ladder[player].ContainsKey(position)) // If player plays a new position
                        {
                            ladder[player].Add(position, skill); // Add that position
                            continue;
                        }

                        // If player plays the same position 
                        int existingSkill = ladder[player][position]; // Get existing skill
                        if (ladder[player].ContainsKey(player) &&
                            skill > existingSkill) // Check if current skill is higher
                        {
                            ladder[player][position] = skill; // If it is update the skill in the ladder
                        }
                    }
                }
                else // It means players are fighting for the ladder
                {
                    string[] data = input.Split(" vs ");
                    string player1 = data[0];
                    string player2 = data[1];

                    if (ladder.ContainsKey(player1) && ladder.ContainsKey(player2)) // If both players are in the ladder
                    {
                        Dictionary<string, int> player1Data = ladder[player1]; // Get the data of player1
                        Dictionary<string, int> player2Data = ladder[player2]; // Get the data of player2 
                        if (player1Data.Keys.Intersect(player2Data.Keys).Any()) // If both players have same lanes
                        {
                            int p1Skill = ladder[player1].Values.Sum(); // Get the total skill of player1
                            int p2Skill = ladder[player2].Values.Sum(); // Get the total skill of player2
                            if (p1Skill > p2Skill) // If p1 has a higher skill than p2
                                ladder.Remove(player2); // Remove p2 from the ladder

                            else if (p2Skill > p1Skill) // If p2 has a higher skill than p1
                                ladder.Remove(player1); // Remove p1 from the ladder
                        }
                    }
                }
            }
            
            foreach (var player in ladder.OrderByDescending(p => p.Value.Values.Sum()))
            {
                // Print the ladder in descending order by the total skill
                Console.WriteLine($"{player.Key}: {player.Value.Values.Sum()} skill");
                foreach (var position in ladder[player.Key].OrderByDescending(s => s.Value).ThenBy(p => p.Key))
                {
                    // Print the skill in descending order then order by position name in ascending order
                    Console.WriteLine($"- {position.Key} <::> {position.Value}");
                }
            }
        }
    }
}