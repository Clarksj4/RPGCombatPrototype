﻿using System.Collections.Generic;

public class FreezeAction : BattleAction
{
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
            new IsHitNode() { Actor = Actor },
            new DoDamageNode() { Actor = Actor, BaseDamage = 10 },
            new ApplyStatusNode() { Actor = Actor, Status = new ImmobilizedStatus() { Duration = 2 } }
        };
    }
}
