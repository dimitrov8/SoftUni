namespace SpaceStation.Core
{
    using Contracts;
    using Models.Astronauts;
    using Models.Astronauts.Contracts;
    using Models.Mission;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Repositories;
    using System;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly AstronautRepository astronautsRepo;
        private readonly PlanetRepository planetsRepo;
        private int exploredPlanetsCount;

        public Controller()
        {
            this.astronautsRepo = new AstronautRepository();
            this.planetsRepo = new PlanetRepository();
            this.exploredPlanetsCount = 0;
        }

        public string AddAstronaut(string type, string astronautName)
        {
            if (type != nameof(Biologist) && type != nameof(Meteorologist) && type != nameof(Geodesist))
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);

            IAstronaut astronaut = null;
            if (type == nameof(Biologist))
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == nameof(Meteorologist))
            {
                astronaut = new Meteorologist(astronautName);
            }
            else if (type == nameof(Geodesist))
            {
                astronaut = new Geodesist(astronautName);
            }

            this.astronautsRepo.Add(astronaut);

            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);

            foreach (string item in items)
            {
                planet.Items.Add(item);
            }

            this.planetsRepo.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var planet = this.planetsRepo.FindByName(planetName);
            var mission = new Mission();

            var suitableAstronauts = this.astronautsRepo.Models
                .Where(a => a.Oxygen > 60)
                .ToList();

            if (!suitableAstronauts.Any())
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);

            mission.Explore(planet, suitableAstronauts);
            this.exploredPlanetsCount++;
            return string.Format(OutputMessages.PlanetExplored, planetName, this.astronautsRepo.Models.Count(a => !a.CanBreath));
        }

        public string RetireAstronaut(string astronautName)
        {
            if (this.astronautsRepo.FindByName(astronautName) == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));

            var astronautToRetire = this.astronautsRepo.FindByName(astronautName);
            this.astronautsRepo.Remove(astronautToRetire);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.exploredPlanetsCount} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astronaut in this.astronautsRepo.Models)
            {
                string items = astronaut.Bag.Items.Any() ? string.Join(", ", astronaut.Bag.Items) : "none";
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                sb.AppendLine($"Bag items: {items}");
            }

            return sb.ToString().Trim();
        }
    }
}