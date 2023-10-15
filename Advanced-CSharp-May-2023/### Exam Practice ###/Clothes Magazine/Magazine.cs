using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClothesMagazine
{
    public class Magazine
    {
        public Magazine(string type, int capacity)
        {
            this.Type = type;
            this.Capacity = capacity;
            this.Clothes = new List<Cloth>(capacity);
        }

        public string Type { get; set; }
        public int Capacity { get; set; }
        public List<Cloth> Clothes { get; set; }

        public int GetClothCount() => this.Clothes.Count;

        public void AddCloth(Cloth cloth)
        {
            if (this.Capacity > this.Clothes.Count)
            {
                this.Clothes.Add(cloth);
            }
        }

        public bool RemoveCloth(string color)
        {
            Cloth clothToRemove = this.Clothes.FirstOrDefault(c => c.Color == color);

            return this.Clothes.Remove(clothToRemove);
        }

        public Cloth GetSmallestCloth()
        {
            return this.Clothes.OrderBy(c => c.Size).FirstOrDefault();
        }

        public Cloth GetCloth(string color)
        {
            return this.Clothes.FirstOrDefault(c => c.Color == color);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.Type} magazine contains:");
            foreach (var cloth in this.Clothes.OrderBy(c => c.Size))
            {
                sb.AppendLine(cloth.ToString());
            }

            return sb.ToString().Trim();
        }
        
    }
}