using System;
using System.Text;

namespace Animals
{
    public class Animal
    {
        private string _name;
        private int _age;
        private string _gender;

        public Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name
        {
            get => this._name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Invalid input!");
                this._name = value;
            }
        }

        public int Age
        {
            get => this._age;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid input!");
                this._age = value;
            }
        }


        public string Gender
        {
            get => this._gender;
            private set
            {
                if (value != "Male" && value != "Female")
                    throw new ArgumentException("Invalid input!");
                this._gender = value;
            }
        }

        public virtual string ProduceSound() => "Noise";

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}")
                .AppendLine($"{this.Name} {this.Age} {this.Gender}")
                .Append(this.ProduceSound());
            return sb.ToString().Trim();
        }
    }
}