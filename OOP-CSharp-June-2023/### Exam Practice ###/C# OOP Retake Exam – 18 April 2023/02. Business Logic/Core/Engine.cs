namespace EDriveRent.Core
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
            this.reader = new ConsoleReader();
            this.writer = new ConsoleWriter();
            this.controller = new Controller();
        }

        private const string REGISTER_USER_COMMAND = "RegisterUser";
        private const string UPLOAD_VEHICLE_COMMAND = "UploadVehicle";
        private const string ALLOW_ROUTE_COMMAND = "AllowRoute";
        private const string MAKE_TRIP_COMMAND = "MakeTrip";
        private const string REPAIR_VEHICLES_COMMAND = "RepairVehicles";
        private const string USERS_REPORT_COMMAND = "UsersReport";

        public void Run()
        {
            string input;
            while ((input = this.reader.ReadLine()) != "End")
            {
                string[] args = input.Split();

                try
                {
                    string result = string.Empty;
                    if (args[0] == REGISTER_USER_COMMAND)
                    {
                        result = this.RegisterUser(args);
                    }
                    else if (args[0] == UPLOAD_VEHICLE_COMMAND)
                    {
                        result = this.UploadVehicle(args);
                    }
                    else if (args[0] == ALLOW_ROUTE_COMMAND)
                    {
                        result = this.AddRoute(args);
                    }
                    else if (args[0] == MAKE_TRIP_COMMAND)
                    {
                        result = this.GoOnTrip(args);
                    }
                    else if (args[0] == REPAIR_VEHICLES_COMMAND)
                    {
                        result = this.controller.RepairVehicles(int.Parse(args[1]));
                    }
                    else if (args[0] == USERS_REPORT_COMMAND)
                    {
                        result = this.controller.UsersReport();
                    }

                    this.writer.WriteLine(result);
                }
                catch (ArgumentException argex)
                {
                    this.writer.WriteLine(argex.Message);
                }
            }
        }

        private string RegisterUser(string[] args)
        {
            string firstName = args[1];
            string lastName = args[2];
            string drivingLicenseNumber = args[3];

            return this.controller.RegisterUser(firstName, lastName, drivingLicenseNumber);
        }

        private string UploadVehicle(string[] args)
        {
            string vehicleType = args[1];
            string brand = args[2];
            string model = args[3];
            string licensePlateNumber = args[4];

            return this.controller.UploadVehicle(vehicleType, brand, model, licensePlateNumber);
        }

        private string AddRoute(string[] args)
        {
            string startPoint = args[1];
            string endPoint = args[2];
            double length = double.Parse(args[3]);

            return this.controller.AllowRoute(startPoint, endPoint, length);
        }

        private string GoOnTrip(string[] args)
        {
            string drivingLicenseNumber = args[1];
            string licensePlateNumber = args[2];
            string routeId = args[3];
            bool isAccidentHappened = bool.Parse(args[4]);

            return this.controller.MakeTrip(drivingLicenseNumber, licensePlateNumber, routeId, isAccidentHappened);
        }
    }
}