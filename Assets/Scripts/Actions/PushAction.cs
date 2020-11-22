using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAction : BattleAction
{
    /// <summary>
    /// The range of this push action.
    /// </summary>
    private const int RANGE = 1;
    /// <summary>
    /// The distance that the target will be pushed.
    /// </summary>
    private const int PUSH_DISTANCE = 1;

    public override int Range { get { return RANGE; } }

    public override ActionTag[] Tags { get { return tags; } }
    private ActionTag[] tags = new ActionTag[] { ActionTag.Movement, ActionTag.Forced };

    public override bool IsActorAble(Actor actor)
    {
        return !actor.Incapacitated;
    }

    public override bool IsTargetValid(Formation formation, Vector2Int position)
    {
        Pawn target = formation.GetPawnAtCoordinate(position);
        bool inRange = formation.IsInRange(OriginPosition, position, Range);
        bool destinationEmpty = IsDestinationEmpty(formation, position);

        return target != null &&
                target != Actor &&
                inRange && 
                destinationEmpty;
    }

    public override IEnumerator Do()
    {
        // Get target pawn
        Pawn target = TargetFormation.GetPawnAtCoordinate(TargetPosition);

        // Get final positions
        Vector2Int destinationCoordinate = GetDestinationCoordinate(TargetFormation, TargetPosition);
        Vector3 destinationWorldPosition = TargetFormation.CoordinateToWorldPosition(destinationCoordinate);

        // Move target to position over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(target.transform.DOMove(destinationWorldPosition, 0.25f).SetEase(Ease.OutExpo));
        sequence.OnComplete(() => {
            // Update coordinate on arrival
            target.SetCoordinate(destinationCoordinate);
        });

        // Invokers wlil know when the move is complete because
        // the coroutine has ended.
        yield return sequence.WaitForCompletion();
    }

    private Vector2Int GetDestinationCoordinate(Formation formation, Vector2Int position)
    {
        // Get direction to target
        Vector2Int direction = GetDirectionToTarget(position);

        // Get final position
        Vector2Int destinationCoordinate = position + (direction * PUSH_DISTANCE);
        return destinationCoordinate;
    }

    private Vector2Int GetDirectionToTarget(Vector2Int targetPosition)
    {
        // Get direction to defender
        Vector2Int delta = targetPosition - Actor.GridPosition;
        Vector2Int direction = delta.Reduce();
        return direction;
    }

    private bool IsDestinationEmpty(Formation formation, Vector2Int position)
    {
        // Get final position
        Vector2Int destinationCoordinate = GetDestinationCoordinate(formation, position);
        return formation.GetPawnAtCoordinate(destinationCoordinate) == null;
    }

    public override IEnumerable<Vector2Int> GetArea()
    {
        yield return TargetPosition;
    }
}
