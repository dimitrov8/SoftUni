using System.Collections.Generic;

namespace PersonsInfo
{
    public class Team
    {
        private string name;
        private readonly List<Person> firstTeam;
        private readonly List<Person> reserveTeam;

        public Team(string name)
        {
            this.name = name;
            this.firstTeam = new List<Person>();
            this.reserveTeam = new List<Person>();
        }

        public IReadOnlyCollection<Person> FirstTeam => this.firstTeam.AsReadOnly();

        public IReadOnlyCollection<Person> ReserveTeam => this.reserveTeam.AsReadOnly();

        public void AddPlayer(Person person)
        {
            (person.Age < 40 ? this.firstTeam : this.reserveTeam).Add(person);
        }
    }
}