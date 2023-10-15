namespace UniversityCompetition.Models
{
    public class TechnicalSubject : Subject
    {
        private const double SUBJECT_RATE = 1.3;

        public TechnicalSubject(int id, string name) : base(id, name, SUBJECT_RATE)
        {
        }
    }
}