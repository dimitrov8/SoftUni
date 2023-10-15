namespace RobotService.Core
{
    using Contracts;
    using IO;
    using IO.Contracts;
    using System;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IController controller;

        public Engine()
        {
            this.reader = new Reader();
            this.writer = new Writer();
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

                    if (input[0] == "CreateRobot")
                    {
                        string model = input[1];
                        string typeName = input[2];

                        result = this.controller.CreateRobot(model, typeName);
                    }
                    else if (input[0] == "CreateSupplement")
                    {
                        string typeName = input[1];

                        result = this.controller.CreateSupplement(typeName);
                    }
                    else if (input[0] == "UpgradeRobot")
                    {
                        string model = input[1];
                        string supplementTypeName = input[2];

                        result = this.controller.UpgradeRobot(model, supplementTypeName);
                    }
                    else if (input[0] == "RobotRecovery")
                    {
                        string model = input[1];
                        int minutes = int.Parse(input[2]);

                        result = this.controller.RobotRecovery(model, minutes);
                    }
                    else if (input[0] == "PerformService")
                    {
                        string serviceName = input[1];
                        int interfaceStandard = int.Parse(input[2]);
                        int totalPowerNeeded = int.Parse(input[3]);

                        result = this.controller.PerformService(serviceName, interfaceStandard, totalPowerNeeded);
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