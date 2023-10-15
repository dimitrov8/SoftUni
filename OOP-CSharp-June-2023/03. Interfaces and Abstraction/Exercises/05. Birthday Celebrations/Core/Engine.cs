namespace BirthdayCelebrations.Core
{
    using Interfaces;
    using IO.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly List<string> birthDaysList;

        private Engine()
        {
            this.birthDaysList = new List<string>();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            this.ReceiveInformation();
            this.PrintAllBirthDays();
        }

        private void ReceiveInformation()
        {
            string input;
            while ((input = this.reader.ReadLine()) != "End")
            {
                string[] tokens = input!.Split(' ');
                if (tokens[0] == "Citizen" || tokens[0] == "Pet")
                    this.birthDaysList.Add(tokens.Last());
            }
        }

        private void PrintAllBirthDays()
        {
            string specificYear = this.reader.ReadLine();
            this.writer.WriteLine(string.Join($"{Environment.NewLine}",
                this.birthDaysList.Where(b => b.EndsWith(specificYear))));
        }
    }
}
