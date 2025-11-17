using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.Creatures;
using Mandatory2DGameFramework.model.strategy;

namespace Mandatory2DGameFramework.model.components
{
    public class CombatComponent
    {
        /// <summary>
        /// Handles all combat-related operations for a creature.
        /// Supports attack strategies and receiving damage.
        /// </summary>
        private readonly Creature _owner;
        private readonly ILogger? _logger;

        /// <summary>
        /// Strategy used to calculate damage during attacks.
        /// </summary>
        public IDamageStrategy? DamageStrategy { get; set; }

        /// <summary>
        /// Constructor for CombatComponent.
        /// Takes the owning creature and an optional logger.
        /// </summary>

        public CombatComponent(Creature owner, ILogger? logger = null)
        {
            _owner = owner;
            _logger = logger;
        }

        /// <summary>
        /// Attacks a target creature, applying damage based on the damage strategy.
        /// </summary>
        /// <param name="target">Target creature</param>
        public void Attack(Creature target)
        {
            if (!_owner.GetIsAlive() || !target.GetIsAlive())
            {
                _logger?.LogWarning($"{_owner.Name} cannot attack {target.Name}");
                return;
            }

            int damage = DamageStrategy?.CalculateDamage(_owner, target) ?? (_owner.Inventory.EquippedWeapon?.Hit ?? 1);
            target.ReceiveHit(damage);

            _logger?.LogInfo($"{_owner.Name} attacked {target.Name} for {damage} damage.");
        }

        /// <summary>
        /// Receives a hit, reducing hit points based on defence items.
        /// </summary>
        /// <param name="hit">Incoming damage</param>
        public void ReceiveHit(int hit)
        {
            int totalDefence = _owner.Inventory.Defence.ReduceHitPoint;
            int reducedHit = System.Math.Max(0, hit - totalDefence);

            _owner.AdjustHitPoints(-reducedHit);
            _logger?.LogInfo($"{_owner.Name} received {reducedHit} damage (after {totalDefence} defence).");
        }
    }
}
