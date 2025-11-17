using Mandatory2DGameFramework.model.defence;
using System.Xml.Linq;

public class DefenceDecorator : DefenceItem
{
    protected readonly DefenceItem _inner;
    public int Bonus { get; }

    public DefenceDecorator(DefenceItem inner, int bonus)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        Bonus = bonus;
        Name = inner.Name;
        Lootable = inner.Lootable;
    }

    public override int ReduceHitPoint => Math.Max(0, _inner.ReduceHitPoint + Bonus);
}
