namespace ChristmasPastryShop.Repositories
{
    using Contracts;
    using Models.Delicacies.Contracts;
    using System.Collections.Generic;

    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private readonly List<IDelicacy> delicacies;

        public DelicacyRepository()
        {
            this.delicacies = new List<IDelicacy>();
        }

        public IReadOnlyCollection<IDelicacy> Models => this.delicacies;

        public void AddModel(IDelicacy model) => this.delicacies.Add(model);
    }
}
