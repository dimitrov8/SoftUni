namespace Formula1.Models
{
    using Contracts;
    using System;
    using Utilities;

    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car;

        public Pilot(string fullName)
        {
            this.FullName = fullName;
            this.CanRace = false;
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));

                this.fullName = value;
            }
        }

        public IFormulaOneCar Car
        {
            get => this.car;
            private set => this.car = value ?? throw new NullReferenceException(ExceptionMessages.InvalidCarForPilot);
        }

        public int NumberOfWins { get; private set; }
        public bool CanRace { get; private set; }

        public void AddCar(IFormulaOneCar car)
        {
            this.Car = car;
            this.CanRace = true;
        }

        public void WinRace() => this.NumberOfWins++;

        public override string ToString() => $"Pilot {this.FullName} has {this.NumberOfWins} wins.";
    }
}