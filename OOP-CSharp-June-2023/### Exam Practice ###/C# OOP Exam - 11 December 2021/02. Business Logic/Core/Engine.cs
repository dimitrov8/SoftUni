namespace Gym.Core
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

                    if (input[0] == "AddGym")
                    {
                        string gymType = input[1];
                        string gymName = input[2];

                        result = this.controller.AddGym(gymType, gymName);
                    }
                    else if (input[0] == "AddEquipment")
                    {
                        string equipmentType = input[1];

                        result = this.controller.AddEquipment(equipmentType);
                    }
                    else if (input[0] == "InsertEquipment")
                    {
                        string gymName = input[1];
                        string equipmentType = input[2];

                        result = this.controller.InsertEquipment(gymName, equipmentType);
                    }
                    else if (input[0] == "AddAthlete")
                    {
                        string gymName = input[1];
                        string athleteType = input[2];
                        string athleteName = input[3];
                        string motivation = input[4];
                        int numberOfMedals = int.Parse(input[5]);

                        result = this.controller.AddAthlete(gymName, athleteType, athleteName, motivation, numberOfMedals);
                    }
                    else if (input[0] == "TrainAthletes")
                    {
                        string gymName = input[1];

                        result = this.controller.TrainAthletes(gymName);
                    }
                    else if (input[0] == "EquipmentWeight")
                    {
                        string gymName = input[1];

                        result = this.controller.EquipmentWeight(gymName);
                    }
                    else if (input[0] == "Report")
                    {
                        result = this.controller.Report();
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