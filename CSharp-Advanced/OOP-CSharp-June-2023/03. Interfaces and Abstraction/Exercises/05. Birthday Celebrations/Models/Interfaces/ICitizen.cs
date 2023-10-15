namespace BirthdayCelebrations.Models.Interfaces
{
    public interface ICitizen
    {
        public string Name { get; }
        public int Age { get; }
        public string Id { get; }
        public string Birthdate { get; }
    }
}