namespace PlanetWars.Core
{
    using Contracts;
    using Models.MilitaryUnits;
    using Models.MilitaryUnits.Contracts;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Models.Weapons;
    using Models.Weapons.Contracts;
    using Repositories;
    using System;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly PlanetRepository planets;

        public Controller()
        {
            this.planets = new PlanetRepository();
        }

        public string CreatePlanet(string name, double budget)
        {
            var planet = this.planets.FindByName(name);

            if (planet != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            planet = new Planet(name, budget);
            this.planets.AddItem(planet);
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            var planet = this.planets.FindByName(planetName);

            if (planet == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            if (unitTypeName != nameof(AnonymousImpactUnit)
                && unitTypeName != nameof(SpaceForces)
                && unitTypeName != nameof(StormTroopers))
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));

            IMilitaryUnit unit = null;
            if (unitTypeName == nameof(AnonymousImpactUnit))
            {
                unit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else if (unitTypeName == nameof(StormTroopers))
            {
                unit = new StormTroopers();
            }

            if (planet.Army.Any(u => u.GetType().Name == unitTypeName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            var planet = this.planets.FindByName(planetName);

            if (planet == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            if (weaponTypeName != nameof(BioChemicalWeapon)
                && weaponTypeName != nameof(NuclearWeapon)
                && weaponTypeName != nameof(SpaceMissiles))
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));

            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));

            IWeapon weapon = null;
            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(SpaceMissiles))
            {
                weapon = new SpaceMissiles(destructionLevel);
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string SpecializeForces(string planetName)
        {
            var planet = this.planets.FindByName(planetName);

            if (planet == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));

            if (!planet.Army.Any())
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);

            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var firstPlanet = this.planets.FindByName(planetOne);
            var secondPlanet = this.planets.FindByName(planetTwo);

            bool noWinner = false;
            IPlanet winnerPlanet = null;
            IPlanet loserPlanet = null;

            if (firstPlanet.MilitaryPower == secondPlanet.MilitaryPower)
            {
                if (firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon))
                    && secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
                {
                    noWinner = true;
                }

                else if (!firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon))
                         && !secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
                {
                    noWinner = true;
                }

                else if (firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
                {
                    winnerPlanet = firstPlanet;
                    loserPlanet = secondPlanet;
                }
                else if (secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
                {
                    winnerPlanet = secondPlanet;
                    loserPlanet = firstPlanet;
                }
            }

            else if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                winnerPlanet = firstPlanet;
                loserPlanet = secondPlanet;
            }
            else if (secondPlanet.MilitaryPower > firstPlanet.MilitaryPower)
            {
                winnerPlanet = secondPlanet;
                loserPlanet = firstPlanet;
            }

            if (noWinner)
            {
                firstPlanet.Spend(firstPlanet.Budget / 2);
                secondPlanet.Spend(secondPlanet.Budget / 2);
                return string.Format(OutputMessages.NoWinner);
            }

            winnerPlanet.Spend(winnerPlanet.Budget / 2);
            winnerPlanet.Profit(loserPlanet.Budget / 2);
            winnerPlanet.Profit(loserPlanet.Weapons.Sum(w => w.Price) + loserPlanet.Army.Sum(a => a.Cost));

            this.planets.RemoveItem(loserPlanet.Name);
            return string.Format(OutputMessages.WinnigTheWar, winnerPlanet.Name, loserPlanet.Name);
        }

        public string ForcesReport()
        {
            var orderedPlanets = this.planets.Models
                .OrderByDescending(p => p.MilitaryPower)
                .ThenBy(p => p.Name)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in orderedPlanets)
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().Trim();
        }
    }
}