using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloodAction : BattleAction
{
    public FloodAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Only usable when not in front rank
        actorRestrictions = new List<TargetingRestriction>()
        {
            new RankCellsRestriction(this, 1, 2)
        };

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction(this, TargetableFormation.Other),
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedFormation(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new DoDamageNode(this, 10)
        };
    }
}
