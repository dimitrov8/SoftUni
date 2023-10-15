using System;

namespace CocktailParty
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Cocktail cocktail = new Cocktail("Pina Colada", 3, 10);
            Ingredient rum = new Ingredient("Rum", 2, 3);
            Console.WriteLine(rum.ToString());
            cocktail.Add(rum);
            Console.WriteLine(cocktail.Remove("Rum")); 
            Ingredient vodka = new Ingredient("Vodka", 2, 5);
            Ingredient milk = new Ingredient("Milk", 0, 5);
            cocktail.Add(vodka);
            cocktail.Add(milk);
            Console.WriteLine(cocktail.GetMostAlcoholicIngredient());
            Console.WriteLine(cocktail.CurrentAlcoholLevel);
            Console.WriteLine(cocktail.Report());
        }
    }
}
