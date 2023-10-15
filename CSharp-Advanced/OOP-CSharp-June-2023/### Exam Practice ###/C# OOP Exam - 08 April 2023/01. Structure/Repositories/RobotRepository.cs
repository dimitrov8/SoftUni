namespace RobotService.Repositories
{
    using Contracts;
    using Models.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> robots;

        public RobotRepository()
        {
            this.robots = new List<IRobot>();
        }

        public IReadOnlyCollection<IRobot> Models() => this.robots.AsReadOnly();

        public void AddNew(IRobot model) => this.robots.Add(model);

        public bool RemoveByName(string typeName) => this.robots.Remove(this.robots.FirstOrDefault(r => r.GetType().Name == typeName));

        public IRobot FindByStandard(int interfaceStandard)
            => this.robots.FirstOrDefault(r => r.InterfaceStandards.Any(i => i == interfaceStandard));
    }
}