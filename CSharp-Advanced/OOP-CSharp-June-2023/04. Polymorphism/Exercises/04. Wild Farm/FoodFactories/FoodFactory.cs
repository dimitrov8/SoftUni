namespace WildFarm.FoodFactories
{
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.Interfaces;

    public class FoodFactory : IFoodFactory
    {
        public FoodFactory()
        {
        }
        
        public IFood CreateFood(string type, int quantity)
        {
            IFood food = type switch
            {
               "Vegetable" => new Vegetable(quantity),
               "Fruit" => new Fruit(quantity),
               "Meat" => new Meat(quantity),
               "Seeds" => new Seeds(quantity),
               _ => throw new InvalidFoodTypeException(ExceptionMessages.INVALID_FOOD_TYPE)
            };
            return food;
        }
    }
}