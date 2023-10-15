namespace RobotService.Models
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Utilities.Messages;

    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int convertionCapacityIndex;
        private readonly List<int> interfaceStandards;

        protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
        {
            this.Model = model;
            this.BatteryCapacity = batteryCapacity;
            this.BatteryLevel = batteryCapacity;
            this.ConvertionCapacityIndex = convertionCapacityIndex;
            this.interfaceStandards = new List<int>();
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.MODEL_NULL_OR_WHITESPACE);

                this.model = value;
            }
        }

        public int BatteryCapacity
        {
            get => this.batteryCapacity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.BATTERY_CAPACITY_BELOW_ZERO);

                this.batteryCapacity = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public int ConvertionCapacityIndex
        {
            get => this.convertionCapacityIndex;
            private set => this.convertionCapacityIndex = value;
        }

        public IReadOnlyCollection<int> InterfaceStandards => this.interfaceStandards;

        public void Eating(int minutes)
        {
            int producedEnergy = this.ConvertionCapacityIndex * minutes;
            this.BatteryLevel = producedEnergy > this.BatteryCapacity - this.BatteryLevel
                ? this.BatteryCapacity
                : this.BatteryLevel + producedEnergy;
        }

        public void InstallSupplement(ISupplement supplement)
        {
            this.interfaceStandards.Add(supplement.InterfaceStandard);
            this.BatteryCapacity -= supplement.BatteryUsage;
            this.BatteryLevel -= supplement.BatteryUsage;
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (this.BatteryLevel < consumedEnergy)
                return false;

            this.BatteryLevel -= consumedEnergy;
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name} {this.model}:");
            sb.AppendLine($"--Maximum battery capacity: {this.BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {this.BatteryLevel}");

            sb.AppendLine(this.InterfaceStandards.Count == 0
                ? "--Supplements installed: none"
                : $"--Supplements installed: {string.Join(" ", this.InterfaceStandards)}");

            return sb.ToString().Trim();
        }
    }
}