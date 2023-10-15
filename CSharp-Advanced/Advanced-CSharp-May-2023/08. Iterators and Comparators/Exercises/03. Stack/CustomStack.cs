using System;
using System.Collections;
using System.Collections.Generic;

namespace Stack
{
    public class CustomStack<T> : IEnumerable<T>
    {
        private List<T> customStack;
        private int position = -1;

        public CustomStack()
        {
            this.customStack = new List<T>();
        }

        public void Push(params T[] elements)
        {
            foreach (var element in elements)
            {
                this.customStack.Insert(++this.position, element);
            }
        }

        public void Pop()
        {
            if (this.customStack.Count == 0)
            {
                throw new InvalidOperationException("No elements");
            }

            this.customStack.RemoveAt(this.position--);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.position; i >= 0; i--)
            {
                yield return this.customStack[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}