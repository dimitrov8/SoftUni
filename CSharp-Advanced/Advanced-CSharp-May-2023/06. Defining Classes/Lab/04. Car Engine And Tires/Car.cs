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
        private Engine engine;
        private Tire[] tires;

        private Car()
        {
            make = "VW";
            model = "Golf";
            year = 2025;
            fuelQuantity = 200;
            fuelConsumption = 10;
        }

        private Car(string make, string model, int year) : this()
        {
            this.make = make;
            this.model = model;
            this.year = year;
        }

        private Car(string make, string model, int year, double fuelQuantity, double fuelConsumption) : this(make, model, year)
        {
            this.fuelQuantity = fuelQuantity;
            this.fuelConsumption = fuelConsumption;
        }

        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption, Engine engine, Tire[] tires)
            : this(make, model, year, fuelQuantity, fuelConsumption)
        {
            this.engine = engine;
            this.tires = tires;
        }

        public string Make
        {
            get => this.make;
            set => this.make = value;
        }

        public string Model
        {
            get => this.model;
            set => this.model = value;
        }

        public int Year
        {
            get => this.year;
            set => this.year = value;
        }

        public double FuelQuantity
        {
            get => this.FuelQuantity;
            set => this.fuelQuantity = value;
        }

        public double FuelConsumption
        {
            get => this.fuelConsumption;
            set => this.fuelConsumption = value;
        }

        public Engine Engine
        {
            get => this.engine;
            set => this.engine = value;
        }

        public Tire[] Tires
        {
            get => this.tires;
            set => this.tires = value;
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Make: {make}");
            sb.AppendLine($"Model: {model}");
            sb.AppendLine($"Year: {year}");
            sb.AppendLine($"Fuel Quantity: {fuelQuantity:F2}");
            sb.AppendLine($"Fuel Consumption: {fuelConsumption:F2}");
            sb.AppendLine($"Horse Power: {engine.HorsePower}");
            sb.AppendLine($"Cubic Capacity: {engine.CubicCapacity}");
            return sb.ToString();
        }
    }
}