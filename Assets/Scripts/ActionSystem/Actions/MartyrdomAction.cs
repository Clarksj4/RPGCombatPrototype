using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MartyrdomAction : BattleAction
{
    public MartyrdomAction(Actor actor)
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

        // Take damage on behalf of ally
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode(this) { Status = new GuardedStatus(2, Actor) }
        };
    }
}
