using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheRace
{
    public class Race
    {
        public List<Racer> Racers { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public int Count => this.Racers.Count;

        public Race(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Racers = new List<Racer>(capacity);
        }

        public void Add(Racer racer)
        {
            if (this.Count < this.Capacity)
            {
                this.Racers.Add(racer);
            }
        }

        public bool Remove(string name)
        {
            Racer racerToRemove = this.Racers.FirstOrDefault(r => r.Name == name);
            return this.Racers.Remove(racerToRemove);
        }

        public Racer GetOldestRacer()
        {
            return this.Racers.OrderByDescending(r => r.Age).FirstOrDefault();
        }

        public Racer GetRacer(string name)
        {
            return this.Racers.FirstOrDefault(r => r.Name == name);
        }

        public Racer GetFastestRacer()
        {
            return this.Racers.OrderByDescending(r => r.Car.Speed).FirstOrDefault();
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Racers participating at {this.Name}:");
            foreach (var racer in this.Racers)
            {
                sb.AppendLine(racer.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}