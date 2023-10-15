using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._SoftUni_Exam_Results
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            var results = new Dictionary<string, List<int>>();
            var submissions = new Dictionary<string, int>();
            while ((input = Console.ReadLine()) != "exam finished")
            {
                string[] tokens = input.Split('-');
                string name = tokens[0];
                if (tokens[1] == "banned")
                {
                    results.Remove(name);
                    continue;
                }

                string language = tokens[1];
                int points = int.Parse(tokens[2]);

                if (!results.ContainsKey(name))
                {
                    results.Add(name, new List<int>() { points });
                }

                else if (results.ContainsKey(name))
                {
                    results[name].Add(points);
                }

                if (!submissions.ContainsKey(language))
                {
                    submissions.Add(language, 1);
                }
                else if (submissions.ContainsKey(language))
                {
                    submissions[language]++;
                }
            }

            Console.WriteLine("Results:");
            foreach (var participant in results.OrderByDescending(p => p.Value.Max())
                         .ThenBy(n => n.Key))
            {
                Console.WriteLine($"{participant.Key} | {participant.Value.Max()}");
            }

            Console.WriteLine("Submissions:");
            foreach (var submission in submissions.OrderByDescending(c => c.Value)
                         .ThenBy(s => s.Key))
            {
                Console.WriteLine($"{submission.Key} - {submission.Value}");
            }
        }
    }
}