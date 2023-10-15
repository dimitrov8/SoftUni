namespace WildFarm.Models.Animal
{
    using Exceptions;
    using Interfaces;

    public class Dog : Mammal
    {
        private const double WEIGHT_GAIN = 0.40;
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override string ProduceSound() => "Woof!";

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