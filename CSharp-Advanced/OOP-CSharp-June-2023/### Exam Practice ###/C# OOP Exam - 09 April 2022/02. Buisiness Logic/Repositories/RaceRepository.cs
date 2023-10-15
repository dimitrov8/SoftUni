namespace Formula1.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            this.races = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => this.races;

        public void Add(IRace race) => this.races.Add(race);

        public bool Remove(IRace race) => this.races.Remove(race);

        public IRace FindByName(string name) => this.races.FirstOrDefault(r => r.RaceName == name);
    }
}