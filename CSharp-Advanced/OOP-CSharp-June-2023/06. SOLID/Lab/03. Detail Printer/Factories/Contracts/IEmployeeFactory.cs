namespace P03.Detail_Printer.Factories.Contracts
{
    using Employees.Contracts;

    public interface IEmployeeFactory
    {
        IEmployee CreateEmployee(params string[] args);
    }
}