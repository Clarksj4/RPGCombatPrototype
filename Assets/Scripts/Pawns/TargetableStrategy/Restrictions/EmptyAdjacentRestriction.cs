using UnityEngine;
using System.Collections;

public class EmptyAdjacentRestriction : TargetableCellRestriction
{
    public EmptyAdjacentRestriction(BattleAction action)
        : base(action) { /* Nothing! */ }
    
    public override bool IsTargetValid(Formation formation, Vector2Int coordinate)
    {
        return IsDestinationOnFormation(formation, coordinate) &&
                IsDestinationEmpty(formation, coordinate);
    }

    private bool IsDestinationOnFormation(Formation formation, Vector2Int position)
    {
        // Get final position
        Vector2Int destinationCoordinate = GetDestinationCoordinate(position);
        return formation.ContainsCoordinate(destinationCoordinate);
    }

    private bool IsDestinationEmpty(Formation formation, Vector2Int position)
    {
        // Get final position
        Vector2Int destinationCoordinate = GetDestinationCoordinate(position);
        return formation.GetPawnAtCoordinate(destinationCoordinate) == null;
    }

    private Vector2Int GetDestinationCoordinate(Vector2Int position)
    {
        // Get direction to target
        Vector2Int direction = GetDirectionToTarget(position);

        // Get final position
        Vector2Int destinationCoordinate = position + (direction * 1);
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
