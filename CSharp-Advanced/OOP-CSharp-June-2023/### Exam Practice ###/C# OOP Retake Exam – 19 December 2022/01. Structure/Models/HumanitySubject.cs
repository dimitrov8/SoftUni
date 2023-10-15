namespace UniversityCompetition.Models.Contracts
{
    public class HumanitySubject : Subject
    {
        private const double SUBJECT_RATE = 1.15;

        public HumanitySubject(int subjectId, string subjectName) : base(subjectId, subjectName, SUBJECT_RATE)
        {
        }
    }
}