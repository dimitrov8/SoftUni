namespace Raiding.Core
{
    using Factories;
    using Factories.Interfaces;
    using Interfaces;
    using IO;
    using IO.Interfaces;
    using Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly ICollection<IBaseHero> heroes;
        private readonly IHeroFactory heroFactory;

        private Engine()
        {
            this.heroes = new List<IBaseHero>();
            this.heroFactory = new HeroFactory();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            int numberOfHeroes = int.Parse(this.reader.ReadLine());
            while (this.heroes.Count < numberOfHeroes)
                try
                {
                    this.heroes.Add(this.CreateHero());
                }
                catch (Exception ih)
                {
                    this.writer.WriteLine(ih.Message);
                }

            int bossPower = int.Parse(this.reader.ReadLine());
            this.AttackBoss(bossPower);
        }

        private IBaseHero CreateHero()
        {
            string heroName = this.reader.ReadLine();
            string heroType = this.reader.ReadLine();

            IBaseHero hero = this.heroFactory.CreateHero(heroName, heroType);
            return hero;
        }

        private void AttackBoss(int bossPower)
        {
            foreach (var hero in this.heroes)
                this.writer.WriteLine(hero.CastAbility());

            this.writer.WriteLine(this.heroes.Sum(h => h.Power) >= bossPower ? "Victory!" : "Defeat...");
        }
    }
}