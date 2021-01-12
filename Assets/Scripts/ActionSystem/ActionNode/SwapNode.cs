using DG.Tweening;
using UnityEngine;

public class SwapNode : MoveNode
{
    public SwapNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell cell)
    {
        // Move actor to target cell
        base.ApplyToCell(cell);

        // Move the target to the actor's cell
        Pawn target = action.TargetCell.GetContent<Pawn>();
        if (target != null)
            TranslatePawn(target, action.OriginCell);

        return true;
    }
}
