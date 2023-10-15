namespace ChristmasPastryShop.Models.Cocktails
{
    using Contracts;
    using System;
    using Utilities.Messages;

    public abstract class Cocktail : ICocktail
    {
        private string name;

        protected Cocktail(string name, string size, double price)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);

                this.name = value;
            }
        }

        public string Size { get; private set; }
        public double Price { get; private set; }

        public override string ToString() => $"{this.Name} ({this.Size}) - {this.Price:F2} lv";
    }
}