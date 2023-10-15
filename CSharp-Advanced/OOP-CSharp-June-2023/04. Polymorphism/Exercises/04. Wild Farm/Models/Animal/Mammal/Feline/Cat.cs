namespace WildFarm.Models.Animal
{
    using Exceptions;
    using Interfaces;

    public class Cat : Feline
    {
        private const double WEIGHT_GAIN = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion,
            breed)
        {
        }

        public override string ProduceSound() => "Meow";

        public override string Feed(IFood food)
        {
            string foodName = food.GetType().Name;
            foodName = foodName switch
            {
                "Vegetable" => this.ProduceSound(),
                "Meat" => this.ProduceSound(),
                _ => throw new AnimalFoodTypeException(string.Format(ExceptionMessages.INVALID_FOOD_TYPE_FOR_ANIMAL,
                    this.GetType().Name, food.GetType().Name))
            };

            this.Weight += WEIGHT_GAIN * food.Quantity;
            this.FoodEaten += food.Quantity;
            return foodName;
        }
    }
}