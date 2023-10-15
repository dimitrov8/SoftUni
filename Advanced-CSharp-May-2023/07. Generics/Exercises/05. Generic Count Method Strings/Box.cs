using System;
using System.Collections.Generic;

namespace GenericCountMethodStrings
{
    public class Box<T> where T : IComparable<T>
    {
        private readonly List<T> _items;
        public int ItemsCount => this._items.Count;

        public Box()
        {
            this._items = new List<T>();
        }

        public void Add(T item)
        {
           this._items.Add(item);
        }

        public int Count(T inputItem)
        {
            int count = 0;
            foreach (var item  in _items)
            {
                if (item.CompareTo(inputItem) == 1)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
