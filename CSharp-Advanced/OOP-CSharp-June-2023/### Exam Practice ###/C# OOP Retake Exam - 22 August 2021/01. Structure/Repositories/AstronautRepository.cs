namespace SpaceStation.Repositories
{
    using Contracts;
    using Models.Astronauts.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly ICollection<IAstronaut> astronauts;

        public AstronautRepository()
        {
            this.astronauts = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => (IReadOnlyCollection<IAstronaut>)this.astronauts;

        public void Add(IAstronaut astronaut) => this.astronauts.Add(astronaut);

        public bool Remove(IAstronaut astronaut) => this.astronauts.Remove(astronaut);

        public IAstronaut FindByName(string name) => this.astronauts.FirstOrDefault(a => a.Name == name);
    }
}