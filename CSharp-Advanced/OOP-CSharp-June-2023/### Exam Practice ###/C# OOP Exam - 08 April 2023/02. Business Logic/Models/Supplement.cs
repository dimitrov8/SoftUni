namespace RobotService.Models
{
    using Contracts;

    public abstract class Supplement : ISupplement
    {
        protected Supplement(int interfaceStandard, int batteryUsage)
        {
            this.InterfaceStandard = interfaceStandard;
            this.BatteryUsage = batteryUsage;
        }
        
        public int InterfaceStandard { get; private set; }
        public int BatteryUsage { get; private set; }
    }
}