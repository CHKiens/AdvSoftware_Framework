using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.strategy;

namespace Mandatory2DGameFramework.model.Creatures
{
    public class Hunter : Creature
    {
        public Hunter(string name, int posX, int posY, ILogger? logger = null)
            : base(logger)
        {
            // Basic Creature properties
            Name = name;
            PosX = posX;
            PosY = posY;
            HitPoint = 90;
            MoveRange = 4;

            // Combat behavior
            Combat.DamageStrategy = new CritDamageStrategy();

            // Default weapon
            Inventory.Loot(new AttackItem
            {
                Name = "Bow",
                Hit = 10,
                Range = 3
            });
        }
    }
}
