using System;

namespace EqualityLogic
{
    public class Person : IEquatable<Person>, IComparable<Person>
    {
        private readonly string name;
        private readonly int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public override int GetHashCode()
        {
            return this.name.Length * this.age;
        }

        public bool Equals(Person other)
        {
            if (other == null)
            {
                return false;
            }

            return this.name.Equals(other.name) && this.age.Equals(other.age);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Person);
        }

        public int CompareTo(Person other)
        {
            int result = this.name.CompareTo(other.name);
            if (result == 0)
            {
                result = this.age.CompareTo(other.age);
            }

            return result;
        }
    }
}