namespace Raiding.Factories
{
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.Interfaces;
    using System;

    public class HeroFactory : IHeroFactory
    {
        public IBaseHero CreateHero(string name, string type)
        {
            IBaseHero hero = type switch
            {
                "Druid" => new Druid(name),
                "Paladin" => new Paladin(name),
                "Rogue" => new Rogue(name),
                "Warrior" => new Warrior(name),
                _ => throw new Exception(ExceptionMessages.INVALID_HERO_EXCEPTION)
            };
            return hero;
        }
    }
}