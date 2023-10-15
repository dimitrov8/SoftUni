namespace GenericBoxOfString
{
    public class Box<T>
    {
        private readonly string _value;

        public Box(string text)
        {
            _value = text;
        }

        public override string ToString()
        {
            return $"{typeof(T)}: {this._value}";
        }
    }
}