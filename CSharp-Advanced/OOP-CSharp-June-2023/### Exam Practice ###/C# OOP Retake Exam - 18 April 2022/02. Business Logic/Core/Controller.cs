namespace Heroes.Core
{
    using Contracts;
    using Models.Heroes;
    using Models.Map;
    using Models.Weapons;
    using Repositories;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly HeroRepository heroes;
        private readonly WeaponRepository weapons;

        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            var hero = this.heroes.FindByName(name);

            if (hero != null)
                return string.Format(OutputMessages.HeroAlreadyExist, name);

            if (type != nameof(Barbarian) && type != nameof(Knight))
                return string.Format(OutputMessages.HeroTypeIsInvalid);

            if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
            }
            else if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
            }

            this.heroes.Add(hero);

            if (type == nameof(Barbarian))
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);

            return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            var weapon = this.weapons.FindByName(name);

            if (weapon != null)
                return string.Format(OutputMessages.WeaponAlreadyExists, name);

            if (type != nameof(Mace) && type != nameof(Claymore))
                return string.Format(OutputMessages.WeaponTypeIsInvalid);

            if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            else if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);
            }

            this.weapons.Add(weapon);

            return string.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = this.heroes.FindByName(heroName);
            var weapon = this.weapons.FindByName(weaponName);

            if (hero == null)
                return string.Format(OutputMessages.HeroDoesNotExist, heroName);

            if (weapon == null)
                return string.Format(OutputMessages.WeaponDoesNotExist, weaponName);

            if (hero.Weapon != null)
                return string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName);

            hero.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        }

        public string StartBattle()
        {
            var map = new Map();
            return map.Fight(this.heroes.Models.Where(h => h.IsAlive && h.Weapon != null).ToList());
        }

        public string HeroReport()
        {
            var sb = new StringBuilder();

            foreach (var hero in this.heroes.Models
                         .OrderBy(h => h.GetType().Name)
                         .ThenByDescending(h => h.Health)
                         .ThenBy(h => h.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                sb.Append("--Weapon: ");
                sb.Append(hero.Weapon == null ? "Unarmed" : $"{hero.Weapon.Name}");
                sb.AppendLine();
            }

            return sb.ToString().Trim();
        }
    }
}