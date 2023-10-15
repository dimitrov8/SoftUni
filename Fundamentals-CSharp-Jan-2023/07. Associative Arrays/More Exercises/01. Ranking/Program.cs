using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Dictionary<string, string> courseNameAndPassword = new Dictionary<string, string>();
            while ((input = Console.ReadLine()) != "end of contests")
            {
                string[] tokens = input.Split(":");
                courseNameAndPassword.Add(tokens[0], tokens[1]);
            }

            Dictionary<string, Dictionary<string, int>>
                studentsInfo = new Dictionary<string, Dictionary<string, int>>();
            while ((input = Console.ReadLine()) != "end of submissions")
            {
                string[] tokens = input.Split("=>");
                string courseInput = tokens[0];
                string coursePasswordInput = tokens[1];
                string student = tokens[2];
                int coursePoints = int.Parse(tokens[3]);

                // If student input course and password is valid
                if (courseNameAndPassword.ContainsKey(courseInput) &&
                    courseNameAndPassword.ContainsValue(coursePasswordInput))
                {
                    // If Student is not in the course
                    if (!studentsInfo.ContainsKey(student))
                    {
                        // Add student, course and points to dictionary
                        studentsInfo.Add(student, new Dictionary<string, int> { { courseInput, coursePoints } });
                    }
                    else // If Student is in the course
                    {
                        // If we receive the same course and same student and the coursePoints are higher than before
                        // Add the coursePoints to the existing points
                        if (studentsInfo[student].ContainsKey(courseInput) &&
                            studentsInfo[student][courseInput] < coursePoints)
                        {
                            studentsInfo[student][courseInput] = coursePoints;
                        }
                        // If we don't have the current course in the dictionary for the student, add it
                        else if (!studentsInfo[student].ContainsKey(courseInput))
                        {
                            studentsInfo[student].Add(courseInput, coursePoints);
                        }
                    }
                }
            }

            var bestCandidate = studentsInfo.OrderByDescending(s => s.Value.Values.Sum()).First();

            Console.WriteLine(
                $"Best candidate is {bestCandidate.Key} with total {bestCandidate.Value.Values.Sum()} points.");
            Console.WriteLine("Ranking:");

            foreach (var student in studentsInfo.Keys.OrderBy(n => n))
            {
                Console.WriteLine(student);
                foreach (var courseInfo in studentsInfo[student].OrderByDescending(p => p.Value).ThenBy(c => c.Key))
                {
                    Console.WriteLine($"#  {courseInfo.Key} -> {courseInfo.Value}");
                }
            }
        }
    }
}