namespace UniversityCompetition.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class SubjectRepository : IRepository<ISubject>
    {
        private readonly List<ISubject> subjects;

        public SubjectRepository()
        {
            this.subjects = new List<ISubject>();
        }

        public IReadOnlyCollection<ISubject> Models => this.subjects;


        public void AddModel(ISubject model) => this.subjects.Add(model);

        public ISubject FindById(int id) => this.subjects.FirstOrDefault(s => s.Id == id);

        public ISubject FindByName(string name) => this.subjects.FirstOrDefault(s => s.Name == name);
    }
}