namespace P03.Detail_Printer.Employees.Contracts
{
    public interface IEmployee
    {
        string Name { get; }

        string PrintEmployee();
    }
}