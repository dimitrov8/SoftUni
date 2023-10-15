namespace Gym.Repositories
{
    using Contracts;
    using Models.Equipment.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly ICollection<IEquipment> equipment;

        public EquipmentRepository()
        {
            this.equipment = new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => (IReadOnlyCollection<IEquipment>)this.equipment;

        public void Add(IEquipment equipment) => this.equipment.Add(equipment);

        public bool Remove(IEquipment equipment) => this.equipment.Remove(equipment);

        public IEquipment FindByType(string type) => this.equipment.FirstOrDefault(e => e.GetType().Name == type);
    }
}