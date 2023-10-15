namespace FoodShortage.Models
{
    using Interfaces;

    public class Rebel : Base, IRebel, IBuyer
    {
        public Rebel(string name, int age, string group)
            : base(name, age)
        {
            this.Group = group;
        }

        public string Group { get; }
        
        public void BuyFood() => this.Food += 5;
    }
}