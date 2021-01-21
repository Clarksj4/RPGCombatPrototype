using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SleepingDraughtAction : BattleAction
{
    public SleepingDraughtAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Enemy),
            new ExposedCellsRestriction(this)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode(this) { Status = new DrowsyStatus(1, 2) }
        };
    }
}
