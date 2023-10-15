namespace Raiding.Models.Interfaces
{
    public interface IBaseHero
    {
        public string Name { get; }
        public int Power { get; }
        public string CastAbility();
    }
}