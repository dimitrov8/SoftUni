namespace Formula1.Core
{
    using Contracts;
    using Models;
    using Models.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using System;
    using System.Linq;
    using System.Text;
    using Utilities;

    public class Controller : IController
    {
        private readonly RaceRepository races;
        private readonly PilotRepository pilots;
        private readonly FormulaOneCarRepository cars;

        public Controller()
        {
            this.races = new RaceRepository();
            this.pilots = new PilotRepository();
            this.cars = new FormulaOneCarRepository();
        }

        public string CreatePilot(string fullName)
        {
            var existingPilot = this.pilots.FindByName(fullName);

            if (existingPilot != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));

            var pilot = new Pilot(fullName);
            this.pilots.Add(pilot);

            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            var existingCar = this.cars.FindByName(model);
            if (existingCar != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));

            if (type != nameof(Ferrari) && type != nameof(Williams))
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));

            IFormulaOneCar car = null;
            if (type == nameof(Ferrari))
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == nameof(Williams))
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }

            this.cars.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            var existingRace = this.races.FindByName(raceName);
            if (existingRace != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));

            var race = new Race(raceName, numberOfLaps);
            this.races.Add(race);

            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var existingPilot = this.pilots.FindByName(pilotName);
            if (existingPilot == null || existingPilot.Car != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));

            var existingCar = this.cars.FindByName(carModel);
            if (existingCar == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));

            existingPilot.AddCar(existingCar);
            this.cars.Remove(existingCar);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, existingCar.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var existingRace = this.races.FindByName(raceName);
            if (existingRace == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            var existingPilot = this.pilots.FindByName(pilotFullName);
            if (existingPilot == null || !existingPilot.CanRace || existingRace.Pilots.Contains(existingPilot))
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));

            existingRace.Pilots.Add(existingPilot);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {
            var existingRace = this.races.FindByName(raceName);
            if (existingRace == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            if (existingRace.Pilots.Count < 3)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));

            if (existingRace.TookPlace)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));

            var fastestPilots = this.pilots.Models
                .OrderByDescending(c => c.Car.RaceScoreCalculator(existingRace.NumberOfLaps))
                .Take(3)
                .ToList();
            fastestPilots[0].WinRace();

            existingRace.TookPlace = true;

            var sb = new StringBuilder();
            sb.AppendLine(string.Format(OutputMessages.PilotFirstPlace, fastestPilots[0].FullName, raceName));
            sb.AppendLine(string.Format(OutputMessages.PilotSecondPlace, fastestPilots[1].FullName, raceName));
            sb.AppendLine(string.Format(OutputMessages.PilotThirdPlace, fastestPilots[2].FullName, raceName));

            return sb.ToString().Trim();
        }

        public string RaceReport()
        {
            var sb = new StringBuilder();
            
            foreach (var race in this.races.Models.Where(r => r.TookPlace))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().Trim();
        }

        public string PilotReport()
        {
            var sb = new StringBuilder();
            
            foreach (var pilot in this.pilots.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}