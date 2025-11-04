using Mandatory2DGameFramework.model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    public class World
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public string Difficulty { get; set; }

        // world objects
        private List<WorldObject> _worldObjects;
        // world creatures
        private List<Creature> _creatures;

        public World(int maxX, int maxY, string difficulty)
        {
            MaxX = maxX;
            MaxY = maxY;
            Difficulty = difficulty;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
        }


        public void AddWorldObject(WorldObject obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _worldObjects.Add(obj);
        }

        public void AddCreature(Creature creature)
        {
            if (creature == null) throw new ArgumentNullException(nameof(creature));
            _creatures.Add(creature);
        }
        public override string ToString()
        {
            return $"{{{nameof(MaxX)}={MaxX.ToString()}, {nameof(MaxY)}={MaxY.ToString()}, {nameof(Difficulty)}}}";
        }
    }
}
