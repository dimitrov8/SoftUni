namespace Raiding.Factories.Interfaces
{
    using Models.Interfaces;

    public interface IHeroFactory
    {
        IBaseHero CreateHero(string name, string type);
    }
}