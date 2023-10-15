namespace PlanetWars.Models.Weapons
{
    using Contracts;
    using System;
    using Utilities.Messages;

    public class Weapon : IWeapon
    {
        private int destructionLevel;

        public Weapon(int destructionLevel, double price)
        {
            this.DestructionLevel = destructionLevel;
            this.Price = price;
        }

        public int DestructionLevel
        {
            get => this.destructionLevel;
            private set
            {
                if (value < 1)
                    throw new ArgumentException(ExceptionMessages.TooLowDestructionLevel);

                if (value > 10)
                    throw new ArgumentException(ExceptionMessages.TooHighDestructionLevel);

                this.destructionLevel = value;
            }
        }

        public double Price { get; private set; }
    }
}