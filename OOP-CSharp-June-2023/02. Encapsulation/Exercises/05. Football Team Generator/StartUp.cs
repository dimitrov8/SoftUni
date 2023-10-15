namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        private static List<Team> teamList;

        static void Main(string[] args)
        {
            teamList = new List<Team>();

            RunEngine();
        }

        private static void RunEngine()
        {
            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command!.Split(';');
                string mainCommand = tokens[0];
                string teamName = tokens[1];
                try
                {
                    if (mainCommand == "Team")
                        CreateTeam(teamName);
                    else if (mainCommand == "Add")
                        AddPlayerToTeam(teamName, tokens);

                    else if (mainCommand == "Remove")
                        RemovePlayerFromTeam(teamName, tokens);

                    else if (mainCommand == "Rating") RateTeam(teamName);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }

        static void CreateTeam(string teamName)
        {
            Team createdTeam = new Team(teamName);
            teamList.Add(createdTeam);
        }

        static void AddPlayerToTeam(string teamName, string[] tokens)
        {
            Team teamToJoin = teamList.FirstOrDefault(t => t.Name == teamName);
            if (teamToJoin == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.TEAM_IS_MISSING,
                    teamName));

            string playerName = tokens[2];
            int endurance = int.Parse(tokens[3]);
            int sprint = int.Parse(tokens[4]);
            int dribble = int.Parse(tokens[5]);
            int passing = int.Parse(tokens[6]);
            int shooting = int.Parse(tokens[7]);
            Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
            teamToJoin.AddPlayer(player);
        }

        static void RemovePlayerFromTeam(string teamName, string[] tokens)
        {
            string player = tokens[2];
            Team teamToRemovePlayerFrom = teamList.FirstOrDefault(t => t.Name == teamName);
            if (teamToRemovePlayerFrom == null)
                throw new ArgumentException(string.Format(ExceptionMessages.TEAM_IS_MISSING, teamName));

            teamToRemovePlayerFrom.RemovePlayer(player);
        }

        static void RateTeam(string teamName)
        {
            Team teamToRate = teamList.FirstOrDefault(t => t.Name == teamName);

            if (teamToRate == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.TEAM_IS_MISSING,
                    teamName));

            Console.WriteLine(teamToRate.ToString());
        }
    }
}