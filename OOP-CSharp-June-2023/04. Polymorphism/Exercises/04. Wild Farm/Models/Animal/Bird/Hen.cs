namespace WildFarm.Models.Animal
{
    using Interfaces;

    public class Hen : Bird
    {
        private const double WEIGHT_GAIN = 0.35;
        public Hen(string name, double wingSize, double weight) : base(name, wingSize, weight)
        {
        }

        public override string ProduceSound() => "Cluck";

        public override string Feed(IFood food)
        {
            this.Weight += WEIGHT_GAIN * food.Quantity;
            this.FoodEaten += food.Quantity;
            return this.ProduceSound();
        }
    }
}