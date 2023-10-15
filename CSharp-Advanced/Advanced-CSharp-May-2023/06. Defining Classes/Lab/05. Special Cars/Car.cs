using System.Linq;
using System.Text;

namespace Special_Cars
{
    public class Car
    {
        // Fields
        private string make;
        private string model;
        private int year;
        private double fuelQuantity;
        private double fuelConsumption;
        private Engine engine;
        private Tires[] tires;
        
        // Properties
        public string Make
        {
            get => make;
            set => make = value;
        }

        public string Model
        {
            get => model;
            set => model = value;
        }

        public int Year
        {
            get => year;
            set => year = value;
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

        public Engine Engine
        {
            get => engine;
            set => engine = value;
        }

        public Tires[] Tires
        {
            get => tires;
            set => tires = value;
        }

        // Constructors
        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption)
        {
            Make = make;
            Model = model;
            Year = year;
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
        }

        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption, Engine engine, Tires[] tires)
            : this(make, model, year, fuelQuantity, fuelConsumption)
        {
            Engine = engine;
            Tires = tires;
        }


        public bool isSpecial(Car car)
        {
            return car.FuelQuantity - car.fuelConsumption / 100 * 20 >= 0 &&
                   car.Year >= 2007 &&
                   car.engine.HorsePower > 330 &&
                   car.Tires.Select(x => x.Pressure).Sum() >= 9 &&
                   car.tires.Select(x => x.Pressure).Sum() <= 10;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Make: {make}");
            sb.AppendLine($"Model: {model}");
            sb.AppendLine($"Year: {year}");
            sb.AppendLine($"HorsePowers: {engine.HorsePower}");
            sb.AppendLine($"FuelQuantity: {fuelQuantity -= fuelConsumption / 100 * 20}");
            return sb.ToString().TrimEnd();
        }
    }
}