using UnityEngine;
using System.Linq;

public class ExposedCellsRestriction : TargetingRestriction
{
    public override bool IsTargetValid(Pawn actor, Cell cell)
    {
        Formation formation = cell.Formation;
        Vector2Int facing = formation.Facing;

        Vector2Int walker = cell.Coordinate + facing;
        while (formation.Contains(walker))
        {
            // If there is a cell in front that contains anything
            // then the given cell is not exposed.
            Cell obscuringCell = formation.Grid.GetCell(walker);
            if (obscuringCell != null && obscuringCell.Contents.Any())
                return false;

            walker += facing;
        }
        return true;
    }
}
