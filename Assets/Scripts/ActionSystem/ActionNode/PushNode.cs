using DG.Tweening;
using System.Linq;
using UnityEngine;

public class PushNode : ActionNode
{
    /// <summary>
    /// The distance that the target will be pushed.
    /// </summary>
    private int distance;
    private RelativeDirection direction;

    public PushNode(BattleAction action, int distance, RelativeDirection direction)
        : base(action) 
    {
        this.distance = distance;
        this.direction = direction;
    }

    public override bool ApplyToCell(Formation formation, Vector2Int position)
    {
        // Get target pawn
        Pawn target = action.TargetFormation.GetPawnAtCoordinate(position);

        // Get final positions
        Vector2Int destinationDirection = position.GetRelativeDirections(action.Actor.GridPosition, direction).Single();
        Vector2Int destinationCoordinate = position + (destinationDirection * distance);
        Vector3 destinationWorldPosition = formation.CoordinateToWorldPosition(destinationCoordinate);

        // Move target to position over time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(target.transform.DOMove(destinationWorldPosition, 0.25f).SetEase(Ease.OutExpo));
        sequence.OnComplete(() => {
            // Update coordinate on arrival
            target.SetCoordinate(destinationCoordinate);
        });

        return true;
    }
}
