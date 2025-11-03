using Mandatory2DGameFramework.model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.strategy
{
    public class CritDamageStrategy : IDamageStrategy
    {
        private readonly Random _rng = new Random();

        public int CalculateDamage(Creature attacker, Creature target)
        {
            int baseDamage = attacker.AttackWeapon?.Hit ?? 2;
            int totalDefence = target.DefenceItems.Sum(d => d.ReduceHitPoint);

            // 25% chance to do double damage
            bool crit = _rng.NextDouble() < 0.25;
            int damage = Math.Max(0, baseDamage - totalDefence);

            return crit ? damage * 2 : damage;
        }
    }

}
