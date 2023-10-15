namespace WildFarm.Models.Animal
{
    using Exceptions;
    using Interfaces;

    public class Owl : Bird
    {
        private const double WEIGHT_GAIN = 0.25;
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override string ProduceSound() => "Hoot Hoot";

        public override string Feed(IFood food)
        {
            if (food.GetType().Name != "Meat")
                throw new AnimalFoodTypeException(string.Format(ExceptionMessages.INVALID_FOOD_TYPE_FOR_ANIMAL,
                    this.GetType().Name, food.GetType().Name));

            this.Weight += WEIGHT_GAIN * food.Quantity;
            this.FoodEaten += food.Quantity;
            return this.ProduceSound();
        }
    }
}