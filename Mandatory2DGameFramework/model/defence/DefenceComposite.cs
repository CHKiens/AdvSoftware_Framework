using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    public class DefenceComposite : DefenceItem
    {
        private List<DefenceItem> _items = new List<DefenceItem>();

        public void Add(DefenceItem item) => _items.Add(item);
        public void Remove(DefenceItem item) => _items.Remove(item);

        public override int ReduceHitPoint => _items.Sum(i => i.ReduceHitPoint);

        public IReadOnlyList<DefenceItem> Items => _items.AsReadOnly();
    }

}
