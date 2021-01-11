using UnityEngine;
using System.Linq;

public class LinearCellsRestriction : TargetingRestriction
{
    public LinearCellsRestriction(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool IsTargetValid(Cell cell)
    {
        return cell.Coordinate.x == action.OriginCell.Coordinate.x ||
                cell.Coordinate.y == action.OriginCell.Coordinate.y;
    }
}
