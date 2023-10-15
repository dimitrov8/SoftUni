namespace FoodShortage.Models
{
    public class Base
    {
        protected Base(string name, int age)
        {
            this.Name = name;
            this.Age = age;
            this.Food = 0;
        }
        
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int Food { get; protected set; }
    }
}