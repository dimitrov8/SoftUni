namespace FoodShortage.Models
{
    using Interfaces;

    public class Citizen : Base, ICitizen, IBuyer
    {
        public Citizen(string name, int age, string id, string birthdate)
            : base(name, age)
        {
            this.Id = id;
            this.Birthdate = birthdate;
        }

        public string Id { get; }
        public string Birthdate { get; }

        public void BuyFood() => this.Food += 10;
    }
}