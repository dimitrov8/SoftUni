using System.Collections;
using System.Collections.Generic;

namespace Froggy
{
    public class Lake : IEnumerable<int>
    {
        private List<int> stones;
        private int Count => this.stones.Count;

        public Lake(params int[] stones)
        {
            this.stones = new List<int>(stones);
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i+= 2)
            {
                if (i % 2 == 0)
                {
                    yield return this.stones[i];
                }
            }

            for (int j = this.Count - 1; j >= 0; j--)
            {
                if (j % 2 != 0)
                {
                    yield return this.stones[j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}