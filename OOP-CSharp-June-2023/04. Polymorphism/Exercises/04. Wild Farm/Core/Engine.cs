namespace WildFarm.Core
{
    using AnimalFactories;
    using AnimalFactories.Interfaces;
    using Exceptions;
    using FoodFactories;
    using FoodFactories.Interfaces;
    using Interfaces;
    using IO.Interfaces;
    using Models.Interfaces;
    using System.Collections.Generic;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IAnimalFactory animalFactory;
        private readonly IFoodFactory foodFactory;
        private readonly ICollection<IAnimal> animals;

        private Engine()
        {
            this.animalFactory = new AnimalFactory();
            this.foodFactory = new FoodFactory();
            this.animals = new List<IAnimal>();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string input;
            while ((input = this.reader.ReadLine()) != "End")
                try
                {
                    IAnimal animal = this.ReceiveAnimal(input);
                    IFood food = this.ReceiveFood();
                    this.writer.WriteLine(animal.ProduceSound());
                    animal.Feed(food);
                }
                catch (AnimalFoodTypeException fte)
                {
                    this.writer.WriteLine(fte.Message);
                }
                catch (InvalidAnimalTypeException iat)
                {
                    this.writer.WriteLine(iat.Message);
                }
                catch (InvalidFoodTypeException ift)
                {
                    this.writer.WriteLine(ift.Message);
                }

            this.PrintAnimals();
        }

        private IAnimal ReceiveAnimal(string input)
        {
            string[] animalInfo = input.Split();
            IAnimal animal = this.animalFactory.CreateAnimal(animalInfo);

            this.animals.Add(animal);
            return animal;
        }

        private IFood ReceiveFood()
        {
            string[] foodInfo = this.reader.ReadLine().Split();
            IFood food = this.foodFactory.CreateFood(foodInfo[0], int.Parse(foodInfo[1]));

            return food;
        }

        private void PrintAnimals()
        {
            foreach (IAnimal animal in this.animals)
                this.writer.WriteLine(animal.ToString());
        }
    }
}