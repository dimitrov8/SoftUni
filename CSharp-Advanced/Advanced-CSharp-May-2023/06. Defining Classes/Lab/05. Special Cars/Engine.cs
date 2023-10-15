namespace Special_Cars
{
    public class Engine
    {
        // Fields
        private int horsePower;
        private double cubicCapacity;
        
        // Properties
        public int HorsePower
        {
            get => horsePower;
            set => horsePower = value;
        }

        public double CubicCapacity
        {
            get => cubicCapacity;
            set => cubicCapacity = value;
        }
        
        // Constructors
        public Engine(int horsePower, double cubicCapacity)
        {
            HorsePower = horsePower;
            CubicCapacity = cubicCapacity;
        }
    }
}