namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double CONSUMPTION_INCREASE = 1.6;

        public Truck(double fuelQuantity, double fuelConsumption) : base(fuelQuantity, fuelConsumption,
            CONSUMPTION_INCREASE)
        {
        }

        public override void Refuel(double liters) => base.Refuel(liters * 0.95);
    }
}