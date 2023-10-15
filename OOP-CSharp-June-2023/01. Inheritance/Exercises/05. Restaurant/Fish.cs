namespace Restaurant
{
    public class Fish : MainDish
    {
        // Default values
        private new const double Grams = 22;

        public Fish(string name, decimal price) : base(name, price, Grams)
        {
        }

    }
}