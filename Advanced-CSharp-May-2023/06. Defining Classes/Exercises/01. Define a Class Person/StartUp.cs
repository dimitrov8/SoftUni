using System.Collections.Generic;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>() {new Person("Peter", 20), new Person("George", 18), new Person("Jose", 43)};
        }
    }
}