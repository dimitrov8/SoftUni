namespace EDriveRent.Repositories.Contracts
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        void AddModel(T user);

        bool RemoveById(string identifier);

        T FindById(string identifier);

        IReadOnlyCollection<T> GetAll();
    }
}