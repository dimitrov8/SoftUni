namespace ChristmasPastryShop.Repositories
{
    using Contracts;
    using Models.Cocktails.Contracts;
    using System.Collections.Generic;

    public class CocktailRepository : IRepository<ICocktail>
    {
        private readonly List<ICocktail> cocktails;

        public CocktailRepository()
        {
            this.cocktails = new List<ICocktail>();
        }

        public IReadOnlyCollection<ICocktail> Models => this.cocktails;

        public void AddModel(ICocktail model) => this.cocktails.Add(model);
    }
}
