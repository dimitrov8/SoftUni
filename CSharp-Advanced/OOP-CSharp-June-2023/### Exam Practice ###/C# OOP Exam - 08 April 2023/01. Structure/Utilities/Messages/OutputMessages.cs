namespace RobotService.Utilities.Messages
{
    public class OutputMessages
    {
        public const string ROBOT_CANNOT_BE_CREATED = "Robot type {0} cannot be created.";
        public const string ROBOT_CREATED_SUCCESSFULLY = "{0} {1} is created and added to the RobotRepository.";

        public const string SUPPLEMENT_CANNOT_BE_CREATED = "{0} is not compatible with our robots.";
        public const string SUPPLEMENT_CREATED_SUCCESSFULLY = "{0} is created and added to the SupplementRepository.";

        public const string ALL_MODELS_UPGRADED = "All {0} are already upgraded!";
        public const string UPGRADE_SUCCESSFUL = "{0} is upgraded with {1}.";

        public const string ROBOTS_FED = "Robots fed: {0}";

        public const string UNABLE_TO_PERFORM = "Unable to perform service, {0} not supported!";
        public const string MORE_POWER_NEEDED = "{0} cannot be executed! {1} more power needed.";
        public const string PERFORMED_SUCCESSFULLY = "{0} is performed successfully with {1} robots.";
    }
}