namespace Threeuple
{
    public class CustomThreeuple<T1, T2, T3>
    {
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;

        public CustomThreeuple(T1 item1, T2 item2, T3 item3)
        {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
        }

        public override string ToString()
        {
            return $"{this._item1} -> {this._item2} -> {this._item3}";
        }
    }
}