﻿namespace Raiding.Models
{
    using Interfaces;

    public abstract class BaseHero : IBaseHero
    {
        protected BaseHero(string name, int power)
        {
            this.Name = name;
            this.Power = power;
        }

        public string Name { get; private set; }
        public int Power { get; private set; }

        public abstract string CastAbility();
    }
}