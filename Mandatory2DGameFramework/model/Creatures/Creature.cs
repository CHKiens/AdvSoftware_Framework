using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.model.components;
using Mandatory2DGameFramework.worlds;
using System.Collections.Generic;

namespace Mandatory2DGameFramework.model.Creatures
{
    public abstract class Creature : ICreature
    {
        /// <summary>
        /// Abstract base class representing a creature in the game world.
        /// A creature can attack, receive damage and loot objects.
        /// Implements the Observer pattern to notify subscribers when hit.
        /// </summary>
        private readonly ILogger? _logger;
        private readonly List<ICreatureObserver> _observers = new();

        /// <summary>
        /// Creature's name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Current hit points of the creature.
        /// </summary>
        protected int HitPoint { get; set; }

        /// <summary>
        /// Indicates if the creature is alive.
        /// </summary>
        protected bool IsAlive { get; set; } = true;

        /// <summary>
        /// How far the creature can move per turn. (movement not really implemented)
        /// </summary>
        public int MoveRange { get; protected set; }


        /// <summary>
        /// X-coordinate in the world.
        /// </summary>
        public int PosX { get; set; }

        /// <summary>
        /// Y-coordinate in the world.
        /// </summary>
        public int PosY { get; set; }


        /// <summary>
        /// Component handling combat logic (attacks and receiving damage).
        /// </summary>
        public CombatComponent Combat { get; }

        /// <summary>
        /// Component handling inventory (weapons, defence items, looting).
        /// </summary>
        public InventoryComponent Inventory { get; }

        /// <summary>
        /// Constructor for Creature.
        /// </summary>
        /// <param name="logger">Optional logger</param>
        protected Creature(ILogger? logger = null)
        {
            _logger = logger;
            Combat = new CombatComponent(this, logger);
            Inventory = new InventoryComponent(this, logger);
        }

        /// <summary>
        /// Attaches an observer to the creature to receive hit notifications.
        /// </summary>
        /// <param name="observer">Observer to attach</param>
        public void Attach(ICreatureObserver observer) => _observers.Add(observer);
        /// <summary>
        /// Detaches an observer from the creature.
        /// </summary>
        /// <param name="observer">Observer to detach</param>

        public void Detach(ICreatureObserver observer) => _observers.Remove(observer);

        /// <summary>
        /// Notifies all attached observers about a hit event.
        /// </summary>
        /// <param name="e">Event argurments</param>
        internal void NotifyObservers(CreatureHitEventArgs e)
        {
            foreach (var obs in _observers) obs.OnCreatureHit(this, e);
        }


        /// <summary>
        /// Gets the current hit points of the creature.
        /// </summary>
        /// <returns>Current creature hitpoints</returns>
        public int GetCurrentHitPoints() => HitPoint;

        /// <summary>
        /// Gets whether the creature is alive.
        /// </summary>
        /// <returns>IsAlive = true or false</returns>
        public bool GetIsAlive() => IsAlive;

        /// <summary>
        /// Adjusts the hit points of the creature by the specified delta.
        /// </summary>
        /// <param name="delta">The amount of hitpoints to adjust by</param>
        internal void AdjustHitPoints(int delta)
        {
            if (!IsAlive) return;

            int previous = HitPoint;
            HitPoint = System.Math.Max(0, HitPoint + delta);

            if (HitPoint == 0) IsAlive = false;

            NotifyObservers(new CreatureHitEventArgs
            {
                DamageReceived = -delta,
                PreviousHitPoints = previous,
                CurrentHitPoints = HitPoint,
                IsDead = !IsAlive
            });

            if (!IsAlive)
                _logger?.LogInfo($"{Name} has died.");
        }

        /// <summary>
        /// Performs an attack on the target creature.
        /// </summary>
        /// <param name="target"></param>
        public void PerformAttack(Creature target) => Combat.Attack(target);
        /// <summary>
        /// Receives a hit with the specified damage.
        /// </summary>
        /// <param name="damage"></param>
        public void ReceiveHit(int damage) => Combat.ReceiveHit(damage);

        /// <summary>
        /// Loots the specified world object.
        /// </summary>
        /// <param name="obj"></param>
        public void Loot(WorldObject obj) => Inventory.Loot(obj);

        /// <summary>
        /// Returns a string representation of the creature.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} - HP: {HitPoint}, Pos: ({PosX},{PosY}), Weapon: {Inventory.EquippedWeapon?.Name ?? "None"}, " +
                   $"Defence: {Inventory.Defence.ReduceHitPoint}, Status: {(IsAlive ? "Alive" : "Dead")}";
        }
    }
}
