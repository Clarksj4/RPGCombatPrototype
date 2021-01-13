using DG.Tweening;
using UnityEngine;

public class SwapNode : MoveNode
{
    public SwapNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell originCell, Cell targetCell)
    {
        // Move actor to target cell
        base.ApplyToCell(originCell, targetCell);

        // Move the target to the actor's cell
        Pawn target = targetCell.GetContent<Pawn>();
        if (target != null)
            TranslatePawn(target, originCell);

        return true;
    }
}
