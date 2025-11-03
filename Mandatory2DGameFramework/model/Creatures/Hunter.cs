using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.model.strategy;
using Mandatory2DGameFramework.worlds;


namespace Mandatory2DGameFramework.model.Creatures
{
    public class Hunter : Creature
    {
        public Hunter(string name, ILogger? logger = null) : base(logger)
        {
            Name = name;
            HitPoint = 90;
            MoveRange = 4;
            DamageStrategy = new CritDamageStrategy();

            // Equip default attack weapon
            AttackWeapon = new AttackItem
            {
                Name = "Bow",
                Hit = 10,
                Range = 3,
            };
        }
    }
}
