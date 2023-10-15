using System.Text;

namespace CarManufacturer
{
    class Car
    {
        private string Make {get; set; }
        private string Model {get; set; }
        private int Year {get; set; }
        private double FuelQuantity {get; set; }
        private double FuelConsumption {get; set; }

        public Car()
        {
            Make = "VW";
            Model = "Golf";
            Year = 2025;
            FuelQuantity = 200;
            FuelConsumption = 10;
        }
        
        public Car(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
        }

        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption) : this(make, model, year)
        {
            FuelQuantity= fuelQuantity;
            FuelConsumption = fuelConsumption;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Make: {Make}");
            sb.AppendLine($"Model: {Model}");
            sb.AppendLine($"Year: {Year}");
            sb.AppendLine($"Fuel Quantity: {FuelQuantity:F2}");
            sb.AppendLine($"Fuel Consumption: {FuelConsumption:F2}");
            return sb.ToString();
        }
    }
}