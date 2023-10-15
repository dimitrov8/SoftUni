namespace NavalVessels.Core
{
    using Contracts;
    using Models;
    using Models.Contracts;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly VesselRepository vessels;
        private readonly ICollection<ICaptain> captains;

        public Controller()
        {
            this.vessels = new VesselRepository();
            this.captains = new HashSet<ICaptain>();
        }

        public string HireCaptain(string fullName)
        {
            if (this.captains.FirstOrDefault(c => c.FullName == fullName) != null)
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);

            var captain = new Captain(fullName);
            this.captains.Add(captain);

            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (this.vessels.FindByName(name) != null)
            {
                var existingVessel = this.vessels.FindByName(name);
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, existingVessel.GetType().Name, name);
            }

            if (vesselType != nameof(Submarine) && vesselType != nameof(Battleship))
                return string.Format(OutputMessages.InvalidVesselType);

            IVessel vessel = null;
            if (vesselType == nameof(Submarine))
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == nameof(Battleship))
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            
            this.vessels.Add(vessel);

            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            if (this.captains.FirstOrDefault(c => c.FullName == selectedCaptainName) == null)
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);

            if (this.vessels.FindByName(selectedVesselName) == null)
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);

            var vessel = this.vessels.FindByName(selectedVesselName);
            if (vessel.Captain != null)
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);

            var captain = this.captains.First(c => c.FullName == selectedCaptainName);
            vessel.Captain = captain;
            captain.AddVessel(vessel);

            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string CaptainReport(string captainFullName)
        {
            var captain = this.captains.FirstOrDefault(c => c.FullName == captainFullName);
            return captain?.Report();
        }

        public string VesselReport(string vesselName)
        {
            var vessel = this.vessels.FindByName(vesselName);
            return vessel?.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            if (this.vessels.FindByName(vesselName) == null)
                return string.Format(OutputMessages.VesselNotFound, vesselName);

            var vessel = this.vessels.FindByName(vesselName);
            if (vessel.GetType() == typeof(Battleship))
            {
                var battleShip = (Battleship)vessel;
                battleShip.ToggleSonarMode();

                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }

            var submarine = (Submarine)vessel;
            submarine.ToggleSubmergeMode();

            return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            if (this.vessels.FindByName(attackingVesselName) == null)
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);

            if (this.vessels.FindByName(defendingVesselName) == null)
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);

            var attackingVessel = this.vessels.FindByName(attackingVesselName);
            if (attackingVessel.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            
            var defendingVessel = this.vessels.FindByName(defendingVesselName);
            if (defendingVessel.ArmorThickness == 0)
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);

            attackingVessel.Attack(defendingVessel);

            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName,
                defendingVessel.ArmorThickness);
        }

        public string ServiceVessel(string vesselName)
        {
            if (this.vessels.FindByName(vesselName) == null)
                return string.Format(OutputMessages.VesselNotFound, vesselName);

            var vessel = this.vessels.FindByName(vesselName);
            vessel.RepairVessel();

            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }
    }
}