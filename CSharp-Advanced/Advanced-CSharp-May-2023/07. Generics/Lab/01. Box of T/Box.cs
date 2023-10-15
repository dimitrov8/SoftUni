using System;
using System.Collections.Generic;

namespace BoxOfT
{
    public class Box<T>
    {
        private Stack<T> box;

        public Box()
        {
            this.box = new Stack<T>();
        }

        public void Add(T element)
        {
            box.Push(element);
        }

        public T Remove()
        {
            if (box.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            return box.Pop();
        }

        public int Count => box.Count;
    }
}