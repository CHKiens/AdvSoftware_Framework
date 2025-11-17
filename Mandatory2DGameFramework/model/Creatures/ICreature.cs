using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Creatures
{
    public interface ICreature
    {
        string Name { get; }
        int GetCurrentHitPoints();
        bool GetIsAlive();
        void PerformAttack(Creature target);
        void ReceiveHit(int damage);
        void Loot(WorldObject obj);
    }

}
