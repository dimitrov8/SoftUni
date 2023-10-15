namespace ChristmasPastryShop.Models.Delicacies
{
    using Contracts;
    using System;
    using Utilities.Messages;

    public abstract class Delicacy : IDelicacy
    {
        private string name;

        protected Delicacy(string name, double price)
        {
            this.Name = name;
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

        public double Price { get; private set; }

        public override string ToString() => $"{this.Name} - {this.Price:F2} lv";
    }
}