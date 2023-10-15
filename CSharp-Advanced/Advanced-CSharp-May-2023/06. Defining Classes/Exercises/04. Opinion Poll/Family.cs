using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;

namespace DefiningClasses
{
    public class Family
    {
        private List<Person> people;

        public List<Person> People
        {
            get => people;
            set => people = value;
        }

        public Family()
        {
            people = new List<Person>();
        }
        
        public void AddMember(Person member)
        {
         people.Add(member);   
        }

        public Person GetOldestMember(List<Person> people)
        {
            Person person = people.OrderByDescending(x => x.Age).First();
            return person;
        }

        public List<Person> MemberOlderThanThirty(List<Person> people)
        {
            return people.Where(x => x.Age > 30).OrderBy(x => x.Name).ToList();
        }
    }
}