namespace SpaceStation.Models.Astronauts
{
    using Contracts;

    public class Biologist : Astronaut, IAstronaut
    {
        private const double INITIAL_OXYGEN = 70;

        public Biologist(string name) 
            : base(name, INITIAL_OXYGEN)
        {
        }

        public override void Breath() => this.Oxygen -= 5;
    }
}