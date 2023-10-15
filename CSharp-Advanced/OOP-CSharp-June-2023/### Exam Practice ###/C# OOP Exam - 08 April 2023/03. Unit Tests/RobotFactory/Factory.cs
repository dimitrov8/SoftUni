namespace RobotFactory
{
    using System.Collections.Generic;
    using System.Linq;

    public class Factory
    {
        public Factory(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Robots = new List<Robot>();
            this.Supplements = new List<Supplement>();
        }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public List<Robot> Robots;

        public List<Supplement> Supplements;

        public string ProduceRobot(string model, double price, int interfaceStandard)
        {
            if (this.Robots.Count < this.Capacity)
            {
                var robot = new Robot(model, price, interfaceStandard);
                this.Robots.Add(robot);
                return $"Produced --> {robot}";
            }

            return "The factory is unable to produce more robots for this production day!";
        }

        public string ProduceSupplement(string name, int interfaceStandard)
        {
            var supplement = new Supplement(name, interfaceStandard);
            this.Supplements.Add(supplement);

            return supplement.ToString();
        }

        public bool UpgradeRobot(Robot robot, Supplement supplement)
        {
            if (robot.Supplements.Contains(supplement) || robot.InterfaceStandard != supplement.InterfaceStandard)
            {
                return false;
            }

            robot.Supplements.Add(supplement);
            return true;
        }

        public Robot SellRobot(double price)
        {
            var orderedRobots = this.Robots.OrderByDescending(r => r.Price).ToList();

            var robot = orderedRobots.FirstOrDefault(r => r.Price <= price);

            return robot;
        }
    }
}