using UnityEngine;

public class LinearCellsRestriction : TargetingRestriction
{
    public override bool IsTargetValid(Pawn actor, Cell cell)
    {
        return cell.Coordinate.x == actor.Coordinate.x ||
                cell.Coordinate.y == actor.Coordinate.y;
    }
}
