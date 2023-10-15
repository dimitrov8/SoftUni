namespace PlanetWars.Repositories
{
    using Contracts;
    using Models.Planets.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.planets;

        public void AddItem(IPlanet model) => this.planets.Add(model);

        public IPlanet FindByName(string name) => this.planets.FirstOrDefault(p => p.Name == name);

        public bool RemoveItem(string name) => this.planets.Remove(this.planets.FirstOrDefault(p => p.Name == name));
    }
}