namespace PlanetWars.Repositories
{
    using Contracts;
    using Models.Weapons.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void AddItem(IWeapon model) => this.weapons.Add(model);

        public IWeapon FindByName(string name) => this.weapons.FirstOrDefault(w => w.GetType().Name == name);

        public bool RemoveItem(string name) => this.weapons.Remove(this.weapons.FirstOrDefault(w => w.GetType().Name == name));
    }
}