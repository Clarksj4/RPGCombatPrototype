using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindWalkAction : BattleAction
{
    public WindWalkAction(Actor actor)
        : base(actor) { /* Nothing! */ }

    protected override void Setup()
    {
        // Target any ally
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Ally)
        };

        // Just the one ally actually...
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // Instantiate instances separately because need to link em.
        EvasiveStatus evasive = new EvasiveStatus() { Duration = 2, AttacksToEvade = 1 };
        AgilityStatus agility = new AgilityStatus() { Duration = 1, BonusMovement = 1, LinkedTo = evasive };

        // Move faster and evade an attack...
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode(this) { Status = evasive },
            new ApplyStatusNode(this) { Status = agility }
        };
    }
}
