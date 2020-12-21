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

    public override bool IsTargetValid(Cell cell)
    {
        bool valid;
        if (allEmpty)
            valid = AllAdjacentCellsEmpty(cell);
        else
            valid = AnyAdjacentCellsEmpty(cell);
        return valid;
    }

    private bool AllAdjacentCellsEmpty(Cell cell)
    {
        // Get closest cell on target formation to actor.
        IEnumerable<Vector2Int> relativeDirections = cell.Coordinate.GetRelativeDirections(action.OriginPosition, directions);

        foreach (Vector2Int relativeDirection in relativeDirections)
        {
            Vector2Int adjacentPosition = cell.Coordinate + relativeDirection;
            Cell adjacentCell = action.Grid.GetCell(adjacentPosition);

            // Cells that don't exist are considered to be empty.
            bool empty = true;
            if (adjacentCell != null)
                empty = adjacentCell.IsOccupied<IDefender>();
            
            // If its not empty then halt iteration.
            if (!empty)
                return false;
        }

        return true;
    }

    private bool AnyAdjacentCellsEmpty(Cell cell)
    {
        // Get closest cell on target formation to actor.
        IEnumerable<Vector2Int> relativeDirections = cell.Coordinate.GetRelativeDirections(action.OriginPosition, directions);

        foreach (Vector2Int relativeDirection in relativeDirections)
        {
            Vector2Int adjacentPosition = cell.Coordinate + relativeDirection;
            Cell adjacentCell = action.Grid.GetCell(adjacentPosition);

            // Cells that don't exist are considered to be empty.
            bool empty = true;
            if (adjacentCell != null)
                empty = adjacentCell.IsOccupied<IDefender>();

            // If its empty then halt iteration.
            if (empty)
                return true;
        }

        return false;
    }
}
