namespace SpaceStation.Models.Bags
{
    using Contracts;
    using System.Collections.Generic;

    public class Backpack : IBag
    {
        public Backpack()
        {
            this.Items = new List<string>();
        }

        public ICollection<string> Items { get; }
    }
}