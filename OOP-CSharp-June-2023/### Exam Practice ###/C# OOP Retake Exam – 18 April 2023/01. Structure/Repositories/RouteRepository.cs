namespace EDriveRent.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RouteRepository : IRepository<IRoute>
    {
        private List<IRoute> routes;

        public RouteRepository(List<IRoute> routes)
        {
            this.routes = routes;
        }

        public void AddModel(IRoute route)
        {
            this.routes.Add(route);
        }

        public bool RemoveById(string identifier) => this.routes.Remove(this.FindById(identifier));

        public IRoute FindById(string identifier)
            => this.routes.FirstOrDefault(u => u.RouteId == int.Parse(identifier));

        public IReadOnlyCollection<IRoute> GetAll() => this.routes;
    }
}