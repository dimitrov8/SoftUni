namespace EDriveRent.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class UserRepository : IRepository<IUser>
    {
        private List<IUser> users;

        public UserRepository(List<IUser> users)
        {
            this.users = users;
        }

        public void AddModel(IUser user)
        {
            this.users.Add(user);
        }

        public bool RemoveById(string identifier) => this.users.Remove(this.FindById(identifier));

        public IUser FindById(string identifier)
            => this.users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);

        public IReadOnlyCollection<IUser> GetAll() => this.users;
    }
}