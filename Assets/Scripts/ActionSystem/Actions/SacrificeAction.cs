using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SacrificeAction : BattleAction
{
    public SacrificeAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Heal;

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
        actionSequence = new List<ActionNode>()
        {
            new GiveHealthNode(this, 10)
        };
    }
}
