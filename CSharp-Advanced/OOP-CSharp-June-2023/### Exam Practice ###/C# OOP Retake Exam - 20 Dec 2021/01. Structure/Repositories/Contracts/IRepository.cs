namespace NavalVessels.Repositories.Contracts
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IReadOnlyCollection<T> Models { get; }

        void Add(T vessel);

        bool Remove(T vessel);

        T FindByName(string name);
    }
}
