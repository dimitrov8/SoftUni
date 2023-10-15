namespace UniversityCompetition.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class UniversityRepository : IRepository<IUniversity>
    {
        private readonly List<IUniversity> universities;

        public UniversityRepository()
        {
            this.universities = new List<IUniversity>();
        }

        public IReadOnlyCollection<IUniversity> Models => this.universities;

        public void AddModel(IUniversity model) => this.universities.Add(model);

        public IUniversity FindById(int id) => this.universities.FirstOrDefault(u => u.Id == id);

        public IUniversity FindByName(string name) => this.universities.FirstOrDefault(u => u.Name == name);
    }
}