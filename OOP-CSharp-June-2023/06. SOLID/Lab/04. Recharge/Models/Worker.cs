namespace P04.Recharge
{
    using Contracts;
    using System;

    public abstract class Worker : IWorker
    {
        private string id;
        private int workingHours;

        protected Worker(string id)
        {
            this.Id = id;
        }

        public string Id
        {
            get => this.id;
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ID cannot be null or empty!");

                this.id = value;
            }
        }

        public virtual void Work(int hours)
        {
            this.workingHours += hours;
        }
    }
}