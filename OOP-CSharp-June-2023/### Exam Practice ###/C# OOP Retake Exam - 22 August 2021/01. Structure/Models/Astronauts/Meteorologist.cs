namespace SpaceStation.Models.Astronauts
{
    using Contracts;

    public class Meteorologist : Astronaut, IAstronaut
    {
        private const double INITIAL_OXYGEN = 90;

        public Meteorologist(string name) 
            : base(name, INITIAL_OXYGEN)
        {
        }
    }
}