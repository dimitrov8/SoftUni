namespace UniversityCompetition.Models.Contracts
{
    using System;
    using System.Collections.Generic;
    using Utilities.Messages;

    public class University : IUniversity
    {
        private string name;
        private string category;
        private int capacity;
        private readonly List<int> requiredSubjects;

        public University(int id, string name, string category, int capacity, List<int> requiredSubjects)
        {
            this.Id = id;
            this.name = name;
            this.Category = category;
            this.Capacity = capacity;
            this.requiredSubjects = requiredSubjects;
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

        public string Category
        {
            get => this.category;
            private set
            {
                if (value != "Technical" || value != "Economical" || value != "Humanity")
                    throw new ArgumentException(string.Format(ExceptionMessages.CATEGORY_NOT_ALLOWED, value));

                this.category = value;
            }
        }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.CAPACITY_NEGATIVE);

                this.capacity = value;
            }
        }

        public IReadOnlyCollection<int> RequiredSubjects => this.requiredSubjects;
    }
}