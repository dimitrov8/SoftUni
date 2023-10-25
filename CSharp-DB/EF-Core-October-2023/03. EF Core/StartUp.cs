namespace SoftUni;

using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Globalization;
using System.Text;

public class StartUp
{
    static void Main(string[] args)
    {
        var context = new SoftUniContext();

        //string result = ...(context);
        //Console.WriteLine(result);
    }

    // 03. Employees Full Information
    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        var employees = context.Employees
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.MiddleName,
                e.JobTitle,
                e.Salary
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}");
        }

        return sb.ToString().TrimEnd();
    }

    // 04. Employees with Salary Over 50 000 
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        var employees = context.Employees
            .OrderBy(e => e.FirstName)
            .Where(e => e.Salary > 50000)
            .Select(e => new
            {
                e.FirstName,
                e.Salary
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} - {e.Salary:F2}");
        }

        return sb.ToString().TrimEnd();
    }

    // 05. Employees from Research and Development
    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e => e.Department.Name == "Research and Development")
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                DepartmentName = e.Department.Name,
                e.Salary
            })
            .OrderBy(e => e.Salary)
            .ThenByDescending(e => e.FirstName)
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:F2}");
        }

        return sb.ToString().TrimEnd();
    }

    // 06. Adding a New Address and Updating Employee 
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        var newAddress = new Address
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };

        var employee = context.Employees
            .FirstOrDefault(e => e.LastName == "Nakov");

        employee.Address = newAddress;

        context.SaveChanges();

        string[] employeeAddresses = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address!.AddressText)
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, employeeAddresses);
    }

    // 07. Employees and Projects 
    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        var employees = context.Employees
            .Take(10)
            .Select(e => new
            {
                EmployeeName = e.FirstName + ' ' + e.LastName,
                ManagerName = e.Manager!.FirstName + ' ' + e.Manager.LastName,
                Projects = e.EmployeesProjects
                    .Where(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003)
                    .Select(ep => new
                    {
                        ep.Project.Name,
                        StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                        EndDate = ep.Project.EndDate.HasValue 
                        ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                        : "not finished"
                    })
                    .ToArray()
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.EmployeeName} - Manager: {e.ManagerName}");

            foreach (var p in e.Projects)
            {
                sb.AppendLine($"--{p.Name} - {p.StartDate} - {p.EndDate}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    // 08. Addresses by Town 
    public static string GetAddressesByTown(SoftUniContext context)
    {
        var addresses = context.Addresses
            .OrderByDescending(a => a.Employees.Count)
            .ThenBy(a => a.Town!.Name)
            .ThenBy(a => a.AddressText)
            .Take(10)
            .Select(a => new
            {
                a.AddressText,
                TownName = a.Town!.Name,
                EmployeeCount = a.Employees.Count
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var a in addresses)
        {
            sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeeCount} employees");
        }

        return sb.ToString().TrimEnd();
    }

    // 09. Employee 147 
    public static string GetEmployee147(SoftUniContext context)
    {
        var employee = context.Employees
            .Where(e => e.EmployeeId == 147)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                Projects = e.EmployeesProjects
                    .OrderBy(ep => ep.Project.Name)
                    .Select(ep => ep.Project.Name)
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var e in employee)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}")
                .AppendLine(string.Join(Environment.NewLine, e.Projects));
        }

        return sb.ToString().TrimEnd();
    }

    // 10. Departments with More Than 5 Employees 
    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var departments = context.Departments
            .Where(d => d.Employees.Count > 5)
            .OrderBy(d => d.Employees.Count)
            .ThenBy(d => d.Name)
            .Select(d => new
            {
                DepartmentName = d.Name,
                ManagerName = d.Manager.FirstName + ' ' + d.Manager.LastName,
                Employees = d.Employees
                    .Select(e => new
                    {
                        EmployeeName = e.FirstName + ' ' + e.LastName,
                        e.JobTitle
                    })
                    .ToArray()
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var d in departments)
        {
            sb.AppendLine($"{d.DepartmentName} - {d.ManagerName}");

            foreach (var e in d.Employees)
            {
                sb.AppendLine($"{e.EmployeeName} - {e.JobTitle}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    // 11. Find Latest 10 Projects 
    public static string GetLatestProjects(SoftUniContext context)
    {
        var projects = context.Projects
            .OrderByDescending(p => p.StartDate)
            .Take(10)
            .OrderBy(p => p.Name)
            .Select(p => new
            {
                p.Name,
                p.Description,
                StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var p in projects)
        {
            sb.AppendLine(p.Name)
                .AppendLine(p.Description)
                .AppendLine(p.StartDate);
        }

        return sb.ToString().TrimEnd();
    }

    // 12. Increase Salaries
    public static string IncreaseSalaries(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e =>
                e.Department.Name == "Engineering" ||
                e.Department.Name == "Tool Design" ||
                e.Department.Name == "Marketing" ||
                e.Department.Name == "Information Services")
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                Salary = e.Salary * (decimal)1.12
            })
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
        }

        return sb.ToString().TrimEnd();
    }

    // 13. Find Employees by First Name Starting with "Sa"
    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e => e.FirstName.Substring(0, 2).ToLower() == "sa")
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                e.Salary
            })
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
        }

        return sb.ToString().TrimEnd();
    }

    // 14. Delete Project by Id 
    public static string DeleteProjectById(SoftUniContext context)
    {
        var projectToDelete = context.Projects.Find(2);

        context.Projects.Remove(projectToDelete!);

        context.SaveChanges();

        var projects = context.Projects
            .Take(10)
            .Select(p => new
            {
                p.Name
            })
            .AsNoTracking()
            .ToArray();

        var sb = new StringBuilder();

        foreach (var p in projects)
        {
            sb.AppendLine(p.Name);
        }

        return sb.ToString().TrimEnd();
    }

    // 15. Remove Town
    public static string RemoveTown(SoftUniContext context)
    {
        var townToRemove = context.Towns
            .FirstOrDefault(t => t.Name == "Seattle");

        Address[] addressesToRemove = context.Addresses
            .Where(a => a.Town!.Name == townToRemove!.Name)
            .ToArray();

        Employee[] employees = context.Employees
            .Where(e => e.Address!.Town!.Name == townToRemove!.Name)
            .ToArray();

        foreach (var e in employees)
        {
            e.AddressId = null;
        }

        context.Addresses.RemoveRange(addressesToRemove);
        context.Towns.Remove(townToRemove!);

        context.SaveChanges();

        return $"{addressesToRemove.Length} addresses in {townToRemove.Name} were deleted";
    }
}