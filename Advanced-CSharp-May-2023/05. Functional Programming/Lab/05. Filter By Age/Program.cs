using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _05._Filter_By_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the age filter function
            // It takes a Person object, a string condition, and an int age
            // It returns a bool that represents whether the person satisfies the condition based on their age
            Func<Person, string, int, bool> ageFilter = (p, f, a) => f == "older" ? p.Age >= a : p.Age < a;
            
            // Define the formatter function
            // It takes a Person object and a string array pattern
            // It formats the Person object based on the given pattern and returns the result as a string
            Func<Person, string[], string> formatter = (p, f) =>
            {
                StringBuilder sb = new StringBuilder();
                if (f.Length == 2)
                {
                    if (f[0] == "name")
                    {
                        sb.Append($"{p.Name} - {p.Age}");
                    }
                    else if (f[0] == "age")
                    {
                        sb.Append($"{p.Age} - {p.Name}");
                    }
                }
                else if (f.Length == 1)
                {
                    if (f[0] == "name")
                    {
                        sb.Append(p.Name);
                    }
                    else if (f[0] == "age")
                    {
                        sb.Append(p.Age);
                    }
                }

                return string.Format(sb.ToString(), p.Name, p.Age);
            };

            List<Person> people = new List<Person>();  // Create a list of Person objects
            int numberOfPeople = int.Parse(Console.ReadLine()); // Read the number of people to be added to the list
            for (int i = 0; i < numberOfPeople; i++) // Add each person to the list by reading their name and age from the console input
            {
                string[] personInfo = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries);
                people.Add(new Person(personInfo[0], int.Parse(personInfo[1]))); // Name, Age
            }

            // Read the condition and age from the console input
            string condition = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            string[] pattern = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries); // Read the output format pattern from the console input
            
            // Filter the list of people based on the age filter function and the given condition and age
            // Then format the filtered people based on the given pattern and join them as a string separated by a new line
            Console.WriteLine(string.Join(Environment.NewLine,
                people.Where(p => ageFilter(p, condition, age))
                    .Select(p => formatter(p, pattern))));
        }
    }
    
    // The Person class defines a person with a Name and an Age property
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}