namespace MiniORM.App;

using Data;
using Data.Entities;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniDbContext dbContext = new SoftUniDbContext(Config.ConnectionString);

        dbContext.Employees.Add(new Employee()
            {
                FirstName = "Test",
                LastName = "Testov",
                DepartmentId = dbContext.Departments.First().Id
            }
        );

        Employee newEmployee = dbContext
            .Employees.First(e => e.FirstName == "Test");

        // dbContext.Employees.Remove(newEmployee);

        dbContext.SaveChanges();
    }
}