namespace ExplicitInterfaces.Core
{
    using Interfaces;
    using IO.Interfaces;
    using Models;
    using Models.Interfaces;
    using System;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private Citizen citizen;
        private IResident resident;
        private IPerson person;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string input;
            while ((input = this.reader.ReadLine()) != "End")
            {
                string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                string country = tokens[1];
                int age = int.Parse(tokens[2]);

                this.citizen = new Citizen(name, age);
                this.resident = this.citizen;
                this.person = this.citizen;

                this.writer.WriteLine(this.person.GetName());
                this.writer.WriteLine(this.resident.GetName());
            }
        }
    }
}