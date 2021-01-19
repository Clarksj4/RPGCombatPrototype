using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CauterizeAction : BattleAction
{
    public CauterizeAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Ally)
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new HealNode(this) { Amount = 20 },
            new ApplyStatusNode(this) { Status = new VulnerabilityStatus() { Duration = 2 } }
        };
    }
}
