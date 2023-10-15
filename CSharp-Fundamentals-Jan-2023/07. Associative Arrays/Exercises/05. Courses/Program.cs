using System;
using System.Collections.Generic;

namespace _05._Courses
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Dictionary<string, List<string>> courses = new Dictionary<string, List<string>>();
            while ((input = Console.ReadLine()) != "end")
            {
                string[] tokens = input.Split(": ");
                string courseName = tokens[0];
                string student = tokens[1];

                if (!courses.ContainsKey(courseName))
                {
                    courses.Add(courseName, new List<string>());
                    courses[courseName].Add(student);
                    continue;
                }

                courses[courseName].Add(student);
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Key.TrimEnd()}: {course.Value.Count}");
                foreach (string student in course.Value)
                {
                    Console.WriteLine($"-- {student}");
                }
            }
        }
    }
}