using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Creatures
{
    public abstract class Creature
    {
        private readonly ILogger? _logger;
        public string Name { get; set; }
        public int HitPoint { get; set; }

        public bool isAlive { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }


        // Todo consider how many attack / defence weapons are allowed
        public AttackItem?   Attack { get; set; }
        public DefenceItem?  Defence { get; set; }

        public Creature(ILogger? logger = null)
        {
            Name = string.Empty;
            HitPoint = 100;
            isAlive = true;
            _logger = logger;
            Attack = null;
            Defence = null;

        }

        public int Hit()
        {
            if (Attack != null)
            {
                return Attack.Hit;
            }
            else
            {
                return 1;  // Hit uden våben, sat til 1 som default
            }
        }

        public void ReceiveHit(int hit)
        {
            if (!isAlive)
            {
                _logger?.LogWarning($"{Name} is already dead and cannot receive more hits.");
                return;
            }
            int reducedHit = hit;
            if (Defence != null)
            {
                reducedHit -= Defence.ReduceHitPoint;
            }
            if (reducedHit < 0)
            {
                reducedHit = 0;
            }
            _logger?.LogInfo($"{Name} received hit of {reducedHit} points.");
            HitPoint -= reducedHit;
            if (HitPoint <= 0)
            {
                HitPoint = 0;
                isAlive = false;
                _logger?.LogInfo($"{Name} is dead.");
            }
        }

        public void Loot(WorldObject obj)
        {
            if (!obj.Lootable)
            {
                _logger?.LogWarning($"{Name} tried to loot {obj.Name}, but it is not lootable.");
                return;
            }

            if (obj is AttackItem attackItem)
            {
                if (Attack != null)
                {
                    _logger?.LogInfo($"{Name} replaced attack item: {Attack.Name} with {attackItem.Name}.");
                }
                Attack = attackItem;
                _logger?.LogInfo($"{Name} looted attack item: {attackItem.Name}.");
            }



            else if (obj is DefenceItem defenceItem)
            {
                Defence = defenceItem;
                _logger?.LogInfo($"{Name} looted defence item: {defenceItem.Name}.");
            }
            else
            {
                _logger?.LogWarning($"{Name} tried to loot {obj.Name}, but it is neither an attack nor a defence item.");
            }
        }





        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(HitPoint)}={HitPoint.ToString()}, {nameof(Attack)}={Attack}, {nameof(Defence)}={Defence}}}, {nameof(PosY)}={PosY}}}, {nameof(PosX)}={PosX}}}";
        }
    }
}
