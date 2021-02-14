using UnityEngine;
using System.Collections.Generic;

public class AdjcentCellContentRestriction : CellContentRestriction
{
    /// <summary>
    /// Gets the adjacent directions relative to the actor
    /// that will be checked for their content.
    /// </summary>
    public RelativeDirection Directions;

    public override bool IsTargetValid(Pawn actor, Cell cell)
    {
        // Get closest cell on target formation to actor.
        IEnumerable<Vector2Int> relativeDirections = cell.Coordinate.GetRelativeDirections(actor.Coordinate, Directions);

        foreach (Vector2Int relativeDirection in relativeDirections)
        {
            Vector2Int adjacentPosition = cell.Coordinate + relativeDirection;
            Cell adjacentCell = actor.Grid.GetCell(adjacentPosition);

            // There's gota be a cell there to test
            bool conformsToRestriction = false;
            if (adjacentCell != null)
                conformsToRestriction = base.IsTargetValid(actor, adjacentCell);

            // If its not empty then halt iteration.
            if (!conformsToRestriction)
                return false;
        }

        return true;
    }
}
