namespace EDriveRent.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class UserRepository : IRepository<IUser>
    {
        private readonly List<IUser> users;

        public UserRepository()
        {
            this.users = new List<IUser>();
        }

        public void AddModel(IUser user)
        {
            this.users.Add(user);
        }

        public bool RemoveById(string identifier) => this.users.Remove(this.FindById(identifier));

        public IUser FindById(string identifier) => this.users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);

        public IReadOnlyCollection<IUser> GetAll() => this.users;
    }
}