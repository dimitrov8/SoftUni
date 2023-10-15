using System;

namespace NeedForSpeed
{
    public class Vehicle
    {
        public Vehicle(int horsePower, double fuel)
        {
            this.HorsePower = horsePower;
            this.Fuel = fuel;
            this.DefaultFuelConsumption = 1.25;
        }
        
        public double DefaultFuelConsumption { get; set; }
        public virtual double FuelConsumption { get; set; }
        public double Fuel { get; set; }
        public int HorsePower { get; set; }

        public virtual void Drive(double kilometers)
        {
            if (this.Fuel >= kilometers * this.FuelConsumption)
            {
                this.Fuel -= kilometers * this.FuelConsumption;
                Console.WriteLine($"Fuel left in the tank: {this.Fuel:F2}");
            }
            else
            {
                Console.WriteLine("Not enough fuel for this trip!");
                Console.WriteLine($"Current fuel in the tank: {this.Fuel:F2}");
                Console.WriteLine($"Fuel needed: {kilometers * this.FuelConsumption:F2}");
            }
        }
    }
}