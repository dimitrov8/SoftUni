namespace Vehicles.Models
{
    using Exceptions;

    public class Bus : Vehicle
    {
        private const double CONSUMPTION_INCREASE = 1.4;

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity,
            fuelConsumption, CONSUMPTION_INCREASE, tankCapacity)
        {
        }

        public string DriveEmpty(double distance)
        {
            double fuelNeeded = distance * (this.FuelConsumption - CONSUMPTION_INCREASE);

            if (this.FuelQuantity - fuelNeeded < 0)
                throw new InsufficientFuelException(string.Format(ExceptionMessages.INSUFFICIENT_FUEL_EXCEPTION_MESSAGE,
                    this.GetType().Name));

            this.FuelQuantity -= fuelNeeded;
            return $"{this.GetType().Name} travelled {distance} km";
        }
    }
}