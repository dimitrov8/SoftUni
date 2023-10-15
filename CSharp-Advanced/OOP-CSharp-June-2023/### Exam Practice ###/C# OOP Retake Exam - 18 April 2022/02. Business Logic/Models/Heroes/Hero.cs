namespace Heroes.Models.Heroes
{
    using Contracts;
    using System;
    using Utilities.Messages;

    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armour;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.HeroNameNull);

                this.name = value;
            }
        }

        public int Health
        {
            get => this.health;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.HeroHealthBelowZero);

                this.health = value;
            }
        }

        public int Armour
        {
            get => this.armour;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);

                this.armour = value;
            }
        }

        public IWeapon Weapon
        {
            get => this.weapon;
            private set
            {
                if (value == null)
                    throw new ArgumentException(ExceptionMessages.WeaponNull);

                this.weapon = value;
            }
        }

        public bool IsAlive => this.Health > 0;

        public void TakeDamage(int points)
        {
            if (this.Armour > 0)
            {
                if (this.Armour - points >= 0)
                {
                    this.Armour -= points;
                }
                else if (this.Armour - points < 0)
                {
                    int remainingDamage = Math.Abs(this.Armour - points);
                    this.Armour = 0;


                    if (this.Health - remainingDamage >= 0)
                    {
                        this.Health -= remainingDamage;
                    }
                    else if (this.Health - remainingDamage < 0)
                    {
                        this.Health = 0;
                    }
                }
            }

            else if (this.Health > 0)
            {
                if (this.Health - points >= 0)
                {
                    this.Health -= points;
                }
                else if (this.Health - points < 0)
                {
                    this.Health = 0;
                }
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon ??= weapon;
        }
    }
}