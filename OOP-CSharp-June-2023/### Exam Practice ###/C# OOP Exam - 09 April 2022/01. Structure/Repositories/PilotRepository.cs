namespace Formula1.Repositories.Contracts
{
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots;

        public PilotRepository()
        {
            this.pilots = new List<IPilot>();
        }

        public IReadOnlyCollection<IPilot> Models => this.pilots;

        public void Add(IPilot pilot) => this.pilots.Add(pilot);

        public bool Remove(IPilot pilot) => this.pilots.Remove(pilot);

        public IPilot FindByName(string name) => this.pilots.FirstOrDefault(p => p.FullName == name);
    }
}