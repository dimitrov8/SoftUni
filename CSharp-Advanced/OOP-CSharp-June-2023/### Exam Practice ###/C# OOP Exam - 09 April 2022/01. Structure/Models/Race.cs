namespace Formula1.Models
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Utilities;

    public class Race : IRace
    {
        private string name;
        private int laps;
        private readonly ICollection<IPilot> pilots;

        public Race(string name, int laps)
        {
            this.RaceName = name;
            this.NumberOfLaps = laps;
            this.TookPlace = false;
            this.pilots = new List<IPilot>();
        }

        public string RaceName
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));

                this.name = value;
            }
        }

        public int NumberOfLaps
        {
            get => this.laps;
            private set
            {
                if (value < 1)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));

                this.laps = value;
            }
        }

        public bool TookPlace { get; set; }
        public ICollection<IPilot> Pilots => this.pilots;

        public void AddPilot(IPilot pilot) => this.pilots.Add(pilot);

        public string RaceInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"The {this.RaceName} race has:");
            sb.AppendLine($"Participants: {this.Pilots.Count}");
            sb.AppendLine($"Number of laps: {this.NumberOfLaps}");
            sb.AppendLine(this.TookPlace ? "Took place: Yes" : "Took place: No");

            return sb.ToString().Trim();
        }
    }
}