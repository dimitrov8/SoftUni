namespace FootballTeamGenerator
{
    using System;

    public class Player
    {
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Name = name;
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
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

        public int Endurance
        {
            get => this.endurance;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(string.Format(ExceptionMessages.INVALID_STATS, nameof(this.Endurance)));
                this.endurance = value;
            }
        }

        public int Sprint
        {
            get => this.sprint;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(string.Format(ExceptionMessages.INVALID_STATS, nameof(this.Sprint)));
                this.sprint = value;
            }
        }

        public int Dribble
        {
            get => this.dribble;

            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(string.Format(ExceptionMessages.INVALID_STATS, nameof(this.Dribble)));
                this.dribble = value;
            }
        }

        public int Passing
        {
            get => this.passing;

            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(string.Format(ExceptionMessages.INVALID_STATS, nameof(this.Passing)));
                this.passing = value;
            }
        }

        public int Shooting
        {
            get => this.shooting;

            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException(string.Format(ExceptionMessages.INVALID_STATS, nameof(this.Shooting)));
                this.shooting = value;
            }
        }

        public double OverallRating =>
            (this.Endurance + this.Sprint + this.Dribble + this.Passing + this.Shooting) / 5.0;
    }
}