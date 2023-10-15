using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailParty
{
    public class Cocktail
    {
        public Cocktail(string name, int capacity, int maxAlcoholLevel)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.MaxAlcoholLevel = maxAlcoholLevel;
            this.Cocktails = new HashSet<Ingredient>(capacity);
        }

        public HashSet<Ingredient> Cocktails { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public int MaxAlcoholLevel { get; set; }

        public int CurrentAlcoholLevel => this.Cocktails.Sum(i => i.Alcohol);
        
        public void Add(Ingredient ingredient)
        {
            if (this.Capacity > this.Cocktails.Count && !this.Cocktails.Contains(ingredient))
            {
                this.Cocktails.Add(ingredient);
            }
        }

        public bool Remove(string name)
        {
            Ingredient ingredientToRemove = this.Cocktails.FirstOrDefault(i => i.Name == name);
            return this.Cocktails.Remove(ingredientToRemove);
        }

        public Ingredient FindIngredient(string name)
        {
            return this.Cocktails.FirstOrDefault(x => x.Name == name);
        }

        public Ingredient GetMostAlcoholicIngredient()
        {
            return this.Cocktails.OrderByDescending(i => i.Alcohol).FirstOrDefault();
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Cocktail: {this.Name} - Current Alcohol Level: {this.CurrentAlcoholLevel}");
            foreach (var cocktail in this.Cocktails)
            {
                sb.AppendLine(cocktail.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}