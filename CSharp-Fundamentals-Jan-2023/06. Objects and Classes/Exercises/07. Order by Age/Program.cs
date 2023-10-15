using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Order_by_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<Person> peopleList = new List<Person>();
            while ((input = Console.ReadLine()) != "End")
            {
                string[] personInfo = input.Split(' ');
                string name = personInfo[0];
                string id = personInfo[1];
                int age = int.Parse(personInfo[2]);

                bool isFound = peopleList.Any(p => p.ID == id);
                Person currPerson = new Person(name, id, age);
                if (isFound)
                {
                    Person existingPerson = peopleList.FirstOrDefault(p => p.ID == id);
                    existingPerson.Name = name;
                    existingPerson.Age = age;
                    continue;
                }

                peopleList.Add(currPerson);
            }

            foreach (Person person in peopleList.OrderBy(p => p.Age))
            {
                Console.WriteLine(person.ToString());
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int Age { get; set; }

        public Person(string name, string id, int age)
        {
            Name = name;
            ID = id;
            Age = age;
        }

        public override string ToString() =>
            $"{Name} with ID: {ID} is {Age} years old.";
    }
}