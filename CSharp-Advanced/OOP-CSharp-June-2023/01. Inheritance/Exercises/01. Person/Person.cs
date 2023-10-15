using System.Text;

namespace Person
{
    public class Person
    {
        private int _age;

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }

        public int Age
        {
            get => this._age;
            set
            {
                if (value > 0) this._age = value;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Name: {this.Name}, Age: {this.Age}");
            return sb.ToString().Trim();
        }
    }
}