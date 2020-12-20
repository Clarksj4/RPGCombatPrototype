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

    public override bool ApplyToCell(Cell cell)
    {
        bool any = cell.Contents.Any(c => c is IDefender);

        foreach (IGridBased target in cell.Contents)
        {
            if (target is Pawn)
            {
                Pawn pawn = target as Pawn;

                // Get final positions
                Vector2Int destinationDirection = cell.Coordinate.GetRelativeDirections(action.Actor.Coordinate, direction).Single();
                Vector2Int destinationCoordinate = cell.Coordinate + (destinationDirection * distance);
                Vector3 destinationWorldPosition = action.Grid.CoordinateToWorldPosition(destinationCoordinate);

                // Move target to position over time
                Sequence sequence = DOTween.Sequence();
                sequence.Append(pawn.transform.DOMove(destinationWorldPosition, 0.25f).SetEase(Ease.OutExpo));
                sequence.OnComplete(() => {
                    // Update coordinate on arrival
                    pawn.SetCoordinate(destinationCoordinate);
                });

            }
        }

        return any;
    }
}
