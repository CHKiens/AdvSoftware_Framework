using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.model.strategy;
using Mandatory2DGameFramework.worlds;

namespace Mandatory2DGameFramework.model.Creatures
{
    public class Warrior : Creature
    {
        public Warrior(string name, int posX, int posY, ILogger? logger = null)
            : base(logger)
        {
            Name = name;
            PosX = posX;
            PosY = posY;
            HitPoint = 150;
            MoveRange = 2;

            Combat.DamageStrategy = new ArmorPenDamageStrategy();

            Inventory.Loot(new AttackItem
            {
                Name = "Sword",
                Hit = 20,
                Range = 1
            });
        }
    }

}
