using UnityEngine;

public class RangeRestriction : TargetingRestriction
{
    /// <summary>
    /// Gets or sets the range the targeted cells
    /// must be within to be valid.
    /// </summary>
    public int Range { get; set; }

    public override bool IsTargetValid(Cell cell)
    {
        bool infiniteRange = Range < 0;
        int distance = Actor.Coordinate.GetTravelDistance(cell.Coordinate);
        bool positionInRange = distance <= Range;

        bool isRangeValid = infiniteRange || positionInRange;
        return isRangeValid;
    }
}
