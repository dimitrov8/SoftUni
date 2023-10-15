namespace Vehicles.Models
{
    using Exceptions;
    using Interfaces;

    public class Vehicle : IVehicle
    {
        private double fuelQuantity;

        public Vehicle(double fuelQuantity, double fuelConsumption, double consumptionIncrease, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption + consumptionIncrease;
        }

        public double FuelQuantity
        {
            get => this.fuelQuantity;
            protected set => this.fuelQuantity = value > this.TankCapacity ? 0 : value;
        }

        public double FuelConsumption { get; private set; }

        public double TankCapacity { get; private set; }

        public string Drive(double distance)
        {
            if (this.FuelQuantity - distance * this.FuelConsumption < 0)
                throw new InsufficientFuelException(string.Format(ExceptionMessages.INSUFFICIENT_FUEL_EXCEPTION_MESSAGE,
                    this.GetType().Name));

            this.FuelQuantity -= distance * this.FuelConsumption;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double liters)
        {
            if (liters <= 0)
                throw new FuelMustBePositiveNumberException(ExceptionMessages.FUEL_MUST_BE_POSITIVE_NUMBER);

            if (this.FuelQuantity + liters > this.TankCapacity)
                throw new CannotFitFuelException(string.Format(ExceptionMessages.CANNOT_FIT_FUEL_EXCEPTION_MESSAGE,
                    liters));

            this.FuelQuantity += liters;
        }

        public override string ToString() => $"{this.GetType().Name}: {this.FuelQuantity:F2}";
    }
}