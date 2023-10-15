namespace Gym.Core
{
    using Contracts;
    using Models.Athletes;
    using Models.Athletes.Contracts;
    using Models.Equipment;
    using Models.Equipment.Contracts;
    using Models.Gyms;
    using Models.Gyms.Contracts;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly EquipmentRepository equipment;
        private readonly ICollection<IGym> gyms;

        public Controller()
        {
            this.equipment = new EquipmentRepository();
            this.gyms = new List<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType != nameof(BoxingGym) && gymType != nameof(WeightliftingGym))
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);

            IGym gym = null;
            if (gymType == nameof(BoxingGym))
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == nameof(WeightliftingGym))
            {
                gym = new WeightliftingGym(gymName);
            }

            this.gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType != nameof(BoxingGloves) && equipmentType != nameof(Kettlebell))
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);

            IEquipment equipment = null;
            if (equipmentType == nameof(BoxingGloves))
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == nameof(Kettlebell))
            {
                equipment = new Kettlebell();
            }

            this.equipment.Add(equipment);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var desiredEquipment = this.equipment.FindByType(equipmentType);
            if (desiredEquipment == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));

            var gym = this.gyms.First(g => g.Name == gymName);
            gym.AddEquipment(desiredEquipment);
            this.equipment.Remove(desiredEquipment);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            if (athleteType != nameof(Boxer) && athleteType != nameof(Weightlifter))
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);

            IAthlete athlete = null;

            if (athleteType == nameof(Boxer))
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == nameof(Weightlifter))
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }

            var gym = this.gyms.First(g => g.Name == gymName);
            string gymType = gym.GetType().Name;
            
            if (gymType == nameof(BoxingGym) && athleteType != nameof(Boxer) ||
                gymType == nameof(WeightliftingGym) && athleteType != nameof(Weightlifter))
                return string.Format(OutputMessages.InappropriateGym);

            gym.AddAthlete(athlete);

            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string TrainAthletes(string gymName)
        {
            var gym = this.gyms.First(g => g.Name == gymName);
            gym.Exercise();

            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = this.gyms.First(g => g.Name == gymName);

            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            foreach (var gym in this.gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().Trim();
        }
    }
}