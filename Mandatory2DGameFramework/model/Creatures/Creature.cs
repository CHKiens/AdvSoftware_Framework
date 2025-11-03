using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.model.strategy;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mandatory2DGameFramework.model.Creatures
{
    /// <summary>
    /// Abstract base class for all creatures in the game world.
    /// Implements Template, Observer, and Damage Strategy patterns.
    /// </summary>
    public abstract class Creature
    {
        private readonly ILogger? _logger;
        private readonly List<ICreatureObserver> _observers = new List<ICreatureObserver>();

        public string Name { get; set; }
        public int HitPoint { get; set; }
        public bool IsAlive { get; set; }
        public int MoveRange { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public AttackItem? AttackWeapon { get; set; }
        public DefenceComposite DefenceItems { get; set; }

        public IDamageStrategy? DamageStrategy { get; set; }

        public const int MaxDefenceItems = 3;

        protected Creature(ILogger? logger = null)
        {
            Name = string.Empty;
            HitPoint = 100;
            IsAlive = true;
            _logger = logger;
            AttackWeapon = null;
            DefenceItems = new DefenceComposite();
        }

        #region Observer Pattern

        public void Attach(ICreatureObserver observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Detach(ICreatureObserver observer)
        {
            _observers.Remove(observer);
        }

        protected void NotifyObservers(CreatureHitEventArgs e)
        {
            foreach (var observer in _observers)
                observer.OnCreatureHit(this, e);
        }

        #endregion

        #region Template Method Pattern

        public void PerformAttack(Creature target)
        {
            if (!CanAttack(target))
            {
                _logger?.LogWarning($"{Name} cannot attack {target.Name}");
                return;
            }

            BeforeAttack();

            int damage = DamageStrategy != null ? DamageStrategy.CalculateDamage(this, target) : Hit();
            target.ReceiveHit(damage);

            AfterAttack(target, damage);
        }

        protected virtual bool CanAttack(Creature target) => IsAlive && target.IsAlive;

        protected virtual void BeforeAttack() => _logger?.LogInfo($"{Name} prepares to attack.");

        protected virtual void AfterAttack(Creature target, int damage)
            => _logger?.LogInfo($"{Name} dealt {damage} damage to {target.Name}.");

        #endregion

        public int Hit() => AttackWeapon?.Hit ?? 1;

        public void ReceiveHit(int hit)
        {
            if (!IsAlive)
            {
                _logger?.LogWarning($"{Name} is already dead and cannot receive more hits.");
                return;
            }

            int totalDefence = DefenceItems.ReduceHitPoint;
            int reducedHit = Math.Max(0, hit - totalDefence);

            int previousHP = HitPoint;
            HitPoint -= reducedHit;

            NotifyObservers(new CreatureHitEventArgs
            {
                DamageReceived = reducedHit,
                PreviousHitPoints = previousHP,
                CurrentHitPoints = HitPoint,
                IsDead = HitPoint <= 0
            });

            _logger?.LogInfo($"{Name} received hit of {reducedHit} points (reduced from {hit} by {totalDefence}).");

            if (HitPoint <= 0)
            {
                HitPoint = 0;
                IsAlive = false;
                _logger?.LogInfo($"{Name} has died.");
            }
        }

        public void Loot(WorldObject obj)
        {
            if (!obj.Lootable)
            {
                _logger?.LogWarning($"{Name} tried to loot {obj.Name}, but it is not lootable.");
                return;
            }

            if (obj is AttackItem attackItem)
            {
                if (AttackWeapon != null)
                    _logger?.LogInfo($"{Name} replaced weapon '{AttackWeapon.Name}' with '{attackItem.Name}'.");
                else
                    _logger?.LogInfo($"{Name} equipped weapon: {attackItem.Name}.");

                AttackWeapon = attackItem;
            }
            else if (obj is DefenceItem defenceItem)
            {
                if (DefenceItems.Items.Count >= MaxDefenceItems)
                {
                    _logger?.LogWarning($"{Name} cannot carry more defence items (max {MaxDefenceItems}).");
                    return;
                }

                DefenceItems.Add(defenceItem);
                _logger?.LogInfo($"{Name} equipped defence item: {defenceItem.Name} (Total defence: {DefenceItems.ReduceHitPoint}).");
            }
            else
            {
                _logger?.LogWarning($"{Name} tried to loot {obj.Name}, but it is neither an attack nor a defence item.");
            }
        }

        public override string ToString()
        {
            string weaponInfo = AttackWeapon != null ? AttackWeapon.Name : "None (bare hands)";
            int totalDefence = DefenceItems.ReduceHitPoint;

            return $"{Name} - HP: {HitPoint}, Position: ({PosX},{PosY}), " +
                   $"Weapon: {weaponInfo}, Defence: {DefenceItems.Items.Count}/{MaxDefenceItems} (Total: {totalDefence}), " +
                   $"Status: {(IsAlive ? "Alive" : "Dead")}";
        }
    }
}
