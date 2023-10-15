using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUniKindergarten
{
    public class Kindergarten
    {
        public Kindergarten(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Registry = new List<Child>(capacity);
        }
        
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Child> Registry { get; set; }

        public int ChildrenCount => this.Registry.Count;

        public bool AddChild(Child child)
        {
            if (this.Capacity <= this.ChildrenCount) return false;
            this.Registry.Add(child);
            return true;

        }

        public bool RemoveChild(string childFullName)
        {
            string[] fullName = childFullName.Split();
            string firstName = fullName[0];
            string lastName = fullName[1];

           Child childToRemove = this.Registry.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
           return this.Registry.Remove(childToRemove);
        }

        public Child GetChild(string childFullName)
        {
            string[] fullName = childFullName.Split();
            string firstName = fullName[0];
            string lastName = fullName[1];
            
            return this.Registry.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
        }

        public string RegistryReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Registered children in {this.Name}:");
            foreach (var child in this.Registry.OrderByDescending(c => c.Age).ThenBy(c => c.LastName).ThenBy(c => c.FirstName))
            {
                sb.AppendLine(child.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}