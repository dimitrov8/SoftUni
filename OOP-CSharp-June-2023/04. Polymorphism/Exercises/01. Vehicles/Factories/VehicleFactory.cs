namespace Vehicles.Factories
{
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.Interfaces;

    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle CreateVehicle(string type, double fuelQuantity, double fuelConsumption)
        {
            IVehicle vehicle = type switch
            {
                "Car" => new Car(fuelQuantity, fuelConsumption),
                "Truck" => new Truck(fuelQuantity, fuelConsumption),
                _ => throw new InvalidVehicleTypeException()
            };

            return vehicle;
        }
    }
}