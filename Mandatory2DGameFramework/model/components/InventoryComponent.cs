using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.Creatures;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.components
{
    /// <summary>
    /// Handles a creatures inventory - equipped weapons and defensive items.
    /// Supports looting objects from the world.
    /// </summary>
    public class InventoryComponent
    {
        private readonly Creature _owner;
        private readonly ILogger? _logger;

        /// <summary>
        /// The currently equipped weapon.
        /// </summary>
        public AttackItem? EquippedWeapon { get; private set; }

        /// <summary>
        /// The currently equipped defensive items.
        /// </summary>
        public DefenceComposite Defence { get; }

        /// <summary>
        /// The maximum number of defensive items a creature can equip.
        /// </summary>
        public const int MaxDefenceItems = 3;

        /// <summary>
        /// Initializes a new instance of the InventoryComponent class.
        /// </summary>
        /// <param name="owner">Creature - owner of the inventory</param>
        /// <param name="logger">Optional logger</param>
        public InventoryComponent(Creature owner, ILogger? logger = null)
        {
            _owner = owner;
            _logger = logger;
            Defence = new DefenceComposite();
        }

        /// <summary>
        /// Loots a world object, equipping it if possible.
        /// </summary>
        /// <param name="obj">Object to loot</param>
        public void Loot(WorldObject obj)
        {
            if (!obj.Lootable)
            {
                _logger?.LogWarning($"{_owner.Name} tried to loot {obj.Name}, but it is not lootable.");
                return;
            }

            switch (obj)
            {
                case AttackItem weapon:
                    EquippedWeapon = weapon;
                    _logger?.LogInfo($"{_owner.Name} equipped {weapon.Name}");
                    break;

                case DefenceItem defence:
                    if (Defence.Items.Count >= MaxDefenceItems)
                    {
                        _logger?.LogWarning($"{_owner.Name} cannot carry more defence items.");
                        return;
                    }
                    Defence.Add(defence);
                    _logger?.LogInfo($"{_owner.Name} equipped {defence.Name}");
                    break;
            }
        }
    }
}
