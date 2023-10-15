namespace CarManufacturer
{
    public class Engine
    {
        private int horsePower;
        private double cubicCapacity;

        public Engine(int _horsePower, double _cubicCapacity)
        {
            horsePower = _horsePower;
            cubicCapacity = _cubicCapacity;
        }

        public int HorsePower
        {
            get => this.horsePower;
            private set => this.horsePower = value;
        }

        public double CubicCapacity
        {
            get => this.cubicCapacity;
            private set => this.cubicCapacity = value;
        }
    }
}