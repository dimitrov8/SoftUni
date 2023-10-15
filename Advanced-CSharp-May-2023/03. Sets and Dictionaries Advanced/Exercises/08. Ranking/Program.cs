using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            // Contests and their passwords
            var contestAndPasswords = new Dictionary<string, string>();
            // Adds contests and their passwords in the dictionary
            while ((input = Console.ReadLine()) != "end of contests")
            {
                string[] contestInfo = input.Split(":", StringSplitOptions.RemoveEmptyEntries);
                string contestName = contestInfo[0];
                string contestPassword = contestInfo[1];

                if (!contestAndPasswords.ContainsKey(contestName))
                {
                    contestAndPasswords.Add(contestName, contestPassword);
                }
            }

            // Store results of each participant for every contest participated
            var contestResults = new SortedDictionary<string, Dictionary<string, int>>();
            while ((input = Console.ReadLine()) != "end of submissions")
            {
                string[] tokens = input.Split("=>");
                string userInputContest = tokens[0]; 
                string userInputPassword = tokens[1];
                string participant = tokens[2];
                int points = int.Parse(tokens[3]);
                
                // If the contest exists and the password is correct
                if (contestAndPasswords.ContainsKey(userInputContest) && contestAndPasswords[userInputContest] == userInputPassword)
                {
                    if (!contestResults.ContainsKey(participant)) // If a participant doesn't exist
                    {
                        contestResults.Add(participant, new Dictionary<string, int>()); // Add the participant to the dictionary
                        if (!contestResults[participant].ContainsKey(userInputContest)) // If the participant is not in the contest
                        {
                            contestResults[participant].Add(userInputContest, points); // Add the participant's result for the contest he participates
                        }
                    }
                    else if (contestResults.ContainsKey(participant)) // If the participant exists
                    {
                        // If the participant is in the contest and his current points are more than last submitted points
                        if (contestResults[participant].ContainsKey(userInputContest)  && contestResults[participant][userInputContest] < points)
                        {
                            contestResults[participant][userInputContest] = points; // Update participant's points for the contest
                        }
                        else if (!contestResults[participant].ContainsKey(userInputContest)) // If the participant is not in the contest
                        {
                            contestResults[participant].Add(userInputContest, points); // Add the contest and the points the participant earned
                        }
                    }
                }
            }
            string bestCandidate = String.Empty; // Variable to store the best candidate
            int bestCandidatePoints = 0; // Variable to store the best candidate points

            foreach (var participant in contestResults) // Foreach participant {kvp}
            {
                // Sums all points for each contest the current participant participated in
                if (participant.Value.Values.Sum() > bestCandidatePoints) // If the points are higher than the current bestCandidatePoints
                {
                    bestCandidatePoints = participant.Value.Values.Sum(); // Update the bestCandidatePoints
                    bestCandidate = participant.Key; // Update the bestCandidate
                }
            }

            // Print
            Console.WriteLine($"Best candidate is {bestCandidate} with total {bestCandidatePoints} points.");
            Console.WriteLine("Ranking:");
            foreach (var participant in contestResults)
            {
                Console.WriteLine(participant.Key);
                Console.WriteLine(string.Join(Environment.NewLine, participant.Value
                    .OrderByDescending(p => p.Value)
                    .Select(p => $"#  {p.Key} -> {p.Value}")));
            }
        }
    }
}

