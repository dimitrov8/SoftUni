namespace Tuple
{
    public class CustomTuple<T1, T2>
    {
        private readonly T1 _item1;
        private readonly T2 _item2;
        
        public CustomTuple(T1 item1, T2 item2)
        {
            this._item1 = item1;
            this._item2 = item2;
        }

        public override string ToString()
        {
            return $"{this._item1} -> {this._item2}";
        }
    }
}