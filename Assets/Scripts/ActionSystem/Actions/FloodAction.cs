using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloodAction : BattleAction
{
    public FloodAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

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
        actionSequence = new List<ActionNode>()
        {
            new DoDamageNode(this)
        };
    }
}
