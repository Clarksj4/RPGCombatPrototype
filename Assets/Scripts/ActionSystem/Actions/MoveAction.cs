﻿using System.Collections.Generic;

public class MoveAction : BattleAction
{
    protected override void Setup()
    {
        // Misc information about the ability
        Tags = ActionTag.Movement;

        // The cells we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new FormationRestriction() { Actor = Actor, Formations = TargetableFormation.Self },
            new RangeRestriction() { Actor = Actor, Range = Actor.Movement },
            new CellContentRestriction() { Actor = Actor, Content = TargetableCellContent.Empty }
        };

        // The cells that will be affected
        areaOfEffect = new List<AffectedArea>()
        {
            new AffectedPoint(this)
        };

        // The effect upon those cells.
        targetedActions = new List<ActionNode>()
        {
            new MoveNode() { Actor = Actor }
        };
    }
}