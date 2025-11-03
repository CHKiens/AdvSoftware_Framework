using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandatory2DGameFramework.worlds;

namespace Mandatory2DGameFramework.model.wall
{
    public class Wall:WorldObject
    {
        public Wall(int y, int x)
        {
            posY = y;
            posX = x;
            Name = "Wall";
            Lootable = false;
            Removeable = false;
        }
    }
}
