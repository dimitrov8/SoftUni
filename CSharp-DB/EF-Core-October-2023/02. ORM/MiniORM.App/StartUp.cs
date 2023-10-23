using MiniORM.App.Data;
using MiniORM.App.Data.Entities;

var context = new SoftUniDbContext(Config.CONNECTION_STRING);
context.Employees.Add(new Employee
{
    FirstName = "Alex",
    LastName = "Smith",
    DepartmentId = context.Departments.First().Id,
    IsEmployed = true
});

var employee = context.Employees.Last();

context.SaveChanges();