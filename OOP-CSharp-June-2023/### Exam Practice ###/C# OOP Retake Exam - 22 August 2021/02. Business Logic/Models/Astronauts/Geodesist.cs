namespace SpaceStation.Models.Astronauts
{
    using Contracts;

    public class Geodesist : Astronaut, IAstronaut
    {
        private const double INITIAL_OXYGEN = 50;

        public Geodesist(string name) 
            : base(name, INITIAL_OXYGEN)
        {
        }
    }
}