using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishingNet
{
    public class Net
    {
        private readonly ICollection<Fish> fish;
        public Net(string material, int capacity)
        {
            this.Material = material;
            this.Capacity = capacity;
            this.fish = new List<Fish>();
        }
        public string Material { get; set; }
        public int Capacity { get; set; }

        public int Count => this.fish.Count;

        public IReadOnlyCollection<Fish> Fish => (IReadOnlyCollection<Fish>)this.fish;

        public string AddFish(Fish fish)
        {
            if (string.IsNullOrWhiteSpace(fish.FishType) || fish.Length <= 0 || fish.Weight <= 0)
            {
                return "Invalid fish.";
            }
            
            if (this.Fish.Count + 1 > this.Capacity)
            {
                return "Fishing net is full.";
            }

            this.fish.Add(fish);
            return $"Successfully added {fish.FishType} to the fishing net.";
        }

        public bool ReleaseFish(double weight)
        {
                Fish fishToRelease = this.fish.FirstOrDefault(f => f.Weight == weight);
                if (fishToRelease != null)
                {
                    return this.fish.Remove(fishToRelease);
                }

                return false;
        }

        public Fish GetFish(string fishType)
        {
            return this.fish.FirstOrDefault(f => f.FishType == fishType);
        }

        public Fish GetBiggestFish()
        {
            return this.fish.OrderByDescending(f => f.Length).FirstOrDefault();
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Into the {this.Material}:");
            foreach (var item in this.Fish.OrderByDescending(f => f.Length))
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}