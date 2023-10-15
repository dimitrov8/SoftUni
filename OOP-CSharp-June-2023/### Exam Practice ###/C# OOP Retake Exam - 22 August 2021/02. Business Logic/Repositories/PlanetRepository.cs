namespace SpaceStation.Repositories
{
    using Contracts;
    using Models.Planets.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly ICollection<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => (IReadOnlyCollection<IPlanet>)this.planets;

        public void Add(IPlanet planet) => this.planets.Add(planet);

        public bool Remove(IPlanet planet) => this.planets.Remove(planet);

        public IPlanet FindByName(string name) => this.planets.FirstOrDefault(p => p.Name == name);
    }
}