namespace UniversityCompetition.Core
{
    using Contracts;
    using IO;
    using IO.Contracts;
    using System;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private IController controller;

        public Engine()
        {
            this.reader = new Reader();
            this.writer = new Writer();
            //this.controller = new Controller();
        }

        public void Run()
        {
            while (true)
            {
                string[] input = this.reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }

                try
                {
                    string result = string.Empty;

                    if (input[0] == "AddSubject")
                    {
                        string subjectName = input[1];
                        string category = input[2];

                        result = this.controller.AddSubject(subjectName, category);
                    }
                    else if (input[0] == "AddUniversity")
                    {
                        string universityName = input[1];
                        string category = input[2];
                        int capacity = int.Parse(input[3]);
                        var requiredSubjects = input[4].Split(",").ToList();

                        result = this.controller.AddUniversity(universityName, category, capacity, requiredSubjects);
                    }
                    else if (input[0] == "AddStudent")
                    {
                        string firstName = input[1];
                        string lastName = input[2];

                        result = this.controller.AddStudent(firstName, lastName);
                    }
                    else if (input[0] == "TakeExam")
                    {
                        int studentId = int.Parse(input[1]);
                        int subjectId = int.Parse(input[2]);

                        result = this.controller.TakeExam(studentId, subjectId);
                    }
                    else if (input[0] == "ApplyToUniversity")
                    {
                        string studentName = input[1] + " " + input[2];
                        string universityName = input[3];

                        result = this.controller.ApplyToUniversity(studentName, universityName);
                    }
                    else if (input[0] == "UniversityReport")
                    {
                        int universityId = int.Parse(input[1]);

                        result = this.controller.UniversityReport(universityId);
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}