using Mandatory2DGameFramework.worlds;
using System;

namespace Mandatory2DGameFramework.model.defence
{
    public class DefenceItem : WorldObject
    {
        public virtual int ReduceHitPoint { get; set; }

        public DefenceItem()
        {
            Name = string.Empty;
            ReduceHitPoint = 0;
            Lootable = true;
        }

        public override string ToString()
        {
            return $"{{Name={Name}, ReduceHitPoint={ReduceHitPoint}}}";
        }
    }
}
