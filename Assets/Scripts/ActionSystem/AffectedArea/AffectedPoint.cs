using UnityEngine;
using System.Collections.Generic;

public class AffectedPoint : AffectedArea
{
    public AffectedPoint(BattleAction action)
        : base(action) { /* Nothing */ }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        yield return action.TargetCell;
    }
}
