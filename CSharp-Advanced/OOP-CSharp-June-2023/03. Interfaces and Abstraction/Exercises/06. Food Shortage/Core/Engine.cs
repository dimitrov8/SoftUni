namespace FoodShortage.Core
{
    using Interfaces;
    using IO.Interfaces;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly HashSet<Citizen> citizens;
        private readonly HashSet<Rebel> rebels;

        private Engine()
        {
            this.citizens = new HashSet<Citizen>();
            this.rebels = new HashSet<Rebel>();
        }

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            this.AddPeople();
            this.BuyFood();
            this.PrintTotalAmountOfFoodPurchased();
        }

        private void AddPeople()
        {
            int nOfPeople = int.Parse(this.reader.ReadLine()!);

            for (int i = 0; i < nOfPeople; i++)
            {
                string[] peopleInfo = this.reader.ReadLine().Split(' ');
                string name = peopleInfo[0];
                int age = int.Parse(peopleInfo[1]);

                if (peopleInfo.Length == 4)
                {
                    string id = peopleInfo[2];
                    string birthdate = peopleInfo[3];
                    Citizen citizen = new Citizen(name, age, id, birthdate);
                    this.citizens.Add(citizen);
                }
                else if (peopleInfo.Length == 3)
                {
                    string group = peopleInfo[2];
                    Rebel rebel = new Rebel(name, age, group);
                    this.rebels.Add(rebel);
                }
            }
        }

        private void BuyFood()
        {
            string input;
            while ((input = this.reader.ReadLine()) != "End")
            {
                string personName = input;

                if (this.IsCitizen(personName))
                    this.GetCitizen(personName).BuyFood();
                else if (this.IsRebel(personName))
                    this.GetRebel(personName).BuyFood();
            }
        }

        private bool IsCitizen(string name) => this.citizens.Select(n => n.Name).Contains(name);
        private bool IsRebel(string name) => this.rebels.Select(n => n.Name).Contains(name);

        private Citizen GetCitizen(string name) => this.citizens.First(n => n.Name == name);
        private Rebel GetRebel(string name) => this.rebels.First(n => n.Name == name);

        private void PrintTotalAmountOfFoodPurchased()
        {
            int totalAmountOfFoodPurchased =
                this.citizens.Select(c => c.Food).Sum() + this.rebels.Select(c => c.Food).Sum();
            this.writer.WriteLine(totalAmountOfFoodPurchased.ToString());
        }
    }
}