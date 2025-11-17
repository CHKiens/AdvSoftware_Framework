using Mandatory2DGameFramework.model.Creatures;
using System;
using System.Collections.Generic;

namespace Mandatory2DGameFramework.worlds
{
    public class World
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public string Difficulty { get; set; }

        private readonly List<WorldObject> _worldObjects;
        private readonly List<Creature> _creatures;

        public IReadOnlyList<WorldObject> WorldObjects => _worldObjects.AsReadOnly();
        public IReadOnlyList<Creature> Creatures => _creatures.AsReadOnly();

        public World(int maxX, int maxY, string difficulty)
        {
            MaxX = maxX;
            MaxY = maxY;
            Difficulty = difficulty;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
        }

        /// <summary>
        /// Adds a creature to the world
        /// </summary>
        public void AddCreature(Creature creature)
        {
            if (creature == null) throw new ArgumentNullException(nameof(creature));
            _creatures.Add(creature);
        }

        /// <summary>
        /// Adds a world object to the world
        /// </summary>
        public void AddWorldObject(WorldObject obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _worldObjects.Add(obj);
        }

        /// <summary>
        /// Removes a world object from the world
        /// </summary>
        public bool RemoveWorldObject(WorldObject obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return _worldObjects.Remove(obj);
        }

        public override string ToString()
        {
            return $"World: MaxX={MaxX}, MaxY={MaxY}, Difficulty={Difficulty}, " +
                   $"Creatures={_creatures.Count}, Objects={_worldObjects.Count}";
        }
    }
}
