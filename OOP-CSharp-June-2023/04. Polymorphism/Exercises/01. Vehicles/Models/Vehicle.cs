namespace Vehicles.Models
{
    using Exceptions;
    using Interfaces;

    public class Vehicle : IVehicle
    {
        public Vehicle(double fuelQuantity, double fuelConsumption, double consumptionIncrease)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption + consumptionIncrease;
        }

        public double FuelQuantity { get; private set; }

        public double FuelConsumption { get; private set; }

        public string Drive(double distance)
        {
            if (this.FuelQuantity - distance * this.FuelConsumption < 0)
                throw new InsufficientFuelException(string.Format(ExceptionMessages.INSUFFICIENT_FUEL_EXCEPTION_MESSAGE,
                    this.GetType().Name));

            this.FuelQuantity -= distance * this.FuelConsumption;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double liters) => this.FuelQuantity += liters;

        public override string ToString() => $"{this.GetType().Name}: {this.FuelQuantity:F2}";
    }
}