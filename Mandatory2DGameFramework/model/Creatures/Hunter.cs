using Mandatory2DGameFramework.logger;
using Mandatory2DGameFramework.model.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Creatures
{
    public class Hunter : Creature
    {
        public Hunter(string name, ILogger? logger = null) : base(logger)
        {
            Name = name;
            DamageStrategy = new CritDamageStrategy();
        }
    }
}
