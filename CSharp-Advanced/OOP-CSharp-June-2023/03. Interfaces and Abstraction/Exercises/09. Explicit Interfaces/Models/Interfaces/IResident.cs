namespace ExplicitInterfaces.Models.Interfaces
{
    public interface IResident 
    {
        public string Name { get; }
        public int Age { get; }
        public string GetName();
    }
}