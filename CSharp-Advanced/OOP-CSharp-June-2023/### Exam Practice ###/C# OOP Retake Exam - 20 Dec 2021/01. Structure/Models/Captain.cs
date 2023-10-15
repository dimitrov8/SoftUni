namespace NavalVessels.Models
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Utilities.Messages;

    public class Captain : ICaptain
    {
        private string fullName;

        private Captain()
        {
            this.Vessels = new HashSet<IVessel>();
            this.CombatExperience = 0;
        }

        public Captain(string fullName)
            : this()
        {
            this.FullName = fullName;
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);

                this.fullName = value;
            }
        }

        public int CombatExperience { get; private set; }
        public ICollection<IVessel> Vessels { get; private set; }

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);

            this.Vessels.Add(vessel);
        }

        public void IncreaseCombatExperience() => this.CombatExperience += 10;

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.FullName} has {this.CombatExperience} combat experience and commands {this.Vessels.Count} vessels.");

            foreach (var vessel in this.Vessels)
            {
                sb.AppendLine(vessel.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}