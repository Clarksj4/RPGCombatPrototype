using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SacrificeAction : BattleAction
{
    public SacrificeAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Heal;

        actorRestrictions = new List<TargetingRestriction>()
        {
            new HealthRestriction(this, 10 + 1)
        };

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

        beginningActions = new List<ActionNode>()
        {
            new DoDamageNode(this, 10) { Target = Actor.Cell }
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new HealNode(this) { Amount = 10 }
        };
    }
}
