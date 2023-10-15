using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Company_Roster
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Employee> employeesList = new List<Employee>();
            for (int i = 1; i <= n; i++)
            {
                string[] employeeInfo = Console.ReadLine().Split();
                string employeeName = employeeInfo[0];
                decimal employeeSalary = decimal.Parse(employeeInfo[1]);
                string employeeDepartment = employeeInfo[2];
                employeesList.Add(new Employee(employeeName, employeeSalary, employeeDepartment));
            }

            CalculateHighestAverageSalaryDepartment(employeesList);
        }

        static void CalculateHighestAverageSalaryDepartment(List<Employee> employeesList)
        {
            var highestAvgSalaryDept = employeesList
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key, AvgSalary = g.Average(e => e.Salary),
                    Employees = g.OrderByDescending(e => e.Salary)
                })
                .OrderByDescending(e => e.AvgSalary)
                .First();

            // 1. GroupBy(e => e.Department): Groups the employees by their department,
            // creating a list of groups where each group contains all employees in a particular department

            // 2. Select(g => new { Department = g.Key, AvgSalary = g.Average(e => e.Salary), Employees = g.OrderByDescending(e => e.Salary) }):
            // Transforms each group in the list of groups created by GroupBy into a new object that contains the following properties:
            // P1: Department: This is the department name for the group, which is obtained from the group's key (g.Key).
            // P2: AvgSalary: This is the average salary for the employees in the group, which is obtained by calling Average
            // P3: Employees: This is a list of employees in the group, sorted by their salary in descending order

            // 3. OrderByDescending(e => e.AvgSalary): Orders the list of transformed groups created by Select in descending order by the AvgSalary property of each group.
            // 4. First(): Returns the first group in the ordered list, which has the highest average salary.

            Console.WriteLine($"Highest Average Salary: {highestAvgSalaryDept.Department}");
            foreach (Employee employee in highestAvgSalaryDept.Employees)
            {
                Console.WriteLine(employee.ToString());
            }
        }
    }

    public class Employee
    {
        private string Name { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }

        public Employee(string employeeName, decimal employeeSalary, string employeeDepartment)
        {
            Name = employeeName;
            Salary = employeeSalary;
            Department = employeeDepartment;
        }

        public override string ToString() =>
            $"{Name} {Salary:F2}";
    }
}