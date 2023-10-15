using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Average_Student_Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            int nOfStudents = int.Parse(Console.ReadLine());
            var dict = new Dictionary<string, List<decimal>>();
            for (int i = 0; i < nOfStudents; i++)
            {
                string[] currStudentInfo = Console.ReadLine().Split();
                string studentName = currStudentInfo[0];
                decimal studentGrade = decimal.Parse(currStudentInfo[1]);

                if (!dict.ContainsKey(studentName))
                {
                    dict.Add(studentName, new List<decimal>() { studentGrade });
                }
                else if (dict.ContainsKey(studentName))
                {
                    dict[studentName].Add(studentGrade);
                }
            }

            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} -> {string.Join(' ', kvp.Value.Select(x => x.ToString("F2")))} (avg: {kvp.Value.Average():F2})");
            }
        }
    }
}