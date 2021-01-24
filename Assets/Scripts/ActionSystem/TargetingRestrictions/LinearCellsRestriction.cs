using UnityEngine;

public class LinearCellsRestriction : TargetingRestriction
{
    public override bool IsTargetValid(Cell cell)
    {
        return cell.Coordinate.x == Actor.Coordinate.x ||
                cell.Coordinate.y == Actor.Coordinate.y;
    }
}
