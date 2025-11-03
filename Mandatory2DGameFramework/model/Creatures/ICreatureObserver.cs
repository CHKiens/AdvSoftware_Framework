using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Creatures
{
    /// <summary>
    /// Observer interface for creatures.
    /// </summary>
    public interface ICreatureObserver
    {
        /// <summary>
        /// Called whenever a creature receives a hit.
        /// </summary>
        /// <param name="creature">The creature that was hit.</param>
        /// <param name="e">Event arguments describing the hit.</param>
        void OnCreatureHit(Creature creature, CreatureHitEventArgs e);
    }
}
