using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Teamwork_Projects
{
    class Program
    {
        static void Main(string[] args)
        {
            int teamToRegister = int.Parse(Console.ReadLine()); // Read the number of teams to register
            List<Team> teams = new List<Team>(); // Initialize an empty list of teams
            for (int i = 1; i <= teamToRegister; i++) // Register each team
            {
                string[] teamData = Console.ReadLine().Split("-"); // Read the input data for the team
                string creatorName = teamData[0];
                string teamName = teamData[1];

                // Check if the team name or creator name already exist
                bool teamExists = teams.Any(t => t.TeamName == teamName);
                bool creatorExists = teams.Any(t => t.CreatorName == creatorName);

                if (teamExists) // If the team already exists or 
                {
                    Console.WriteLine($"Team {teamName} was already created!"); // Print 
                }

                else if (creatorExists) // If the creator has already created a team
                {
                    Console.WriteLine($"{creatorName} cannot create another team!"); // Print
                }

                else // Otherwise, create the team and add it to the list of teams
                {
                    teams.Add(new Team(teamName, creatorName));
                    Console.WriteLine($"Team {teamName} has been created by {creatorName}!");
                }
            }

            string input; // Read the input data for each new member and add them to the appropriate team
            while ((input = Console.ReadLine()) != "end of assignment")
            {
                string[] newMemberData = input.Split("->");
                string newMemberName = newMemberData[0];
                string teamToJoin = newMemberData[1];

                // Check teams and if a team is found it returns it, otherwise it returns null
                Team team = teams.FirstOrDefault(t => t.TeamName == teamToJoin);

                bool teamExists = teams.Any(t => t.TeamName == teamToJoin); // Check if the team exists
                bool creatorExists =
                    teams.Any(t => t.CreatorName == newMemberName) || // Check if new member is a creator of any team
                    teams.Any(t => t.Members.Contains(newMemberName)); // Check if new member is a member of any team
                if (!teamExists) // If the team does not exist
                {
                    Console.WriteLine($"Team {teamToJoin} does not exist!"); // Print
                }

                else if
                    (creatorExists) // If the new member is a creator of any team or if the new member is a member of any team
                {
                    Console.WriteLine($"Member {newMemberName} cannot join team {teamToJoin}!"); // Print
                }

                else // Else
                {
                    team.Members.Add(newMemberName); // Add him to the team
                }
            }

            List<Team> validTeams = teams.Where(t => t.Members.Count > 0) // Get only team which have at least one member
                .OrderByDescending(t => t.Members.Count) // Sort the list of teams by number 
                .ThenBy(t => t.TeamName) // And then by team name
                .ToList();
            foreach (Team team in validTeams) // Foreach valid team
            {
                Console.WriteLine(team.TeamName); // Print the team name
                Console.WriteLine($"- {team.CreatorName}"); // Print the creator name
                
                List<string> members = team.Members.OrderBy(m => m).ToList(); // Sort the list of members by name in ascending order
                foreach (string member in members) // Foreach member
                {
                    Console.WriteLine($"-- {member}"); // Print the member name
                }
            }

            List<Team> disbandTeams = teams
                .Where(t => t.Members.Count == 0) // Get teams which have no members
                .OrderBy(t => t.TeamName) // Sort the team names in ascending order
                .ToList(); 
            Console.WriteLine("Teams to disband:"); // Print
            foreach (Team team in disbandTeams) // Foreach disbanded teams
            {
                Console.WriteLine(team.TeamName); // Print the team name
            }
        }

        private class Team
        {
            public string TeamName { get; set; }
            public string CreatorName { get; set; }
            public List<string> Members { get; set; }

            public Team(string teamName, string creatorName)
            {
                TeamName = teamName;
                CreatorName = creatorName;
                // Initialize the list of members
                Members = new List<string>();
            }
        }
    }
}