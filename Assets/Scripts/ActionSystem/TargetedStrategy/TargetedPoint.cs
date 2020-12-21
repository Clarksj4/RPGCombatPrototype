using UnityEngine;
using System.Collections.Generic;

public class TargetedPoint : TargetedStrategy
{
    public TargetedPoint(BattleAction action)
        : base(action) { /* Nothing */ }

    public override IEnumerable<Cell> GetAffectedCoordinates()
    {
        yield return action.TargetCell;
    }
}
