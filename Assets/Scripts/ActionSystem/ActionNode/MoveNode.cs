using DG.Tweening;
using UnityEngine;

public class MoveNode : ActionNode
{
    public MoveNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell cell)
    {
        TranslatePawn(action.Actor, cell);

        return true;
    }

    protected void TranslatePawn(Pawn pawn, Cell cell)
    {
        // Get final position
        Vector3 targetWorldPosition = cell.WorldPosition;

        // Move actor to position over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(pawn.transform.DOMove(targetWorldPosition, 0.5f).SetEase(Ease.OutQuad));
        sequence.OnComplete(() => {
            // Update coordinate on arrival
            pawn.SetCell(cell);
        });
    }
}
