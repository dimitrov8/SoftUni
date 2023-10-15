namespace Special_Cars
{
    public class Tires
    {
        // Fields
        private int year;
        private double pressure;
        
        // Properties
        public int Year
        {
            get => year;
            set => year = value;
        }

        public double Pressure
        {
            get => pressure;
            set => pressure = value;
        }

        // Constructors
        public Tires(int year, double pressure)
        {
            Year = year;
            Pressure = pressure;
        }
    }
}