using UnityEngine;

public class RangeRestriction : TargetableCellRestriction
{
    public RangeRestriction(BattleAction action)
        : base(action) { /* Nothing! */ }

    public override bool IsTargetValid(Formation formation, Vector2Int coordinate)
    {
        bool infiniteRange = action.Range < 0;
        bool positionInRange = formation.IsInRange(action.OriginPosition, coordinate, action.Range);

        bool isRangeValid = infiniteRange || positionInRange;
        return isRangeValid;
    }
}
