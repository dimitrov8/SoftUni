namespace ExplicitInterfaces.Models
{
    using Interfaces;

    public class Citizen : IResident, IPerson
    {
        public Citizen(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }


        string IResident.GetName() => $"Mr/Ms/Mrs {this.Name}";
        string IPerson.GetName() => this.Name;
    }
}