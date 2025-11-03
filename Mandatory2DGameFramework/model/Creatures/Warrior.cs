using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;

namespace Mandatory2DGameFramework.model.Creatures
{
    public class Warrior : Creature
    {
        public Warrior(string name, ILogger? logger = null) : base(logger)
        {
            Name = name;
            HitPoint = 120;
        }

        /// <summary>
        /// Warriors deal extra damage if they have a weapon,otherwise bare hands.
        /// </summary>
        protected override int CalculateDamage()
        {
            int baseDamage = AttackWeapon?.Hit ?? 2; // bare hands stronger than normal
            int bonus = 0;

            // Example: warriors get +2 damage if they have a shield equipped
            if (DefenceItems.Exists(d => d is Shield))
                bonus += 2;

            return baseDamage + bonus;
        }

        /// <summary>
        /// Optional: log a special message before attacking.
        /// </summary>
        protected override void BeforeAttack()
        {
            base.BeforeAttack();
            _logger?.LogInfo($"{Name} roars and charges at the enemy!");
        }

        /// <summary>
        /// Optional: log extra info after attacking.
        /// </summary>
        protected override void AfterAttack(Creature target)
        {
            base.AfterAttack(target);
            _logger?.LogInfo($"{Name} finishes his attack on {target.Name}.");
        }
    }
}
