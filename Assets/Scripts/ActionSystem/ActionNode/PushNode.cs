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

    public override bool ApplyToCell(Cell originCell, Cell targetCell)
    {
        Pawn target = targetCell.GetContent<Pawn>();

        if (target != null)
        {
            Cell destination = GetDestinationCell(originCell, targetCell);
            if (destination != null && !destination.IsOccupied())
            {
                // Move target to position over time
                Sequence sequence = DOTween.Sequence();
                sequence.Append(target.transform.DOMove(targetCell.WorldPosition, 0.25f).SetEase(Ease.OutExpo));
                sequence.OnComplete(() => {
                    // Update coordinate on arrival
                    target.SetCell(destination);
                });

                return true;
            }
        }

        return false;
    }

    private Cell GetDestinationCell(Cell originCell, Cell targetCell)
    {
        // Get direction of push
        Vector2Int destinationDirection = targetCell.Coordinate.GetRelativeDirections(originCell.Coordinate, direction).Single();

        // Convert to coordinate
        Vector2Int destinationCoordinate = targetCell.Coordinate + (destinationDirection * distance);

        // Get cell at coordinate
        return targetCell.Grid.GetCell(destinationCoordinate);

    }
}
