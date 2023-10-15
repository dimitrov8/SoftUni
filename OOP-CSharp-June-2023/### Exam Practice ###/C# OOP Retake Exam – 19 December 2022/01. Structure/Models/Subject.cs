namespace UniversityCompetition.Models
{
    using Contracts;
    using System;
    using Utilities.Messages;

    public abstract class Subject : ISubject
    {
        private string name;

        protected Subject(int id, string name, double rate)
        {
            this.Id = id;
            this.Name = name;
            this.Rate = rate;
        }

        public int Id { get; private set; }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.NAME_NULL_OR_WHITESPACE);

                this.name = value;
            }
        }

        public double Rate { get; private set; }
    }
}