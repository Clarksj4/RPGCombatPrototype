using DG.Tweening;
using UnityEngine;

public class PushNode : ActionNode
{
    /// <summary>
    /// The distance that the target will be pushed.
    /// </summary>
    private const int PUSH_DISTANCE = 1;

    public PushNode(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool ApplyToCell(Formation formation, Vector2Int position)
    {
        // Get target pawn
        Pawn target = action.TargetFormation.GetPawnAtCoordinate(position);

        // Get final positions
        Vector2Int destinationCoordinate = GetDestinationCoordinate(position);
        Vector3 destinationWorldPosition = formation.CoordinateToWorldPosition(destinationCoordinate);

        // Move target to position over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(target.transform.DOMove(destinationWorldPosition, 0.25f).SetEase(Ease.OutExpo));
        sequence.OnComplete(() => {
            // Update coordinate on arrival
            target.SetCoordinate(destinationCoordinate);
        });

        return true;

        // Invokers wlil know when the move is complete because
        // the coroutine has ended.
        //yield return sequence.WaitForCompletion();
    }

    private Vector2Int GetDestinationCoordinate(Vector2Int position)
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
        Vector2Int delta = targetPosition - action.Actor.GridPosition;
        Vector2Int direction = delta.Reduce();
        return direction;
    }
}
