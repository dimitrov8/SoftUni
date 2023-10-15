namespace P03.Detail_Printer.Employees
{
    using System;
    using System.Collections.Generic;

    public class Manager : Employee
    {
        public Manager(string name, IReadOnlyCollection<string> documents) : base(name)
        {
            this.Documents = documents;
        }

        public IReadOnlyCollection<string> Documents { get; private set; }

        public override string PrintEmployee()
        {
            string documentsList = string.Join(Environment.NewLine, this.Documents);
            return $"{base.PrintEmployee()}{Environment.NewLine}Documents:{Environment.NewLine}{documentsList}";
        }
    }
}