using System.Collections.Generic;

public class FireboltAction : BattleAction
{
    public FireboltAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Knowing what we can target
        targetRestrictions = new List<TargetingRestriction>()
        {
            new LinearCellsRestriction(this),
            new FormationRestriction(this, TargetableFormation.Other),
            new CellContentRestriction(this, TargetableCellContent.Enemy),
            new ExposedCellsRestriction(this)
        };
        targetedStrategy = new TargetedPoint(this);

        // Knowing what we do.
        actionSequence = new List<ActionNode>()
        {
            new IsHitNode(this),
            new DoDamageNode(this)
        };
    }
}
