namespace Heroes.Repositories
{
    using Contracts;
    using Models.Contracts;
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

        public void Add(IWeapon model) => this.weapons.Add(model);

        public bool Remove(IWeapon model) => this.weapons.Remove(model);

        public IWeapon FindByName(string name) => this.weapons.FirstOrDefault(w => w.Name == name);
    }
}