using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Oldest_Family_Member
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int n = int.Parse(Console.ReadLine());
            Family family = new Family();
            for (int i = 1; i <= n; i++)
            {
                string[] personData = Console.ReadLine().Split();
                string name = personData[0];
                int age = int.Parse(personData[1]);

                Person person = new Person(name, age);
                family.AddMember(person);
            }

            Console.WriteLine($"{family.GetOldestMember().Name} {family.GetOldestMember().Age}");
        }
    }

    public class Family
    {
        private readonly List<Person> family = new List<Person>();

       public void AddMember(Person member)
       {
           family.Add(member);
       }

       public Person GetOldestMember()
       {
           return family.OrderByDescending(p => p.Age).FirstOrDefault();
       }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age {get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}