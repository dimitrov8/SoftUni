namespace Heroes.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> heroes;

        public HeroRepository()
        {
            this.heroes = new List<IHero>();
        }

        public IReadOnlyCollection<IHero> Models => this.heroes;

        public void Add(IHero model) => this.heroes.Add(model);

        public bool Remove(IHero model) => this.heroes.Remove(model);

        public IHero FindByName(string name) => this.heroes.FirstOrDefault(h => h.Name == name);
    }
}