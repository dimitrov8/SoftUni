namespace Formula1.Core
{
    using Contracts;
    using IO;
    using IO.Contracts;
    using System;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IController controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
        }

        public void Run()
        {
            while (true)
            {
                string[] input = this.reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }

                try
                {
                    string result = string.Empty;

                    if (input[0] == "CreatePilot")
                    {
                        string fullName = input[1];

                        result = this.controller.CreatePilot(fullName);
                    }
                    else if (input[0] == "CreateCar")
                    {
                        string type = input[1];
                        string model = input[2];
                        int horsePower = int.Parse(input[3]);
                        double engineDisplacement = double.Parse(input[4]);

                        result = this.controller.CreateCar(type, model, horsePower, engineDisplacement);
                    }
                    else if (input[0] == "CreateRace")
                    {
                        string name = input[1];
                        int laps = int.Parse(input[2]);

                        result = this.controller.CreateRace(name, laps);
                    }
                    else if (input[0] == "AddCarToPilot")
                    {
                        string pilotName = input[1];
                        string carModel = input[2];

                        result = this.controller.AddCarToPilot(pilotName, carModel);
                    }
                    else if (input[0] == "AddPilotToRace")
                    {
                        string raceName = input[1];
                        string pilotName = input[2];

                        result = this.controller.AddPilotToRace(raceName, pilotName);
                    }
                    else if (input[0] == "StartRace")
                    {
                        string raceName = input[1];

                        result = this.controller.StartRace(raceName);
                    }
                    else if (input[0] == "RaceReport")
                    {
                        result = this.controller.RaceReport();
                    }
                    else if (input[0] == "PilotReport")
                    {
                        result = this.controller.PilotReport();
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}