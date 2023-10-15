namespace PizzaCalories
{
    using System;

    public class Topping
    {
        private const double TOPPING_MEAT = 1.2;
        private const double TOPPING_VEGGIES = 0.8;
        private const double TOPPING_CHEESE = 1.1;
        private const double TOPPING_SAUCE = 0.9;
        private const double CALORIES_PER_GRAM = 2.0;

        private string toppingType;
        private double weight;
        private double totalCalories = CALORIES_PER_GRAM;

        public Topping(string inputToppingType, double inputWeight)
        {
            this.ToppingType = inputToppingType;
            this.Weight = inputWeight;
            this.TotalCalories = this.totalCalories;
        }

        public string ToppingType
        {
            get => this.toppingType;
            private set
            {
                if (this.IsInvalidToppingType(value.ToLower()))
                    throw new ArgumentException(string.Format(ExceptionMessages.INVALID_TOPPING, value));
                this.toppingType = value.ToLower();
            }
        }

        public double Weight
        {
            get => this.weight;
            private set
            {
                if (this.IsNotAllowedWeight(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.NOT_ALLOWED_TOPPING_WEIGHT, char.ToUpper(this.ToppingType[0]) + this.ToppingType.Substring(1)));
                this.weight = value;
            }
        }

        public double TotalCalories
        {
            get => this.totalCalories;
            private set
            {
                value = this.CalculateCalories() * this.Weight;
                this.totalCalories = value;
            }
        }

        private bool IsInvalidToppingType(string currentToppingType)
            => currentToppingType != "meat"
               && currentToppingType != "veggies"
               && currentToppingType != "cheese"
               && currentToppingType != "sauce";

        private bool IsNotAllowedWeight(double currentWeight) => currentWeight < 1 || currentWeight > 50;

        private double CalculateCalories()
        {
            double calories = this.totalCalories;

            calories = this.ToppingType == "meat"
                ? calories * TOPPING_MEAT
                : this.ToppingType == "veggies"
                    ? calories * TOPPING_VEGGIES
                    : this.ToppingType == "cheese"
                        ? calories * TOPPING_CHEESE
                        : calories * TOPPING_SAUCE;

            return calories;
        }
    }
}