using Mandatory2DGameFramework.model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.strategy
{
    public interface IDamageStrategy
    {
        int CalculateDamage(Creature attacker, Creature target);
    }
}
