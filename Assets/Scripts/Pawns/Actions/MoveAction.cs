using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MoveAction : BattleAction
{
    public override int Range { get { return Actor.Movement; } }
    public override ActionTag Tags { get { return ActionTag.Movement; } }
    public override TargetableCellContent TargetableCellContent { get { return TargetableCellContent.Empty; } }

    public override IEnumerator Do()
    {
        // Get final position
        Vector3 targetWorldPosition = TargetFormation.CoordinateToWorldPosition(TargetPosition);
            
        // Move actor to position over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(Actor.transform.DOMove(targetWorldPosition, 0.5f).SetEase(Ease.OutQuad));
        sequence.OnComplete(() => {
            // Update coordinate on arrival
            Actor.SetCoordinate(TargetPosition);
        });

        // Invokers wlil know when the move is complete because
        // the coroutine has ended.
        yield return sequence.WaitForCompletion();
    }
}