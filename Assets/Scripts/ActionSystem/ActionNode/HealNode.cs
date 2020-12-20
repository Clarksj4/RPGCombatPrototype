using System.Linq;
using UnityEngine;

public class HealNode : ActionNode
{
    public HealNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell cell)
    {
        bool any = cell.Contents.Any(c => c is IDefender);

        foreach (IGridBased target in cell.Contents)
        {
            if (target is IDefender)
            {
                IDefender defender = target as IDefender;
                defender.Health += 10;
            }
        }

        return any;
    }
}
