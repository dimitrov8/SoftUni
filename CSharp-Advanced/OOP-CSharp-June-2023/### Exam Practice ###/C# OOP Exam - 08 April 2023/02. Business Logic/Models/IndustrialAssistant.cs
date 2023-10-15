namespace RobotService.Models
{
    public class IndustrialAssistant : Robot
    {
        private const int BATTERY_CAPACITY = 40000;
        private const int CONVERTION_CAPACITY_INDEX = 5000;

        public IndustrialAssistant(string model) : base(model, BATTERY_CAPACITY, CONVERTION_CAPACITY_INDEX)
        {
        }
    }
}