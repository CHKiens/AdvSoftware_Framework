using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Creatures
{
    public class CreatureHitEventArgs : EventArgs
    {
        public int DamageReceived { get; set; }
        public int PreviousHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public bool IsDead { get; set; }
    }
}
