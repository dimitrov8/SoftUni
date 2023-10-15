namespace PizzaCalories
{
    using System;

    public class Dough
    {
        private const double FLOUR_TYPE_WHITE = 1.5;
        private const double FLOUR_TYPE_WHOLEGRAIN = 1.0;
        private const double BAKING_TECHNIQUE_CRISPY = 0.9;
        private const double BAKING_TECHNIQUE_CHEWY = 1.1;
        private const double BAKING_TECHNIQUE_HOMEMADE = 1.0;
        private const double CALORIES_PER_GRAM = 2.0;

        private string flourType;
        private string bakingTechnique;
        private double weight;
        private double totalCalories = CALORIES_PER_GRAM;

        public Dough(string inputFlourType, string inputBakingTechnique, double weight)
        {
            this.FlourType = inputFlourType;
            this.BakingTechnique = inputBakingTechnique;
            this.Weight = weight;
            this.TotalCalories = this.totalCalories;
        }

        public string FlourType
        {
            get => this.flourType;
            private set
            {
                if (this.IsInvalidFlourType(value.ToLower()))
                    throw new ArgumentException(ExceptionMessages.INVALID_DOUGH);
                this.flourType = value.ToLower();
            }
        }

        public string BakingTechnique
        {
            get => this.bakingTechnique;
            private set
            {
                if (this.IsInvalidBakingTechnique(value.ToLower()))
                    throw new ArgumentException(ExceptionMessages.INVALID_DOUGH);
                this.bakingTechnique = value.ToLower();
            }
        }

        public double Weight
        {
            get => this.weight;
            private set
            {
                if (this.IsNotAllowedWeight(value))
                    throw new ArgumentException(ExceptionMessages.NOT_ALLOWED_WEIGHT);
                this.weight = value;
            }
        }

        public double TotalCalories
        {
            get => this.totalCalories;
            private set
            {
                value = this.CalculateCalories();
                this.totalCalories = value;
            }
        }

        private bool IsInvalidFlourType(string currentFlourType)
        {
            return currentFlourType != "white" && currentFlourType != "wholegrain";
        }


        private bool IsInvalidBakingTechnique(string currentBakingTechnique)
        {
            return currentBakingTechnique != "crispy"
                   && currentBakingTechnique != "chewy"
                   && currentBakingTechnique != "homemade";
        }

        private bool IsNotAllowedWeight(double currentWeight)
        {
            return currentWeight < 1 || currentWeight > 200;
        }

        private double CalculateCalories()
        {
            double calories = this.totalCalories;

            calories = this.FlourType == "white"
                ? calories * FLOUR_TYPE_WHITE
                : calories * FLOUR_TYPE_WHOLEGRAIN;

            calories = this.bakingTechnique == "crispy"
                ? calories * BAKING_TECHNIQUE_CRISPY
                : this.bakingTechnique == "chewy"
                    ? calories * BAKING_TECHNIQUE_CHEWY
                    : calories * BAKING_TECHNIQUE_HOMEMADE;

            return calories * this.Weight;
        }
    }
}