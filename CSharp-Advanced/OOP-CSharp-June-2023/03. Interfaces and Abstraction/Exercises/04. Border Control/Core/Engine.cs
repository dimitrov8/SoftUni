namespace BorderControl.Core
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

        private readonly List<string> ids;

        private Engine()
        {
            this.ids = new List<string>();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            this.ReceiveInformation();
            this.DetainFakeIds();
        }


        private void ReceiveInformation()
        {
            string input;
            while ((input = this.reader.ReadLine()) != "End")
                this.ids.Add(input!.Split(' ').Last());
        }

        private void DetainFakeIds()
        {
            string fakeIdsLastDigits = this.reader.ReadLine();
            this.writer.WriteLine(string.Join($"{Environment.NewLine}",
                this.ids.Where(id => id.EndsWith(fakeIdsLastDigits))));
        }
    }
}
