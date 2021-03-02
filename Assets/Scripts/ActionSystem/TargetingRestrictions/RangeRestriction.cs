using System.Linq;
using System.Reflection;
using UnityEngine;

public class RangeRestriction : TargetingRestriction
{
    public string Range;

    public override bool IsTargetValid(Pawn actor, Cell cell)
    {
        int range = GetRange(Range, actor);

        bool infiniteRange = range < 0;
        int distance = actor.Coordinate.GetTravelDistance(cell.Coordinate);
        bool positionInRange = distance <= range;

        bool isRangeValid = infiniteRange || positionInRange;
        return isRangeValid;
    }

    public int GetRange(string rangeString, Pawn actor)
    {
        return actor.Stats[rangeString].Value;
    }
}
