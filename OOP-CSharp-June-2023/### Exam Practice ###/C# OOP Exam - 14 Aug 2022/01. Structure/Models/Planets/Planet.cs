namespace PlanetWars.Models.Planets
{
    using Contracts;
    using MilitaryUnits;
    using MilitaryUnits.Contracts;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;
    using Weapons;
    using Weapons.Contracts;

    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private double militaryPower;
        private readonly UnitRepository militaryUnits;
        private readonly WeaponRepository weapons;

        public Planet(string name, double budget)
        {
            this.Name = name;
            this.Budget = budget;
            this.militaryUnits = new UnitRepository();
            this.weapons = new WeaponRepository();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);

                this.name = value;
            }
        }

        public double Budget
        {
            get => this.budget;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);

                this.budget = value;
            }
        }

        public double MilitaryPower => Math.Round(this.CalculateMilitaryPower(), 3);

        public IReadOnlyCollection<IMilitaryUnit> Army => this.militaryUnits.Models;
        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit) => this.militaryUnits.AddItem(unit);

        public void AddWeapon(IWeapon weapon) => this.weapons.AddItem(weapon);

        public void TrainArmy()
        {
            foreach (var unit in this.Army)
            {
                unit.IncreaseEndurance();
            }
        }

        public void Spend(double amount)
        {
            if (this.Budget < amount)
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);

            this.Budget -= amount;
        }

        public void Profit(double amount) => this.Budget += amount;

        public string PlanetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
            if (!this.militaryUnits.Models.Any())
            {
                sb.AppendLine("--Forces: No units");
            }
            else
            {
                var unitList = this.militaryUnits.Models.Select(u => u.GetType().Name).ToList();

                sb.AppendLine($"--Forces: {string.Join(", ", unitList)}");
            }

            if (!this.weapons.Models.Any())
            {
                sb.AppendLine("--Combat equipment: No weapons");
            }
            else
            {
                var weaponList = this.weapons.Models.Select(w => w.GetType().Name).ToList();
                sb.AppendLine($"--Combat equipment: {string.Join(", ", weaponList)}");
            }

            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().Trim();
        }

        private double CalculateMilitaryPower()
        {
            double totalAmount = this.Army.Sum(a => a.EnduranceLevel) + this.Weapons.Sum(w => w.DestructionLevel);

            if (this.Army.Any(a => a.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                totalAmount *= 1.3;
            }

            if (this.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                totalAmount *= 1.45;
            }

            return totalAmount;
        }
    }
}