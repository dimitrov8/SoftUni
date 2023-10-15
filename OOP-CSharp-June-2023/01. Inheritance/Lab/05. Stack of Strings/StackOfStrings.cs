using System.Collections.Generic;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty() => this.Count == 0;

        public void AddRange(Stack<string> values)
        {
            while (values.Count > 0)
            {
                this.Push(values.Pop());
            }
        }
    }
}