using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Student_Academy
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());

            Dictionary<string, List<double>> studentsData = new Dictionary<string, List<double>>();
            for (int i = 1; i <= rows; i++)
            {
                string studentName = Console.ReadLine();
                double grade = double.Parse(Console.ReadLine());

                if (studentsData.ContainsKey(studentName))
                {
                    studentsData[studentName].Add(grade);
                    continue;
                }

                studentsData.Add(studentName, new List<double>() { grade });
            }

            foreach (var student in studentsData.Where(g => g.Value.Average() >= 4.50))
            {
                Console.WriteLine($"{student.Key} -> {student.Value.Average():F2}");
            }
        }
    }
}