using System;
using System.Collections.Generic;
using System.Linq;

namespace Mandatory2DGameFramework.model.defence
{
    /// <summary>
    /// Composite for combining multiple DefenceItems.
    /// Supports operator overload for easy merging.
    /// </summary>
    public class DefenceComposite
    {
        private readonly List<DefenceItem> _items = new();

        /// <summary>
        /// Exposes all items in read-only mode.
        /// External code cannot modify the list directly.
        /// </summary>
        public IReadOnlyList<DefenceItem> Items => _items;

        /// <summary>
        /// Add a defensive item to the composite.
        /// </summary>
        public void Add(DefenceItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Add(item);
        }

        /// <summary>
        /// Remove a defensive item from the composite.
        /// </summary>
        public void Remove(DefenceItem item)
        {
            _items.Remove(item);
        }

        /// <summary>
        /// Calculates total damage reduction from all items.
        /// </summary>
        public int ReduceHitPoint => _items.Sum(i => i.ReduceHitPoint);

        /// <summary>
        /// Combine two DefenceComposites into one.
        /// </summary>
        public static DefenceComposite operator +(DefenceComposite a, DefenceComposite b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));

            var result = new DefenceComposite();
            foreach (var item in a.Items) result.Add(item);
            foreach (var item in b.Items) result.Add(item);

            return result;
        }

        public override string ToString()
        {
            var names = _items.Select(i => i.Name).ToArray();
            return $"DefenceComposite: [{string.Join(", ", names)}], TotalReduce={ReduceHitPoint}";
        }
    }
}
