using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
    public class ListyIterator<T> : IEnumerable<T>
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

        public void PrintAll()
        {
            if (this.list.Count == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            Console.WriteLine(string.Join(" ", this.list));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}