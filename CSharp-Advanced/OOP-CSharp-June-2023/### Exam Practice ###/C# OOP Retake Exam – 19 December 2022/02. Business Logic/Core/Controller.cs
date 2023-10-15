namespace UniversityCompetition.Core // miNE
{
    using Contracts;
    using Models;
    using Models.Contracts;
    using Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly SubjectRepository subjects;
        private readonly StudentRepository students;
        private readonly UniversityRepository universities;

        private readonly IReadOnlyCollection<string> supportedSubjects = new HashSet<string>
        {
            nameof(TechnicalSubject),
            nameof(HumanitySubject),
            nameof(EconomicalSubject)
        };

        public Controller()
        {
            this.subjects = new SubjectRepository();
            this.students = new StudentRepository();
            this.universities = new UniversityRepository();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (!this.supportedSubjects.Contains(subjectType))
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);

            if (this.subjects.FindByName(subjectName) != null)
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);

            ISubject subject = null;
            int subjectId = this.subjects.Models.Count + 1;
            if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(subjectId, subjectName);
            }
            else if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjectId, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjectId, subjectName);
            }

            this.subjects.AddModel(subject);
            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, nameof(SubjectRepository));
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (this.universities.FindByName(universityName) != null)
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);

            var requiredSubjectsIds = new List<int>();
            foreach (string requiredSubject in requiredSubjects)
            {
                requiredSubjectsIds.Add(this.subjects.FindByName(requiredSubject).Id);
            }

            IUniversity university = new University(this.universities.Models.Count + 1, universityName, category, capacity, requiredSubjectsIds);
            this.universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, nameof(UniversityRepository));
        }

        public string AddStudent(string firstName, string lastName)
        {
            string fullName = firstName + " " + lastName;
            if (this.students.FindByName(fullName) != null)
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);

            IStudent student = new Student(this.students.Models.Count + 1, firstName, lastName);
            this.students.AddModel(student);

            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, nameof(StudentRepository));
        }

        public string TakeExam(int studentId, int subjectId)
        {
            if (this.students.FindById(studentId) == null)
                return string.Format(OutputMessages.InvalidStudentId);

            if (this.subjects.FindById(subjectId) == null)
                return string.Format(OutputMessages.InvalidSubjectId);

            var student = this.students.FindById(studentId);
            var subject = this.subjects.FindById(subjectId);
            if (student.CoveredExams.Contains(subjectId))
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);

            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] studentFullName = studentName.Split();
            string studentFirstName = studentFullName[0];
            string studentLastName = studentFullName[1];

            if (this.students.FindByName(studentName) == null)
                return string.Format(OutputMessages.StudentNotRegitered, studentFirstName, studentLastName);

            if (this.universities.FindByName(universityName) == null)
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);

            var student = this.students.FindByName(studentName);
            var university = this.universities.FindByName(universityName);

            if (!university.RequiredSubjects.SequenceEqual(student.CoveredExams))
                return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);

            if (student.University == university)
                return string.Format(OutputMessages.StudentAlreadyJoined, studentFirstName, studentLastName, universityName);

            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, studentFirstName, studentLastName, universityName);
        }

        public string UniversityReport(int universityId)
        {
            var university = this.universities.FindById(universityId);

            int studentCount = this.students.Models.Count(s => s.University == university);
            int capacityLeft = university.Capacity - studentCount;

            var sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentCount}");
            sb.AppendLine($"University vacancy: {capacityLeft}");

            return sb.ToString().Trim();
        }
    }
}