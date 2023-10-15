namespace Vehicles.Models
{
    using Exceptions;

    public class Truck : Vehicle
    {
        private const double CONSUMPTION_INCREASE = 1.6;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity,
            fuelConsumption,
            CONSUMPTION_INCREASE, tankCapacity)
        {
        }

        public override void Refuel(double liters)
        {
            if (liters <= 0)
                throw new CannotFitFuelException(ExceptionMessages.FUEL_MUST_BE_POSITIVE_NUMBER);

            if (this.FuelQuantity + liters > this.TankCapacity)
                throw new CannotFitFuelException(string.Format(ExceptionMessages.CANNOT_FIT_FUEL_EXCEPTION_MESSAGE, liters));

            this.FuelQuantity += liters * 0.95;
        }
    }
}