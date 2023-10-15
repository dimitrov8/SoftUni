namespace P03.Detail_Printer.Employees
{
    using Contracts;
    using System;

    public class Employee : IEmployee
    {
        public Employee(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public virtual string PrintEmployee() => $"Position: {this.GetType().Name}{Environment.NewLine}Name: {this.Name}";
    }
}