using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.Creatures;
using Mandatory2DGameFramework.model.strategy;
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
    public abstract class GameObjectFactory
    {
        //Ændre til brug af interface i stedet for konkrete klasser
        public Creature CreateWarrior(string name, int posx, int posy, ILogger? logger)
        {
            return new Warrior(name, posx, posy, logger);
        }

        public Creature CreateHunter(string name, int posx, int posy, ILogger? logger)
        {
           return new Hunter(name, posx, posy, logger);
        }

        public Wall CreateWall(int posx, int posy)
        {
            return new Wall(posx, posy);
        }


    }
}
