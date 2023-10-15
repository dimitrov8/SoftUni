using System;
using System.Text;

namespace CarManufacturer
{
    class Car
    {
        private string make;
        private string model;
        private int year;
        private double fuelQuantity;
        private double fuelConsumption;

        public string Make
        {
            get =>  make;
            init => make = value;
        }

        public string Model
        {
            get => model;
            init => model = value;
        }

        public int Year
        {
            get => year;
            init => year = value;
        }

        public double FuelQuantity
        {
            get => fuelQuantity;
            set => fuelQuantity = value;
        }

        public double FuelConsumption
        {
            get => fuelConsumption;
            set => fuelConsumption = value;
        }

        public void Drive(double distance)
        {
            double leftFuel = fuelQuantity - (distance * fuelConsumption);
            if (leftFuel < 0)
            {
                Console.WriteLine("Not enough fuel to perform this trip!");
                return;
            }

            fuelQuantity -= leftFuel;
        }

        public string WhoAmI()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Make: {make}");
            sb.AppendLine($"Model: {model}");
            sb.AppendLine($"Year {year}");
            sb.AppendLine($"Fuel: {fuelQuantity:F2}");
            return sb.ToString();
        }
    }
}