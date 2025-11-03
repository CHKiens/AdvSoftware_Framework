using Mandatory2DGameFramework.model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.strategy
{
    public class ArmorPenDamageStrategy : IDamageStrategy
    {
        public int CalculateDamage(Creature attacker, Creature target)
        {
            int baseDamage = attacker.AttackWeapon?.Hit ?? 2;
            int targetDefence = target.DefenceItems.ReduceHitPoint;

            // Warrior ignores 1/3 of total defence
            int reducedDefence = targetDefence - (targetDefence / 3);

            int finalDamage = Math.Max(0, baseDamage - reducedDefence);
            return finalDamage;
        }
    }

}
