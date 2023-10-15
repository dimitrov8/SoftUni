namespace Gym.Models.Equipment
{
    public class Kettlebell : Equipment
    {
        private const int WEIGHT = 10000;
        private const decimal PRICE = 80;

        public Kettlebell() 
            : base(WEIGHT, PRICE)
        {
        }
    }
}