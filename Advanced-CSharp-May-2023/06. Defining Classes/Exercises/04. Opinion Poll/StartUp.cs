using System;
using System.Collections.Generic;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int nOfPeople = int.Parse(Console.ReadLine());

            Family family = new Family();
            for (int i = 0; i < nOfPeople; i++)
            {
                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                int age = int.Parse(tokens[1]);

                Person person = new Person(name, age);
                family.AddMember(person);
            }

            List<Person> membersOlderThanThirty = family.MemberOlderThanThirty(family.People);
            foreach (var person in membersOlderThanThirty)
            {
                Console.WriteLine(person);
            }
            
        }
    }
}