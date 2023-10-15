namespace Gym.Repositories.Contracts
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IReadOnlyCollection<T> Models { get; }

        void Add(T equipment);

        bool Remove(T model);

        T FindByType(string type);
    }
}
