namespace P03.Detail_Printer.Employees
{
    using System;

    public class Developer : Employee
    {
        public Developer(string name, string programmingLanguage) : base(name)
        {
            this.ProgrammingLanguage = programmingLanguage;
        }

        public string ProgrammingLanguage { get; private set; }

        public override string PrintEmployee() => $"{base.PrintEmployee()}{Environment.NewLine}Programming Language: {this.ProgrammingLanguage}";
    }
}