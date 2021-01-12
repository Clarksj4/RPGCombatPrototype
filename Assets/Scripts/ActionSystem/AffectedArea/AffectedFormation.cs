using UnityEngine;
using System.Collections.Generic;

public class AffectedFormation : AffectedArea
{
    public AffectedFormation(BattleAction action)
        : base(action) { /* Nothing */ }

    public override IEnumerable<Cell> GetAffectedArea()
    {
        return action.TargetCell.Formation.GetCells();
    }
}
