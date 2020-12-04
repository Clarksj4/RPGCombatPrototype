using UnityEngine;

public class RangeRestriction : TargetableCellRestriction
{
    private int range;

    public RangeRestriction(BattleAction action, int range)
        : base(action) 
    {
        this.range = range;
    }

    public override bool IsTargetValid(Formation formation, Vector2Int coordinate)
    {
        bool infiniteRange = range < 0;
        bool positionInRange = formation.IsInRange(action.OriginPosition, coordinate, range);

        bool isRangeValid = infiniteRange || positionInRange;
        return isRangeValid;
    }
}
