namespace UniversityCompetition.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class StudentRepository : IRepository<IStudent>
    {
        private readonly List<IStudent> students;

        public StudentRepository()
        {
            this.students = new List<IStudent>();
        }
        
        public IReadOnlyCollection<IStudent> Models => this.students;

        public void AddModel(IStudent model) => this.students.Add(model);

        public IStudent FindById(int id) => this.students.FirstOrDefault(s => s.Id == id);

        public IStudent FindByName(string name)
        {
            string[] fullName = name.Split();
            string firstName = fullName[0];
            string lastName = fullName[1];

            return this.students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
        }
    }
}