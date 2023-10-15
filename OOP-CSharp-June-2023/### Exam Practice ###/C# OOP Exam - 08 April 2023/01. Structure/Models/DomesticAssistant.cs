namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        private const int BATTERY_CAPACITY = 20000;
        private const int CONVERTION_CAPACITY_INDEX = 2000;

        public DomesticAssistant(string model) : base(model, BATTERY_CAPACITY, CONVERTION_CAPACITY_INDEX)
        {
        }
    }
}