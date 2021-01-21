using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireShieldAction : BattleAction
{
    public FireShieldAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Can only target self
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Self),
        };

        // Affects adjacent cells
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Heals and buffs adjacent
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode(this) { Status = new PoisonStatus(3) },
            new ApplyStatusNode(this) { Status = new InvulnerabilityStatus(1) }
        };
    }
}
