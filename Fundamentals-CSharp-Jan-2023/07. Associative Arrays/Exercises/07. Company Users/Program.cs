using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Company_Users
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Dictionary<string, List<string>> companyData = new Dictionary<string, List<string>>();
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" -> ");
                string companyName = tokens[0];
                string employeeId = tokens[1];

                if (companyData.Any(c => c.Key.Contains(companyName))
                    && companyData[companyName].Any(e => e.Contains(employeeId)))
                {
                    continue;
                }

                if (companyData.ContainsKey(companyName) && !companyData[companyName].Any(e => e.Contains(employeeId)))
                {
                    companyData[companyName].Add(employeeId);
                    continue;
                }

                if (!companyData.ContainsKey(companyName))
                    companyData.Add(companyName, new List<string>() { employeeId });
            }

            foreach (var company in companyData)
            {
                Console.WriteLine(company.Key);

                foreach (var employee in company.Value)
                    Console.WriteLine($"-- {employee}");
            }
        }
    }
}