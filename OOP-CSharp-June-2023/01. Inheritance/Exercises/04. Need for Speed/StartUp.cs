namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            RaceMotorcycle raceMotorcycle = new RaceMotorcycle(200, 20);
            raceMotorcycle.Drive(2);

            SportCar sportCar = new SportCar(600, 40);
            sportCar.Drive(4.69);
        }
    }
}
