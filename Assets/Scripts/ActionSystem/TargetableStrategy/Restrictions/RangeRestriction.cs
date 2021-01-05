using UnityEngine;

public class RangeRestriction : TargetableCellRestriction
{
    public RangeRestriction(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool IsTargetValid(Cell cell)
    {
        bool infiniteRange = action.Range < 0;
        int distance = action.OriginCell.Coordinate.GetTravelDistance(cell.Coordinate);
        bool positionInRange = distance <= action.Range;

        bool isRangeValid = infiniteRange || positionInRange;
        return isRangeValid;
    }
}
