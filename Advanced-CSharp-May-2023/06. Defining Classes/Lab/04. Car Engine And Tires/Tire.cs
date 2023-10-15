namespace CarManufacturer
{
    public class Tire
    {
        private int year;
        private double pressure;

        public Tire(int year, double pressure)
        {
            this.year = year;
            this.pressure = pressure;
        }

        public int Year
        {
            get => this.year;
            set => this.year = value;
        }

        public double Pressure
        {
            get => this.pressure;
            set => this.pressure = value;
        }
    }
}