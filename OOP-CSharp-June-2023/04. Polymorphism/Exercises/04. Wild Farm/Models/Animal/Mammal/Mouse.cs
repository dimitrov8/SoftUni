namespace WildFarm.Models.Animal
{
    using Exceptions;
    using Interfaces;

    public class Mouse : Mammal
    {
        private const double WEIGHT_GAIN = 0.10;

        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override string ProduceSound() => "Squeak";

        public override string Feed(IFood food)
        {
            string foodName = food.GetType().Name;
            foodName = foodName switch
            {
                "Vegetable" => this.ProduceSound(),
                "Fruit" => this.ProduceSound(),
                _ => throw new AnimalFoodTypeException(string.Format(ExceptionMessages.INVALID_FOOD_TYPE_FOR_ANIMAL,
                    this.GetType().Name, food.GetType().Name))
            };

            this.Weight += WEIGHT_GAIN * food.Quantity;
            this.FoodEaten += food.Quantity;
            return foodName;
        }
    }
}