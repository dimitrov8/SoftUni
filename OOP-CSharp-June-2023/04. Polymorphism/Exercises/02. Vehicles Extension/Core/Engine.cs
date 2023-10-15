namespace Vehicles.Core
{
    using Exceptions;
    using Factories;
    using Factories.Interfaces;
    using Interfaces;
    using IO.Interfaces;
    using Models;
    using Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IVehicleFactory factory;
        private readonly ICollection<IVehicle> vehicles;

        private Engine()
        {
            this.factory = new VehicleFactory();
            this.vehicles = new HashSet<IVehicle>();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            this.vehicles.Add(this.CreateVehicleFactory());
            this.vehicles.Add(this.CreateVehicleFactory());
            this.vehicles.Add(this.CreateVehicleFactory());

            int nOfCommands = int.Parse(this.reader.ReadLine());

            for (int i = 0; i < nOfCommands; i++)
                try
                {
                    this.ReceiveCommands();
                }
                catch (InsufficientFuelException ife)
                {
                    this.writer.WriteLine(ife.Message);
                }
                catch (InvalidVehicleTypeException ivt)
                {
                    this.writer.WriteLine(ivt.Message);
                }
                catch (FuelMustBePositiveNumberException fpn)
                {
                    this.writer.WriteLine(fpn.Message);
                }
                catch (CannotFitFuelException cfe)
                {
                    this.writer.WriteLine(cfe.Message);
                }

            this.PrintAllVehicles();
        }

        private IVehicle CreateVehicleFactory()
        {
            string[] input = this.reader.ReadLine().Split();
            string type = input[0];
            double fuelQuantity = double.Parse(input[1]);
            double fuelConsumption = double.Parse(input[2]);
            double tankCapacity = double.Parse(input[3]);
            IVehicle vehicle = this.factory.CreateVehicle(type, fuelQuantity, fuelConsumption, tankCapacity);

            return vehicle;
        }

        private void ReceiveCommands()
        {
            string[] commandInfo = this.reader.ReadLine().Split();
            string mainCommand = commandInfo[0];
            string vehicleType = commandInfo[1];
            double args = double.Parse(commandInfo[2]);

            IVehicle vehicle = this.vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);
            if (vehicle == null)
                throw new InvalidVehicleTypeException();
            if (mainCommand == "Drive")
                this.writer.WriteLine(vehicle.Drive(args));
            else if (mainCommand == "DriveEmpty")
            {
                Bus bus = (Bus)vehicle;
                this.writer.WriteLine(bus.DriveEmpty(args));
            }
            else if (mainCommand == "Refuel")
                vehicle.Refuel(args);
        }

        private void PrintAllVehicles()
        {
            foreach (IVehicle vehicle in this.vehicles)
                this.writer.WriteLine(vehicle.ToString());
        }
    }
}