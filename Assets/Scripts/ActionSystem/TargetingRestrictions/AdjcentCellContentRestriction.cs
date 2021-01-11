using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AdjcentCellContentRestriction : CellContentRestriction
{
    private RelativeDirection directions;

    public AdjcentCellContentRestriction(BattleAction action, TargetableCellContent targetableContent, RelativeDirection directions)
        : base(action, targetableContent) 
    {
        this.directions = directions;
    }

    public override bool IsTargetValid(Cell cell)
    {
        // Get closest cell on target formation to actor.
        IEnumerable<Vector2Int> relativeDirections = cell.Coordinate.GetRelativeDirections(action.OriginCell.Coordinate, directions);

        foreach (Vector2Int relativeDirection in relativeDirections)
        {
            Vector2Int adjacentPosition = cell.Coordinate + relativeDirection;
            Cell adjacentCell = action.Grid.GetCell(adjacentPosition);

            // There's gota be a cell there to test
            bool conformsToRestriction = false;
            if (adjacentCell != null)
                conformsToRestriction = base.IsTargetValid(adjacentCell);

            // If its not empty then halt iteration.
            if (!conformsToRestriction)
                return false;
        }

        return true;
    }
}
