namespace PizzaCalories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pizza
    {
        private string name;
        private Dough dough;
        private readonly List<Topping> toppingList;

        public Pizza(string name)
        {
            this.Name = name;
            this.toppingList = new List<Topping>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (this.IsInvalidPizzaName(value))
                    throw new ArgumentException(ExceptionMessages.INVALID_PIZZA_NAME);
                this.name = value;
            }
        }

        public Dough Dough
        {
            set => this.dough = value;
        }

        private bool IsInvalidPizzaName(string currentPizzaName) => string.IsNullOrEmpty(currentPizzaName) || currentPizzaName.Length - 1 > 15;

        public void AddTopping(Topping topping) => this.toppingList.Add(topping);

        public bool ExceededNumberOfToppings() => this.toppingList.Count > 10
            ? throw new ArgumentException(ExceptionMessages.EXCEEDED_NUMBER_OF_TOPPINGS)
            : false;

        private double TotalCalories() => this.toppingList.Sum(t => t.TotalCalories) + this.dough.TotalCalories;

        public override string ToString()
        {
            return $"{this.Name} - {this.TotalCalories():F2} Calories.";
        }
    }
}