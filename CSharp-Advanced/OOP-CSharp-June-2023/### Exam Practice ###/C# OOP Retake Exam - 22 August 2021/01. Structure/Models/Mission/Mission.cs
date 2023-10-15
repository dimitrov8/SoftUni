namespace SpaceStation.Models.Mission
{
    using Astronauts.Contracts;
    using Contracts;
    using Planets.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var astronaut in astronauts)
            {
                while (astronaut.CanBreath && planet.Items.Any())
                {
                    foreach (string item in planet.Items)
                    {
                        astronaut.Bag.Items.Add(item);
                        astronaut.Breath();
                        planet.Items.Remove(item);
                        break;
                    }
                }
            }
        }
    }
}