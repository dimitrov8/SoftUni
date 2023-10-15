namespace NavalVessels.Models
{
    using Contracts;
    using System.Text;

    public class Battleship : Vessel, IBattleship
    {
        private const double ARMOR_THICKNESS = 300;

        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, ARMOR_THICKNESS)
        {
            this.SonarMode = false;
        }

        public bool SonarMode { get; private set; }

        public void ToggleSonarMode()
        {
            if (this.SonarMode)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
            }
            else if (!this.SonarMode)
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
            }

            this.SonarMode = !this.SonarMode;
        }

        public override void RepairVessel()
        {
            this.ArmorThickness = ARMOR_THICKNESS;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            string sonarModeOnOff = this.SonarMode ? "ON" : "OFF";
            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Sonar mode: {sonarModeOnOff}");

            return sb.ToString().Trim();
        }
    }
}