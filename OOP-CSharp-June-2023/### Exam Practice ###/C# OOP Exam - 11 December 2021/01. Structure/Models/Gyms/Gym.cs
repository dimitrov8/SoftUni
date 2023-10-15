namespace Gym.Models.Gyms
{
    using Athletes.Contracts;
    using Contracts;
    using Equipment.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public abstract class Gym : IGym
    {
        private string name;
        private readonly ICollection<IEquipment> equipment;
        private readonly ICollection<IAthlete> athletes;

        protected Gym(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.equipment = new List<IEquipment>();
            this.athletes = new List<IAthlete>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);

                this.name = value;
            }
        }

        public int Capacity { get; private set; }
        public double EquipmentWeight => this.equipment.Select(e => e.Weight).Sum();
        public ICollection<IEquipment> Equipment => this.equipment;
        public ICollection<IAthlete> Athletes => this.athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (this.Capacity == 0)
                throw new InvalidCastException(ExceptionMessages.NotEnoughSize);

            this.Athletes.Add(athlete);
            this.Capacity--;
        }

        public bool RemoveAthlete(IAthlete athlete) => this.Athletes.Remove(athlete);

        public void AddEquipment(IEquipment equipment) => this.Equipment.Add(equipment);

        public void Exercise()
        {
            foreach (var athlete in this.Athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            var sb = new StringBuilder();
            string athletesInfo = this.Athletes.Any() ? string.Join(", ", this.Athletes.Select(a => a.FullName)) : "No athletes";
            sb.AppendLine($"{this.Name} is a {this.GetType().Name}:");
            sb.AppendLine($"Athletes: {athletesInfo}");
            sb.AppendLine($"Equipment total count: {this.Equipment.Count}");
            sb.AppendLine($"Equipment total weight: {this.EquipmentWeight:F2} grams");

            return sb.ToString().Trim();
        }
    }
}