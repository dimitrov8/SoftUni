namespace Heroes.Models.Map
{
    using Contracts;
    using Heroes;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Messages;

    public class Map : IMap
    {
        public string Fight(ICollection<IHero> heroes)
        {
            var knights = heroes.Where(h => h.GetType().Name == nameof(Knight)).ToList();
            var barbarians = heroes.Where(h => h.GetType().Name == nameof(Barbarian)).ToList();

            while (knights.Any(k => k.IsAlive) && barbarians.Any(b => b.IsAlive))
            {
                foreach (var knight in knights.Where(k => k.IsAlive && k.Weapon.Durability > 0))
                {
                    foreach (var barbarian in barbarians.Where(b => b.IsAlive))
                    {
                        barbarian.TakeDamage(knight.Weapon.DoDamage());
                    }
                }

                foreach (var barbarian in barbarians.Where(b => b.IsAlive && b.Weapon.Durability > 0))
                {
                    foreach (var knight in knights.Where(k => k.IsAlive))
                    {
                        knight.TakeDamage(barbarian.Weapon.DoDamage());
                    }
                }
            }

            return knights.Any(k => k.IsAlive)
                ? string.Format(OutputMessages.MapFightKnightsWin, knights.Count(k => !k.IsAlive))
                : string.Format(OutputMessages.MapFigthBarbariansWin, barbarians.Count(b => !b.IsAlive));
        }
    }
}