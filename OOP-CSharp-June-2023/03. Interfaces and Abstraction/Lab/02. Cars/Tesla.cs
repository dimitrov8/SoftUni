namespace Cars
{
    public class Tesla : IElectricCar
    {
        public Tesla(string model, string color, int batter)
        {
            this.Model = model;
            this.Color = color;
            this.Battery = batter;
        }

        public string Model { get; set; }
        public string Color { get; set; }
        public int Battery { get; set; }

        public string Start() => "Engine start";

        public string Stop() => "Breaaak!";

        public override string ToString() => $"{this.Color} Tesla {this.Model} with {this.Battery} Batteries";
    }
}