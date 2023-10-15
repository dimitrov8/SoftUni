namespace DefiningClasses
{
    public class Pokemon
    {
        public string Name { get; set; }
        public string ElementName { get; set; }
        public int Health { get; set; }

        public Pokemon(string name, string elementName, int health)
        {
            Name = name;
            ElementName = elementName;
            Health = health;
        }
    }
}