using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassroomProject
{
    public class Classroom
    {
        public Classroom(int capacity)
        {
            this.Capacity = capacity;
            this.students = new HashSet<Student>(capacity);
        }

        private HashSet<Student> students;

        public int Capacity { get; set; }

        public int Count => this.students.Count;

        public string RegisterStudent(Student student)
        {
            if (this.Capacity <= this.Count) return "No seats in the classroom";
            this.students.Add(student);
            return $"Added student {student.FirstName} {student.LastName}";

        }

        public string DismissStudent(string firstName, string lastName)
        {
            Student studentToDismiss = this.students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
            if (studentToDismiss != null)
            {
                this.students.Remove(studentToDismiss);
                return $"Dismissed student {firstName} {lastName}";
            }

            return "Student not found";
        }

        public string GetSubjectInfo(string subject)
        {
            if (this.students.Any(s => s.Subject == subject))
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Subject: {subject}");
                sb.AppendLine("Students:");
                foreach (var student in this.students.Where(s => s.Subject == subject))
                {
                    sb.AppendLine($"{student.FirstName} {student.LastName}");
                }

                return sb.ToString().Trim();
            }

            return "No students enrolled for the subject";
        }

        public string GetStudentsCount()
        {
            return $"{this.students.Count}";
        }

        public Student GetStudent(string firstName, string lastName)
        {
            return this.students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
        }
    }
}