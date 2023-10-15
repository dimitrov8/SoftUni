namespace FoodShortage.Models.Interfaces
{
    public interface IBuyer
    {
        public int Food { get; }
        
        public void BuyFood();
    }
}