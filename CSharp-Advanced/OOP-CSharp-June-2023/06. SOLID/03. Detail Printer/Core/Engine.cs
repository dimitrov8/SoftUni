namespace P03.Detail_Printer.Core
{
    using Contracts;
    using Employees.Contracts;
    using Factories;
    using Factories.Contracts;
    using IO.Contracts;
    using Printer;
    using System;
    using System.Collections.Generic;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IEmployeeFactory employeeFactory;
        private readonly ICollection<IEmployee> employees;

        private Engine()
        {
            this.employeeFactory = new EmployeeFactory();
            this.employees = new List<IEmployee>();
        }

        public Engine(IReader reader, IWriter writer)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string input;
            while ((input = this.reader.ReadLine()) != "End")
            {
                string[] args = input.Split();
                try
                {
                    if (args.Length < 2)
                        throw new ArgumentException("Invalid input!");

                    IEmployee employee = this.employeeFactory.CreateEmployee(args);
                    this.employees.Add(employee);
                }

                catch (ArgumentException argex)
                {
                    this.writer.WriteLine(argex.Message);
                }
            }

            try
            {
                var printer = new DetailsPrinter(this.employees);
                printer.PrintDetails();
            }
            catch (ArgumentException argex)
            {
                Console.WriteLine(argex.Message);
            }
        }
    }
}