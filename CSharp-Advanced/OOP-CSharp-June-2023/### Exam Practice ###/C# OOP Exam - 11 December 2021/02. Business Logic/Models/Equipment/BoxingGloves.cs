namespace Gym.Models.Equipment
{
    public class BoxingGloves : Equipment
    {
        private const int WEIGHT = 227;
        private const decimal PRICE = 120;

        public BoxingGloves() 
            : base(WEIGHT, PRICE)
        {
        }
    }
}