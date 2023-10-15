namespace Vehicles.Factories
{
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.Interfaces;

    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle CreateVehicle(string type, double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            IVehicle vehicle = type switch
            {
                "Car" => new Car(fuelQuantity, fuelConsumption, tankCapacity),
                "Truck" => new Truck(fuelQuantity, fuelConsumption, tankCapacity),
                "Bus" => new Bus(fuelQuantity, fuelConsumption, tankCapacity),
                _ => throw new InvalidVehicleTypeException()
            };

            return vehicle;
        }
    }
}