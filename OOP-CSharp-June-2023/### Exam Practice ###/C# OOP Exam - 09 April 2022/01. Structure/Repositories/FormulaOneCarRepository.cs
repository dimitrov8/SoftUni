namespace Formula1.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private readonly List<IFormulaOneCar> cars;

        public FormulaOneCarRepository()
        {
            this.cars = new List<IFormulaOneCar>();
        }

        public IReadOnlyCollection<IFormulaOneCar> Models => this.cars;

        public void Add(IFormulaOneCar car) => this.cars.Add(car);

        public bool Remove(IFormulaOneCar model) => this.cars.Remove(model);

        public IFormulaOneCar FindByName(string model) => this.cars.FirstOrDefault(c => c.Model == model);
    }
}