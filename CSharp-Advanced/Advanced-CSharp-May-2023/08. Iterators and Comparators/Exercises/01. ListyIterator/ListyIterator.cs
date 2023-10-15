using System;
using System.Collections.Generic;

namespace ListyIterator
{
    public class ListyIterator<T>
    {
        private List<T> list;
        private int currentIndex;

        public ListyIterator(params T[] inputList)
        {
            this.list = new List<T>(inputList);
        }

        public bool Move()
        {
            if (this.currentIndex < this.list.Count - 1)
            {
                this.currentIndex++;
                return true;
            }

            return false;
        }

        public bool HasNext() => this.currentIndex + 1 < this.list.Count;

        public void Print()
        {
            if (this.list.Count == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            Console.WriteLine(this.list[this.currentIndex]);
        }
    }
}