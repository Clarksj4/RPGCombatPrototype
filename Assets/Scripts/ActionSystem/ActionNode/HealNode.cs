using System.Linq;
using UnityEngine;

public class HealNode : ActionNode
{
    public HealNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell originCell, Cell targetCell)
    {
        Pawn target = targetCell.GetContent<Pawn>();

        if (target != null)
        {
            target.Health += 10;
            return true;
        }

        return false;
    }
}
