namespace RobotService.Core
{
    using Contracts;
    using Models;
    using Models.Contracts;
    using Repositories;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly SupplementRepository supplements;
        private readonly RobotRepository robots;

        public Controller()
        {
            this.robots = new RobotRepository();
            this.supplements = new SupplementRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            IRobot robot;
            switch (typeName)
            {
                case nameof(DomesticAssistant):
                    robot = new DomesticAssistant(model);
                    break;
                case nameof(IndustrialAssistant):
                    robot = new IndustrialAssistant(model);
                    break;
                default:
                    return string.Format(OutputMessages.ROBOT_CANNOT_BE_CREATED, typeName);
            }

            this.robots.AddNew(robot);
            return string.Format(OutputMessages.ROBOT_CREATED_SUCCESSFULLY, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            ISupplement supplement;
            switch (typeName)
            {
                case nameof(SpecializedArm):
                    supplement = new SpecializedArm();
                    break;
                case nameof(LaserRadar):
                    supplement = new LaserRadar();
                    break;
                default:
                    return string.Format(OutputMessages.SUPPLEMENT_CANNOT_BE_CREATED, typeName);
            }

            this.supplements.AddNew(supplement);
            return string.Format(OutputMessages.SUPPLEMENT_CREATED_SUCCESSFULLY, typeName);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            var supplement = this.supplements.Models().FirstOrDefault(s => s.GetType().Name == supplementTypeName);
            var selectedRobots = this.robots.Models().Where(r => r.Model == model);
            var stillNotUpgraded = selectedRobots.Where(r => r.InterfaceStandards.All(s => s != supplement.InterfaceStandard));

            if (!stillNotUpgraded.Any())
            {
                return string.Format(OutputMessages.ALL_MODELS_UPGRADED, model);
            }

            var robotToUpgrade = stillNotUpgraded.First();

            robotToUpgrade.InstallSupplement(supplement);
            return string.Format(OutputMessages.UPGRADE_SUCCESSFUL, model, supplementTypeName);
        }

        public string RobotRecovery(string model, int minutes)
        {
            var selectedRobots = this.robots.Models().Where(r => r.Model == model && r.BatteryLevel * 2 < r.BatteryCapacity);

            int fedRobots = 0;
            foreach (var robot in selectedRobots)
            {
                robot.Eating(minutes);
                fedRobots++;
            }

            return string.Format(OutputMessages.ROBOTS_FED, fedRobots);
        }

        public string PerformService(string serviceName, int interfaceStandard, int totalPowerNeeded)
        {
            var selectedRobots = this.robots.Models()
                .Where(r => r.InterfaceStandards.Contains(interfaceStandard))
                .OrderByDescending(r => r.BatteryLevel);

            if (!selectedRobots.Any())
                return string.Format(OutputMessages.UNABLE_TO_PERFORM, interfaceStandard);

            int availablePower = selectedRobots.Sum(r => r.BatteryLevel);

            if (availablePower < totalPowerNeeded)
                return string.Format(OutputMessages.MORE_POWER_NEEDED, serviceName, totalPowerNeeded - availablePower);

            int usedRobots = 0;
            foreach (var robot in selectedRobots)
            {
                usedRobots++;
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }

                totalPowerNeeded -= robot.BatteryLevel;
                robot.ExecuteService(robot.BatteryLevel);
            }

            return string.Format(OutputMessages.PERFORMED_SUCCESSFULLY, serviceName, usedRobots);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var robot in this.robots.Models()
                         .OrderByDescending(r => r.BatteryLevel)
                         .ThenBy(r => r.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}