using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VetClinic
{
    public class Clinic
    {
        private readonly List<Pet> data;
        public int Capacity { get; private set; }

        public int Count => this.data.Count;
        
        public Clinic(int capacity)
        {
            this.Capacity = capacity;
            this.data = new List<Pet>(this.Capacity);
        }

        public void Add(Pet pet)
        {
            if (this.Count >= this.Capacity) return;
            this.data.Add(pet);
            this.Capacity--;
        }

        public bool Remove(string name)
        {
            Pet petToRemove = this.data.Find(p => p.Name == name);
            if (petToRemove == null)
            {
                return false;
            }

            this.Capacity++;
            return this.data.Remove(petToRemove);;
        }

        public Pet GetPet(string name, string owner)
        {
            return this.data.FirstOrDefault(p => p.Name == name && p.Owner == owner);
        }

        public Pet GetOldestPet()
        {
            return this.data.OrderByDescending(p => p.Age).FirstOrDefault();
        }

        public string GetStatistics()
        {
            var sb = new StringBuilder();
            sb.AppendLine("The clinic has the following patients:");
            foreach (var pet in this.data)
            {
                sb.AppendLine($"Pet {pet.Name} with owner: {pet.Owner}");
            }

            return sb.ToString().Trim();
        }
    }
}