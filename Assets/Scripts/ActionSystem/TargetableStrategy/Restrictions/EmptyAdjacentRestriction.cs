using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class EmptyAdjacentRestriction : TargetableCellRestriction
{
    private RelativeDirection directions;
    private bool allEmpty;

    public EmptyAdjacentRestriction(BattleAction action, RelativeDirection directions, bool allEmpty = true)
        : base(action) 
    {
        this.directions = directions;
        this.allEmpty = allEmpty;
    }

    public override bool IsTargetValid(Formation formation, Vector2Int coordinate)
    {
        bool valid;
        if (allEmpty)
            valid = AllAdjacentCellsEmpty(formation, coordinate);
        else
            valid = AnyAdjacentCellsEmpty(formation, coordinate);
        return valid;
    }

    private bool AllAdjacentCellsEmpty(Formation formation, Vector2Int coordinate)
    {
        // Get closest cell on target formation to actor.
        Vector2Int sameFormationOrigin = formation.GetClosestCoordinate(action.Actor.WorldPosition);
        IEnumerable<Vector2Int> relativeDirections = coordinate.GetRelativeDirections(sameFormationOrigin, directions);

        foreach (Vector2Int relativeDirection in relativeDirections)
        {
            Vector2Int adjacentPosition = coordinate + relativeDirection;

            bool containsCoordinate = formation.ContainsCoordinate(adjacentPosition);
            bool empty = formation.GetPawnAtCoordinate(adjacentPosition) == null;

            if (!containsCoordinate ||
                !empty)
                return false;
        }

        return true;
    }

    private bool AnyAdjacentCellsEmpty(Formation formation, Vector2Int coordinate)
    {
        // Get closest cell on target formation to actor.
        Vector2Int sameFormationOrigin = formation.GetClosestCoordinate(action.Actor.WorldPosition);
        IEnumerable<Vector2Int> relativeDirections = coordinate.GetRelativeDirections(sameFormationOrigin, directions);

        foreach (Vector2Int relativeDirection in relativeDirections)
        {
            Vector2Int adjacentPosition = coordinate + relativeDirection;

            bool containsCoordinate = formation.ContainsCoordinate(adjacentPosition);
            bool empty = formation.GetPawnAtCoordinate(adjacentPosition) == null;

            if (containsCoordinate &&
                empty)
                return true;
        }

        return false;
    }
}
