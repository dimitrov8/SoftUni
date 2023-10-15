using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Students_2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<Student> Students = new List<Student>();
            while ((input = Console.ReadLine()) != "end")
            {
                string[] studentInfo = input.Split(" ");
                string firstName = studentInfo[0];
                string lastName = studentInfo[1];
                int age = int.Parse(studentInfo[2]);
                string city = studentInfo[3];

                Student existingStudent =
                    Students.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);

                if (existingStudent != null)
                {
                    Students.Remove(existingStudent);
                }

                Student student = new Student(firstName, lastName, age, city);
                Students.Add(student);
            }

            string desiredCity = Console.ReadLine();

            List<Student> sortedStudents = Students.Where(x => x.City == desiredCity).ToList();

            foreach (Student student in sortedStudents)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} is {student.Age} years old.");
            }
        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public Student(string firstName, string lastName, int age, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            City = city;
        }
    }
}