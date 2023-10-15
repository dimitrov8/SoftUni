namespace GenericBoxOfInteger
{
    public class Box<T>
    {
        private readonly int _value;

        public Box(int integer)
        {
            this._value = integer;
        }

        public override string ToString()
        {
            return $"{typeof(T)}: {this._value}";
        }
    }
}