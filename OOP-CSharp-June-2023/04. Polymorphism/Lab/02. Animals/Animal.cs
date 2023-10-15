namespace Animals
{
    public class Animal
    {
        private string name;
        private string favouriteFood;

        protected Animal(string name, string favouriteFood)
        {
            this.Name = name;
            this.FavouriteFood = favouriteFood;
        }

        protected string Name
        {
            get => this.name;
            set => this.name = value;
        }

        protected string FavouriteFood
        {
            get => this.favouriteFood;
            set => this.favouriteFood = value;
        }

        public virtual string ExplainSelf() => $"I am {this.Name} and my favourite food is {this.FavouriteFood}";
    }
}