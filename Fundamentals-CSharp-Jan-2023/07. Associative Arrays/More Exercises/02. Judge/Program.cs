using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Judge
{
    public class Contest
    {
        public string ContestName { get; set; }
        public Dictionary<string, int> Participants { get; set; }

        public Contest(string contestName)
        {
            ContestName = contestName;
            Participants = new Dictionary<string, int>();
        }
    }

    public class User
    {
        public string Username { get; set; }
        public int TotalPoints { get; set; }

        public User(string username)
        {
            Username = username;
            TotalPoints = 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Dictionary<string, Contest> contests = new Dictionary<string, Contest>();
            Dictionary<string, User> users = new Dictionary<string, User>();
            while ((input = Console.ReadLine()) != "no more time")
            {
                string[] tokens = input.Split(" -> ");
                string username = tokens[0];
                string contestName = tokens[1];
                int points = int.Parse(tokens[2]);

                if (!contests.ContainsKey(contestName)) // If contest is not existing
                {
                    contests.Add(contestName, new Contest(contestName)); // Add new contest
                }

                Contest contest = contests[contestName]; // Get contest
                User user; // Initialize user
                if (!users.ContainsKey(username)) // If user is not existing
                {
                    user = new User(username); // Add new user
                    users.Add(username, user); // Add new user
                }
                else // If user is existing
                {
                    user = users[username]; // Get user
                }

                if (!contest.Participants.ContainsKey(username)) // If a user is not participating in the current contest
                {
                    contest.Participants.Add(username, points); // Add the user in the contest with his username and points
                    user.TotalPoints += points; // Add to his total points
                }
                else // If a user is participating in the current contest
                {
                    if (points > contest.Participants[username]) // If a user is already participating in the contest
                    {
                        user.TotalPoints -= points;
                        contest.Participants[username] = points;
                        user.TotalPoints = points; // Add the user in the contest with his username
                    }
                }
            }

            foreach (var contest in contests.Values) // Print all contests
            {
                Console.WriteLine($"{contest.ContestName}: {contest.Participants.Count} participants");
                int place = 1;
                foreach (var participant in contest.Participants.OrderByDescending(p => p.Value).ThenBy(p => p.Key))
                {
                    // Sort participants by username
                    Console.WriteLine($"{place}. {participant.Key} <::> {participant.Value}");
                    place++;
                }
            }

            Console.WriteLine("Individual standings:");

            int rank = 1;
            foreach (var user in users.Values.OrderByDescending(u => u.TotalPoints)
                         .ThenBy(u => u.Username)) // Sort users by total points
            {
                Console.WriteLine($"{rank}. {user.Username} -> {user.TotalPoints}");
                rank++;
            }
        }
    }
}