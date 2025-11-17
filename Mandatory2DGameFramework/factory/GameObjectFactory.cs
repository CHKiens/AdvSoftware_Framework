using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.Creatures;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.model.wall;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mandatory2DGameFramework.factory
{
    public static class GameObjectFactory
    {
        public enum CreatureType
        {
            Hunter,
            Warrior
        }
        public static WorldObject CreateWall(int y, int x) => new Wall(y, x);
        public static DefenceItem CreateDefence(string name, int reduce) =>
            new DefenceItem { Name = name, ReduceHitPoint = reduce };
        public static Creature CreateCreature(
        CreatureType type,
        string name,
        int posX,
        int posY,
        ILogger? logger = null)
        {
            return type switch
            {
                CreatureType.Hunter => new Hunter(name, posX, posY, logger),
                CreatureType.Warrior => new Warrior(name, posX, posY, logger),

                _ => throw new ArgumentOutOfRangeException(nameof(type),
                    $"Unsupported creature type: {type}")
            };
        }


    }
}
