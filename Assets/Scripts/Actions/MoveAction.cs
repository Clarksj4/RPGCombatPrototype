using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BattleAction
{
    public override int Range { get { return Actor.Movement; } }

    public override ActionTag[] Tags { get { return tags; } }
    private ActionTag[] tags = new ActionTag[] { ActionTag.Movement };

    public override bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }

    public override bool IsTargetValid(Formation formation, Vector2Int position)
    {
        bool isOnSameFormation = formation == OriginFormation;
        bool cellEmpty = formation.GetPawnAtCoordinate(position) == null;
        bool isInRange = formation.IsInRange(OriginPosition, position, Range);

        return isOnSameFormation &&
                cellEmpty &&
                isInRange;
    }

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

    public override IEnumerable<Vector2Int> GetArea()
    {
        // TODO: actually do some path finding or something.
        yield return TargetPosition;
    }
}