namespace P03.Detail_Printer.Printer
{
    using Employees.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DetailsPrinter
    {
        private IEnumerable<IEmployee> employees;

        public DetailsPrinter(IEnumerable<IEmployee> employees)
        {
            this.Employees = employees;
        }

        public IEnumerable<IEmployee> Employees
        {
            get => this.employees;
            private set
            {
                if (!value.Any())
                    throw new ArgumentException("Cannot print empty list of employees!");

                this.employees = value;
            }
        }

        public void PrintDetails()
        {
            int index = 0;
            Console.WriteLine("--- Printing details ---");

            foreach (IEmployee employee in this.employees)
            {
                Console.WriteLine(employee.PrintEmployee());
                if (index - this.employees.Count() != -1)
                {
                    Console.WriteLine();
                }

                index++;
            }
        }
    }
}