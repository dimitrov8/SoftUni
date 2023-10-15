namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Team
    {
        private string name;
        private readonly List<Player> playerList;

        private Team()
        {
            this.playerList = new List<Player>();
        }

        public Team(string name) : this()
        {
            this.Name = name;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.NAME_IS_IS_EMPTY);
                this.name = value;
            }
        }

        private int Rating => this.playerList.Count > 0
            ? (int)Math.Round(this.playerList.Average(p => p.OverallRating), 0)
            : 0;

        public void AddPlayer(Player player)
        {
            this.playerList.Add(player);
        }

        public void RemovePlayer(string player)
        {
            var playerToRemove = this.playerList.FirstOrDefault(p => p.Name == player);

            if (playerToRemove == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PLAYER_IS_MISSING, player,
                    this.Name));
            this.playerList.Remove(playerToRemove);
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Rating:0}";
        }
    }
}