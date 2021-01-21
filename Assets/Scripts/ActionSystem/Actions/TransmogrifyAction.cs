using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransmogrifyAction : BattleAction
{
    public TransmogrifyAction(Actor actor)
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
            new SwapHealthNode(this) 
        };
    }
}
