namespace P04.Recharge
{
    using Contracts;
    using System;

    public class Robot : Worker, IRechargeable
    {
        private int capacity;
        private int currentPower;

        public Robot(string id, int capacity) : base(id)
        {
            this.Capacity = capacity;
            this.CurrentPower = capacity;
        }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Capacity has to be at least 80.");

                if (value > 100)
                {
                    throw new ArgumentException("Capacity cannot be greater than 100.");
                }

                this.capacity = value;
            }
        }

        public int CurrentPower
        {
            get => this.currentPower;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Power cannot be negative!");

                this.currentPower = value;
            }
        }

        public override void Work(int hours)
        {
            if (hours >= this.CurrentPower)
                throw new ArgumentException($"{GetType().Name} with #ID: {this.Id} doesn't have enough power to complete the work!");

            base.Work(hours);
            this.CurrentPower -= hours;
        }

        public void Recharge() => this.CurrentPower = this.Capacity;
    }
}