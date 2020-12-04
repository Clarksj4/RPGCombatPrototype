using DG.Tweening;
using UnityEngine;

public class MoveActorNode : ActionNode
{
    public MoveActorNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Formation formation, Vector2Int position)
    {
        // Get final position
        Vector3 targetWorldPosition = formation.CoordinateToWorldPosition(position);

        // Move actor to position over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(action.Actor.transform.DOMove(targetWorldPosition, 0.5f).SetEase(Ease.OutQuad));
        sequence.OnComplete(() => {
            // Update coordinate on arrival
            action.Actor.SetCoordinate(position);
        });

        return true;

        // Invokers wlil know when the move is complete because
        // the coroutine has ended.
        //yield return sequence.WaitForCompletion();
    }
}
