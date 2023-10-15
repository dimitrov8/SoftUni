namespace BirthdayCelebrations.Models.Interfaces
{
    public interface IPet
    {
        public string Name { get; }
        public string Id { get; }
        public string Birthdate { get; }
    }
}