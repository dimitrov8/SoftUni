using System;
using System.Collections.Generic;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public bool IsEmpty => this.Count == 0;

        public void RandomString()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, this.Count);
            if (!this.IsEmpty)
            {
                this.RemoveAt(randomIndex);
                Console.WriteLine($"Item remove at index: {randomIndex}");
            }
            else
            {
                Console.WriteLine("RandomList is empty!");
                
            }
        }
    }
}