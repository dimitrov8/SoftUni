namespace DefiningClasses
{
    public class Car
    {
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public Cargo Cargo { get; set; }
        public Tires Tires { get; set; }

        public Car(string model, Engine engine, Cargo cargo, Tires tires)
        {
            Model = model;
            Engine = engine;
            Cargo = cargo;
            Tires = tires;
        }

        public override string ToString()
        {
            return $"{Model}";
        }
    }

    public class Engine
    {
        public int Speed { get; set; }

        public int Power { get; set; }

        public Engine(int speed, int power)
        {
            Speed = speed;
            Power = power;
        }
    }

    public class Cargo
    {
        public string Type { get; set; }
        public int Weight { get; set; }

        public Cargo(string type, int weight)
        {
            Type = type;
            Weight = weight;
        }
    }

    public class Tires
    {
        public double[] Pressure { get; set; }
        public int[] Age { get; set;}

        public Tires(double[] pressure, int[] age)
        {
            Pressure = pressure;
            Age = age;
        }
    }
}