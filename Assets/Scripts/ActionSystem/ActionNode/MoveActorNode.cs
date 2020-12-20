using DG.Tweening;
using UnityEngine;

public class MoveActorNode : ActionNode
{
    public MoveActorNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Cell cell)
    {
        // Get final position
        Vector3 targetWorldPosition = cell.WorldPosition;

        // Move actor to position over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(action.Actor.transform.DOMove(targetWorldPosition, 0.5f).SetEase(Ease.OutQuad));
        sequence.OnComplete(() => {
            // Update coordinate on arrival
            action.Actor.SetCell(cell);
        });

        return true;
    }
}
