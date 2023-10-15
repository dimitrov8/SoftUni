namespace PlanetWars.Models.MilitaryUnits
{
    using Contracts;
    using System;
    using Utilities.Messages;

    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            this.Cost = cost;
            this.EnduranceLevel = 1;
        }

        public double Cost { get; private set; }

        public int EnduranceLevel
        {
            get => this.enduranceLevel;
            private set
            {
                if (value > 20)
                    throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);

                this.enduranceLevel = value;
            }
        }

        public void IncreaseEndurance()
        {
            this.EnduranceLevel++;
        }
    }
}