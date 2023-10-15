using System;

namespace DefiningClasses
{
    public class Car
    {
        public string Model { get; set; }
        private double FuelAmount { get; set; }
        private double FuelConsumption { get; set; }
        private double TraveledDistance { get; set; }
        
        public Car(string model, double fuelAmount, double fuelConsumption)
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelConsumption = fuelConsumption;
            TraveledDistance = 0;
        }

        public void Drive(double distance)
        {
            double fuelNeeded = FuelConsumption * distance;
            if (FuelAmount >= fuelNeeded)
            {
                FuelAmount -= fuelNeeded;
                TraveledDistance += distance;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }

        public override string ToString()
        {
            return $"{Model} {FuelAmount:F2} {TraveledDistance}";
        }
    }
}