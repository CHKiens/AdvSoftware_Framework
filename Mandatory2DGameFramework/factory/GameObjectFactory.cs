using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.factory
{
    public abstract class GameObjectFactory
    {
        public static Creature CreateWarrior(ILogger? logger = null)
        {
            logger.LogInfo("Creating Warrior Creature");
            return new Warrior
            {
                Name = "Warrior",
                HitPoints = 150,
                AttackPower = 25,
                Defense = 15,
                Speed = 10
            };
        }
    }
}
