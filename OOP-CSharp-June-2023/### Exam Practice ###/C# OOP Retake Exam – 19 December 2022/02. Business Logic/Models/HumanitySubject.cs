namespace UniversityCompetition.Models
{
    public class HumanitySubject : Subject
    {
        private const double RATE = 1.15;

        public HumanitySubject(int id, string name) : base(id, name, RATE)
        {
        }
    }
}