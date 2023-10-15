namespace NavalVessels.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class VesselRepository : IRepository<IVessel>
    {
        private readonly ICollection<IVessel> vessels;

        public VesselRepository()
        {
            this.vessels = new HashSet<IVessel>();
        }

        public IReadOnlyCollection<IVessel> Models => (IReadOnlyCollection<IVessel>)this.vessels;

        public void Add(IVessel vessel) => this.vessels.Add(vessel);

        public bool Remove(IVessel vessel) => this.vessels.Remove(vessel);

        public IVessel FindByName(string name) => this.vessels.FirstOrDefault(v => v.Name == name);
    }
}