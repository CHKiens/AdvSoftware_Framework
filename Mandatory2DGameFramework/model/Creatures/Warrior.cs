using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.model.strategy;
using Mandatory2DGameFramework.worlds;

namespace Mandatory2DGameFramework.model.Creatures
{
    public class Warrior : Creature
    {
        public Warrior(string name, ILogger? logger = null) : base(logger)
        {
            Name = name;
            DamageStrategy = new ArmorPenDamageStrategy();
        }
    }

}
