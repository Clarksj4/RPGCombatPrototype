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

        // Move faster and evade an attack...
        targetedActions = new List<ActionNode>()
        {
            new ApplyStatusNode(this) { Status = new EvasiveStatus(2, 1) },
            new ApplyStatusNode(this) { Status = new AgilityStatus(2, 1) },
        };
        
        // TODO: Make agility end once evasive ends?
    }
}
