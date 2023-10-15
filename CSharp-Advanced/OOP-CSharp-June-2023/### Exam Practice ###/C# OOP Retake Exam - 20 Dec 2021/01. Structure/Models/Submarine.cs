namespace NavalVessels.Models
{
    using Contracts;
    using System.Text;

    public class Submarine : Vessel, ISubmarine
    {
        private const double ARMOR_THICKNESS = 200;

        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, ARMOR_THICKNESS)
        {
            this.SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; }

        public void ToggleSubmergeMode()
        {
            if (this.SubmergeMode)
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
            else if (!this.SubmergeMode)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }

            this.SubmergeMode = !this.SubmergeMode;
        }

        public override void RepairVessel()
        {
            this.ArmorThickness = ARMOR_THICKNESS;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            string submergeModeOnOff = this.SubmergeMode ? "ON" : "OFF";
            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Submerge mode: {submergeModeOnOff}");

            return sb.ToString().Trim();
        }
    }
}