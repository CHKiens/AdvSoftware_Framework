using Mandatory2DGameFramework.model.Creatures;
using System;

namespace Mandatory2DGameFramework.model.strategy
{
    public class CritDamageStrategy : IDamageStrategy
    {
        private readonly Random _rng = new Random();

        public int CalculateDamage(Creature attacker, Creature target)
        {
            int baseDamage = attacker.Inventory.EquippedWeapon?.Hit ?? 2;

            int totalDefence = target.Inventory.Defence.ReduceHitPoint;

            // 25% chance to do double damage
            bool crit = _rng.NextDouble() < 0.25;

            int damage = Math.Max(0, baseDamage - totalDefence);

            return crit ? damage * 2 : damage;
        }
    }
}
