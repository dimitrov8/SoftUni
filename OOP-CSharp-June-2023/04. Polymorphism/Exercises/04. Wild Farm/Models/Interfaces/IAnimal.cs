namespace WildFarm.Models.Interfaces
{
    public interface IAnimal
    {
        public string Name { get; }
        public double Weight { get; }
        public int FoodEaten { get; }
        public string Feed(IFood food);
        public string ProduceSound();
    }
}