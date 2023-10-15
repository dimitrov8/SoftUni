using System.Collections.Generic;
using System.Text;

namespace GenericSwapMethodIntegers
{
    public class Box<T>
    {
            private readonly List<T> _items;
            public int Count => _items.Count;

            public Box()
            {
                _items = new List<T>();
            }

            public void Add(T item)
            {
                _items.Add(item);
            }

            public void Swap(int firstElementIndex, int secondElementIndex)
            {
                if (firstElementIndex >= 0 && firstElementIndex < _items.Count && secondElementIndex >= 0 && secondElementIndex < _items.Count)
                {
                    (_items[firstElementIndex], _items[secondElementIndex]) = (_items[secondElementIndex], _items[firstElementIndex]);
                }
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in _items)
                {
                    sb.AppendLine($"{typeof(T)}: {item}");
                }

                return sb.ToString().Trim();
            }
    }
}