namespace P03.Detail_Printer.Factories
{
    using Contracts;
    using Employees;
    using Employees.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validators;

    public class EmployeeFactory : IEmployeeFactory
    {
        public IEmployee CreateEmployee(params string[] args)
        {
            string jobPosition = args[0];
            string name = args[1];

            NameValidator.Validate(name);

            switch (jobPosition)
            {
                case nameof(Employee):
                    return new Employee(name);

                case nameof(Manager):
                    IReadOnlyCollection<string> documents = args.Skip(2).ToList();
                    DocumentsValidator.Validate(documents);
                    return new Manager(name, documents);

                case nameof(Developer):
                    ProgrammingLanguageValidator.Validate(args[2]);
                    return new Developer(name, args[2]);
            }

            throw new ArgumentException("Invalid job position!");
        }
    }
}