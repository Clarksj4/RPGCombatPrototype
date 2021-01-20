using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlessAction : BattleAction
{
    public override int Range { get { return 1; } }

    public BlessAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new RangeRestriction(this),
            new CellContentRestriction(this, TargetableCellContent.Ally),
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new HealNode(this) { Amount = 5 },
            new ApplyStatusNode(this) { Status = new PowerStatus(1) }
        };
    }
}
