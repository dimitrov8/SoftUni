﻿namespace Restaurant
{
    public class Cake : Dessert
    {
        // Default values
        private new const double Grams = 250;
        private new const double Calories = 1000;
        private const decimal CakePrice = 5;

        public Cake(string name) : base(name, CakePrice, Grams, Calories)
        {
        }
    }
}