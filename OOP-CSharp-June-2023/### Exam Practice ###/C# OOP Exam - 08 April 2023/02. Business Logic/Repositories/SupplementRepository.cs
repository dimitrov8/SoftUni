namespace RobotService.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplementRepository : IRepository<ISupplement>
    {
        private readonly List<ISupplement> supplements;

        public SupplementRepository()
        {
            this.supplements = new List<ISupplement>();
        }

        public IReadOnlyCollection<ISupplement> Models() => this.supplements.AsReadOnly();

        public void AddNew(ISupplement model) => this.supplements.Add(model);

        public bool RemoveByName(string typeName) => this.supplements.Remove(this.supplements.FirstOrDefault(s => s.GetType().Name == typeName));

        public ISupplement FindByStandard(int interfaceStandard)
            => this.supplements.FirstOrDefault(s => s.InterfaceStandard == interfaceStandard);
    }
}