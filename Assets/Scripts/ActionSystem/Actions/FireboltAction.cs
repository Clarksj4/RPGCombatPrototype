using System.Collections.Generic;

public class FireboltAction : BattleAction
{
    public FireboltAction()
        : base()
    {
        // Misc information about the ability
        Tags = ActionTag.Damage;

        // Knowing what we can target
        targetableFormation = TargetableFormation.Other;
        targetableStrategy = new LinearCells(this);
        targetRestrictions = new List<TargetingRestriction>()
        {
            new CellContentRestriction(this, TargetableCellContent.Enemy)
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
