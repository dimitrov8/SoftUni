using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoeStore
{
    public class ShoeStore
    {
        public ShoeStore(string name, int storageCapacity)
        {
            this.Name = name;
            this.StorageCapacity = storageCapacity;
            this.Shoes = new List<Shoe>(storageCapacity);
        }

        public string Name { get; set; }
        public int StorageCapacity { get; set; }
        public List<Shoe> Shoes { get; set; }
        public int Count => this.Shoes.Count;

        public string AddShoe(Shoe shoe)
        {
            if (this.Count < this.StorageCapacity)
            {
                this.Shoes.Add(shoe);
                return $"Successfully added {shoe.Type} {shoe.Material} pair of shoes to the store.";
            }

            return "No more space in the storage room.";
        }

        public int RemoveShoes(string material)
        {
            int countOfShoesToRemoveByMaterial = this.Shoes.RemoveAll(s => s.Material == material.ToLower());
            return countOfShoesToRemoveByMaterial;
        }

        public List<Shoe> GetShoesByType(string type) => this.Shoes.Where(s => s.Type == type.ToLower()).ToList();

        public Shoe GetShoeBySize(double size) => this.Shoes.FirstOrDefault(s => s.Size == size);

        public string StockList(double size, string type)
        {
            List<Shoe> stockList = this.Shoes.Where(s => s.Size == size && s.Type == type.ToLower()).ToList();

            if (stockList.Count == 0)
            {
                return "No matches found!";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Stock list for size {size} - {type} shoes:");
            foreach (var shoe in stockList)
            {
                sb.AppendLine(shoe.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}