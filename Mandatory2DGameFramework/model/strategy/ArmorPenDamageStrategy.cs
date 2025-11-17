using Mandatory2DGameFramework.model.Creatures;
using System;

namespace Mandatory2DGameFramework.model.strategy
{
    public class ArmorPenDamageStrategy : IDamageStrategy
    {
        public int CalculateDamage(Creature attacker, Creature target)
        {
            int baseDamage = attacker.Inventory.EquippedWeapon?.Hit ?? 2;

            int targetDefence = target.Inventory.Defence.ReduceHitPoint;

            // Ignore 1/3 of total defence
            int reducedDefence = targetDefence - (targetDefence / 3);

            int finalDamage = Math.Max(0, baseDamage - reducedDefence);

            return finalDamage;
        }
    }
}
