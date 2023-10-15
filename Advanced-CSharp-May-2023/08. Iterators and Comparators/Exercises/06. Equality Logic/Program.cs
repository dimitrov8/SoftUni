using System;
using System.Collections.Generic;

namespace EqualityLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            int nPeople = int.Parse(Console.ReadLine()!);
            var peopleSortedSet = new SortedSet<Person>();
            var peopleHashSet = new HashSet<Person>();
            for (int i = 0; i < nPeople; i++)
            {
                string[] personInfo = Console.ReadLine()?.Split();
                string name = personInfo![0];
                int age = int.Parse(personInfo[1]);
                Person person = new Person(name, age);

                peopleSortedSet.Add(person);
                peopleHashSet.Add(person);
            }

            Console.WriteLine(peopleSortedSet.Count);
            Console.WriteLine(peopleHashSet.Count);
        }
    }
}
